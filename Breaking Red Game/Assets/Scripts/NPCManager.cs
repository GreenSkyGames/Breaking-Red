using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;


/*
public class NPC
{
	public virtual bool nonHostile
	{
		return false;
	}

}
*/


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

	public bool isHostile;
	public bool facingRight;

    public SpriteRenderer enemySR;
	public int currentHealth = 9;

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

		//myCanvas = temp.gameObject.GetComponent<Canvas>();

		Debug.Log("myCanvas tag is " + myCanvas.tag);
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
		//Debug.Log("Hostility enable test");
		GameEventsManager.instance.dialogueEvents.onStartHostility += onHostility;
		GameEventsManager.instance.dialogueEvents.onStopHostility += offHostility;
		myCanvas.gameObject.SetActive(false);
	}

	//When an NPC is disabled, the functions are removed as events.
	private void OnDisable()
	{
		if(GameEventsManager.instance != null && GameEventsManager.instance.dialogueEvents != null)
		{
			GameEventsManager.instance.dialogueEvents.onStartHostility -= onHostility;
			GameEventsManager.instance.dialogueEvents.onStopHostility -= offHostility;			
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
		isHostile = !isHostile;
	}
	//Turn off hostility
	public void offHostility()
	{
		isHostile = false;
	}
	//Turn on hostility
	public void onHostility()
	{
		isHostile = true;
	}
	//Set hostility
	public void setHostility(bool activate)
	{
		isHostile = activate;
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
            // Disable rendering and movement, but keep the object active
            if (enemySR != null)
            {
                enemySR.enabled = false;
            }
			this.gameObject.SetActive(false);

            // Optionally, you might want to add other game over logic here,
            // such as displaying a game over screen, triggering events, etc.
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