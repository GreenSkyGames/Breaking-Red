using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine.SceneManagement; //to change scenes when game over 


//Simple class to set up example of dynamic binding
//This virtual should set the NPCs to be hostile.
public class NPC
{
	public virtual bool setNonHostile()
	{
		return true;
	}

}

//This override should set the NPCs to be nonhostile.
//This guarantees they won't be hostile at first.
public class setNPC : NPC
{
	public override bool setNonHostile()
	{
		return false;
	}
}


/* Name: Todd Carter
 * Role: Team Lead 2 -- Software Architect
 *	This is the script for the NPCManager.
 *	It controls the majority of NPC actions and activities.
 *	This includes combat, swapping animations, and utilizing
 *	the dialogueBox canvas that is assigned through the editor.
 *
*/
public class NPCManager : MonoBehaviour
{
	public static Dictionary<string, bool> deadNPCs = new Dictionary<string, bool>();  // Track all dead NPCs by tag
	public float speed = 2;
	public float attackRange = 1;
	public float attackCooldown = 1;
	public float playerDetectRange = 5;
	public Transform detectionPoint;
	//public Transform attackPoint;
	public LayerMask playerLayer;
	//private bool isChasing;

	//private Canvas myCanvas;
	private GameObject myCanvas;

	private float attackCooldownTimer;
	private int facingDirection = -1;
	private EnemyState enemyState;

	private Rigidbody2D rb;
	private Transform player;
	private Animator anim;

	public bool isHostile = false;
	public bool facingRight;

    public SpriteRenderer enemySR;
	public int currentHealth = 9;

	private List<string> _clueList = new List<string>();

	private NPC _npc;

	private GameObject door; //The doors they can open, assigned by tag
	public GameObject lootPrefab; //Assign their loot to their prefab

	public bool metPlayer = false;
	private DialogueManager dialogueManager;
	private InventoryManager inventoryManager;

	private GameObject playerObj;

	private bool hasDropped = false;

    //Start is currently being used to:
	// - find rigidbody component
	// - find animator component
	// - change default animation state to idle
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		changeState(EnemyState.Idle);

		myCanvas = GameObject.Find("DialogueBoxCanvas");

		dialogueManager = FindAnyObjectByType<DialogueManager>();
		inventoryManager = FindAnyObjectByType<InventoryManager>();

		playerObj = GameObject.FindWithTag("Player");

		// If this NPC is marked as dead, destroy it immediately
		if (deadNPCs.ContainsKey(gameObject.tag) && deadNPCs[gameObject.tag])
		{
			Destroy(gameObject);
			return;
		}

		//Debug.Log("myCanvas tag is " + myCanvas.tag);

		//Here is the usage of the dynamic binding.
		_npc = new setNPC();
		bool hostility = _npc.setNonHostile();
		setHostility(hostility);
    }

	//OnEnable is being used to create a delay that ensures the GameEventsManager has started first.
	private void OnEnable()
	{
		StartCoroutine(WaitForGameEventsManager());
	}

	//Once the GameEventsManager has started, these functions can be subscribed as events.
	private IEnumerator WaitForGameEventsManager()
	{
		while(GameEventsManager.instance == null || GameEventsManager.instance.dialogueEvents == null)
		{
			yield return null;
		}
		Debug.Log("Hostility enable test" + gameObject.tag);
		GameEventsManager.instance.dialogueEvents.onStartHostility += onHostility;
		GameEventsManager.instance.dialogueEvents.onStopHostility += offHostility;
		//GameEventsManager.instance.dialogueEvents.onGatherClue += gatherClue;
		myCanvas.gameObject.SetActive(false);
	}

	//When an NPC is disabled, the functions are removed as events.
	private void OnDisable()
	{
		if(GameEventsManager.instance != null && GameEventsManager.instance.dialogueEvents != null)
		{
			Debug.Log("Hostility disable test" + gameObject.tag);
			GameEventsManager.instance.dialogueEvents.onStartHostility -= onHostility;
			GameEventsManager.instance.dialogueEvents.onStopHostility -= offHostility;	
			//GameEventsManager.instance.dialogueEvents.onGatherClue -= gatherClue;		
		}


	}

    //Update is being used to:
	//- Check if enemy is in knockback (not implemented)
	//- Check if enemy is hostile, and if they are, attack the player.
	//- Set state to idle or dialogue accordingly
	//- Refresh the Chase state during pursuit
    void Update()
    {
		if(enemyState != EnemyState.Knockback)
		{
			//If enemy is hostile, they pursue and attack.
			//They can be turned back by way of dialogue manager (or other classes)
			//by using .GetComponent<NPCManager>().switchHostility().
			if(isHostile == true)
			{
				enemyAttack();
				if(attackCooldownTimer > 0)
				{
					attackCooldownTimer -= Time.deltaTime;
				}
			}
			else
			{
				changeState(EnemyState.Idle); //Change to idle if not hostile
				rb.linearVelocity = Vector2.zero; //kill billiards effect

				if(enemyState == EnemyState.Dialogue)
				{
					//changeState(EnemyState.Idle);
					//isHostile = false;
					rb.linearVelocity = Vector2.zero;
					metPlayer = true;
				}
			}

			if(enemyState == EnemyState.Chasing)
			{
				chase();
			}
			else if(enemyState == EnemyState.Attacking)
			{
				rb.linearVelocity = Vector2.zero;
			}
		}
    }

	//Quality of life functions for changing hostility.
	//Swap hostility
	public void switchHostility()
	{
		this.isHostile = !isHostile;
	}
	//Turn off hostility
	public void offHostility()
	{
		Debug.Log("Hostility off test.");
		this.isHostile = false;
	}
	//Turn on hostility
	public void onHostility()
	{
		Debug.Log("Hostility on test.");
		this.isHostile = true;
	}
	//Set hostility
	public void setHostility(bool activate)
	{
		this.isHostile = activate;
	}

	//Spawns loot at enemy location
	public void DropLoot()
    {
        Instantiate(lootPrefab, transform.position, Quaternion.identity);
    }

	//Function to add NPC tag to the clue list in DialogueManager
	//Subscribe this as an event, then bind it through InkExternalFunctions
	public void gatherClue()
	{
		if(dialogueManager != null)
		{
			Debug.Log("Clue gather test" + dialogueManager.tag);
			dialogueManager.addClue(gameObject.tag);
		}
	}

	//Function to check the clue count
	//If it has passed the threshold set in dialogue manager, this returns true.
	//If it is true, then the Axman should move into different dialogue.
	public bool checkClues()
	{
		if(dialogueManager != null)
		{
			Debug.Log("Clue check test" + dialogueManager.tag);
			return dialogueManager.checkClues();
		}
		return false;
	}

	//This should check the player inventory for the Owl's Wing and/or the Can of Tuna
	public bool checkCollectibles()
	{
		if(inventoryManager != null)
		{
			Debug.Log("Item check test" + inventoryManager.tag);
			return inventoryManager.checkCollectibles();
		}
		return false;
	}




	//Chase handles both direction of the NPC and the flipping their animation (for now).
	void chase()
	{
		//NOTE:  THIS IS FOR ENEMIES THAT FACE RIGHT.
		//facingDirection NEEDS TO BE SIGN CHANGED FOR THOSE THAT FACE LEFT.
		//Or a better solution might be to manage the sprite itself.
		if(facingRight == true)
		{
			if(player.position.x > transform.position.x && facingDirection == 1 ||
				player.position.x < transform.position.x && facingDirection == -1)
			{
				flip();
			}
		}
		else
		{
			if(player.position.x > transform.position.x && facingDirection == -1 ||
				player.position.x < transform.position.x && facingDirection == 1)
			{
				flip();
			}
		}
		Vector2 direction = (player.position - transform.position).normalized;
		rb.linearVelocity = direction * speed;
		//rb.linearVelocity = new Vector2(0f, 10f);
		//transform.position += direction * speed * Time.deltaTime;
	}

	//Flips the enemy animation.
	void flip()
	{
		facingDirection *= -1;
		transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
	}

	//Check if the player is within detection range.
	public void enemyAttack()
	{
		//isHostile = true;
		//Detect if there are collsions and put them in the hits array.
		//Only checks the player's layer, so won't detect other objects.
		Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);
	
		//If there are collisions
		if(hits.Length > 0)
		{
			player = hits[0].transform;

			//if player is in attack range AND cooldown is ready
			if(Vector2.Distance(transform.position, player.position) <= attackRange && attackCooldownTimer <= 0)
			{
				attackCooldownTimer = attackCooldown;
				changeState(EnemyState.Attacking);
			}
			else if(Vector2.Distance(transform.position, player.position) > attackRange && enemyState != EnemyState.Attacking)
			{
				changeState(EnemyState.Chasing);
			}
		}
		else
		{
			changeState(EnemyState.Idle);
			//After changing state to idle, kill momentum so as to prevent billiards effect
			rb.linearVelocity = Vector2.zero;
		}
	}

	//Change the animations from idle to chasing to attacking.
	//To change out of attack animation back to Idle,
	//an event has been attached to the attack animation itself on the final frame.
	public void changeState(EnemyState newState)
	{
		//Exit the current animation.
		if(enemyState == EnemyState.Idle || enemyState == EnemyState.Dialogue)
			anim.SetBool("isIdle", false);
		else if (enemyState == EnemyState.Chasing)
			anim.SetBool("isChasing", false);
		else if (enemyState == EnemyState.Attacking)
			anim.SetBool("isAttacking", false);

		//Update current state
		enemyState = newState;
		//Debug.Log("State is " + enemyState);

		//Update the new animation
		if(enemyState == EnemyState.Idle || enemyState == EnemyState.Dialogue)
		{
			anim.SetBool("isIdle", true);
		}
		else if (enemyState == EnemyState.Chasing)
		{
			anim.SetBool("isChasing", true);
		}
		else if (enemyState == EnemyState.Attacking)
		{
			anim.SetBool("isAttacking", true);
		}
	}

	//This actually displays the dialogue box!
	//Now to stop the enemy from attacking at the same time...
	public void displayDialogueBox()
	{
		Debug.Log("myCanvas is: " + myCanvas.tag);
		if(myCanvas.gameObject.activeSelf == true)
		{
			//myCanvas.gameObject.SetActive(false);
			changeState(EnemyState.Idle);
		}
		else
		{
			//Debug.Log("Test.");
			//myCanvas.gameObject.SetActive(true);
			changeState(EnemyState.Dialogue);
		}
	}

	//changeHealth() alters the NPCs health by the passed amount.
    public void changeHealth(int amount)
    {
        currentHealth += amount;
        Debug.Log("Health " + currentHealth);

        if (currentHealth <= 0)
        {
            //This is for updating the killList on the player object:
            dialogueManager.addKill(gameObject.tag);

            // Mark this NPC as dead
            deadNPCs[gameObject.tag] = true;

            //System for making enemies affect terrain on death:
            if (gameObject.CompareTag("TheWolf"))
            {
                Debug.Log("TheWolf has been killed.");

                // Find door with tag "L2"
                GameObject door = GameObject.FindWithTag("L2");

                // Check if the door is found
                if (door != null)
                {
                    Debug.Log("Found L2 door.");
                    var lockedPassage = door.GetComponent<LockedPassage>();
                    if (lockedPassage != null)
                    {
                        lockedPassage.revealPassageway();
                    }
                    else
                    {
                        Debug.LogWarning("LockedPassage component is missing from the door.");
                    }
                }
                else
                {
                    Debug.LogWarning("L2 door not found.");
                }
            }

            // Completely destroy the NPC and all its components
            Destroy(gameObject);
            Destroy(this);
            return;
        }
        else
        {
            //if you want to re-enable them when gaining health back, add this, or similar logic.
            if (enemySR != null && !enemySR.enabled)
            {
                enemySR.enabled = true;
            }
        }
    }

	//Visualize the range of player detection
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
		//Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}
}

//Set up an enum to handle the different enemy animation states.
public enum EnemyState
{
	Idle,
	Chasing,
	Attacking,
	Knockback,
	Dialogue,
}