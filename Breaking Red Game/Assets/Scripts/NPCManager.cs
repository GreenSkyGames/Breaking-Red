using UnityEngine;

public class NPCManager : MonoBehaviour
{
	public float speed = 2;
	public float attackRange = 1;
	public float attackCooldown = 1;
	public float playerDetectRange = 5;
	public Transform DetectionPoint;
	//public Transform attackPoint;
	public LayerMask playerLayer;
	//private bool isChasing;

	public Canvas myCanvas;

	private float attackCooldownTimer;
	private int facingDirection = -1;
	private EnemyState enemyState;

	private Rigidbody2D rb;
	private Transform player;
	private Animator anim;

	public bool isHostile;

	
    //Start is currently being used to:
	// - find rigidbody component
	// - find animator component
	// - change default animation state to idle
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		ChangeState(EnemyState.Idle);
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
				ChangeState(EnemyState.Idle); //Change to idle if not hostile
				rb.linearVelocity = Vector2.zero; //kill billiards effect

				if(enemyState == EnemyState.Dialogue)
				{
					//ChangeState(EnemyState.Idle);
					//isHostile = false;
					rb.linearVelocity = Vector2.zero;
				}
			}

			if(enemyState == EnemyState.Chasing)
			{
				Chase();
			}
			else if(enemyState == EnemyState.Attacking)
			{
				rb.linearVelocity = Vector2.zero;
			}
		}
    }

	//Quality of life functions for changing hostility.
	public void switchHostility()
	{
		isHostile = !isHostile;
	}
	public void offHostility()
	{
		isHostile = false;
	}
	public void onHostility()
	{
		isHostile = true;
	}

	//Chase handles both direction of the NPC and the flipping their animation (for now).
	void Chase()
	{
		//NOTE:  THIS IS FOR ENEMIES THAT FACE RIGHT.
		//facingDirection NEEDS TO BE SIGN CHANGED FOR THOSE THAT FACE LEFT.
		//Or a better solution might be to manage the sprite itself.
		if(player.position.x > transform.position.x && facingDirection == 1 ||
			player.position.x < transform.position.x && facingDirection == -1)
		{
			Flip();
		}
		Vector2 direction = (player.position - transform.position).normalized;
		rb.linearVelocity = direction * speed;
		//rb.linearVelocity = new Vector2(0f, 10f);
		//transform.position += direction * speed * Time.deltaTime;
	}

	//Flips the enemy animation.
	void Flip()
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
		Collider2D[] hits = Physics2D.OverlapCircleAll(DetectionPoint.position, playerDetectRange, playerLayer);
	
		//If there are collisions
		if(hits.Length > 0)
		{
			player = hits[0].transform;

			//if player is in attack range AND cooldown is ready
			if(Vector2.Distance(transform.position, player.position) <= attackRange && attackCooldownTimer <= 0)
			{
				attackCooldownTimer = attackCooldown;
				ChangeState(EnemyState.Attacking);
			}
			else if(Vector2.Distance(transform.position, player.position) > attackRange && enemyState != EnemyState.Attacking)
			{
				ChangeState(EnemyState.Chasing);
			}
		}
		else
		{
			ChangeState(EnemyState.Idle);
			//After changing state to idle, kill momentum so as to prevent billiards effect
			rb.linearVelocity = Vector2.zero;
		}
	}

	//Change the animations from idle to chasing to attacking.
	//To change out of attack animation back to Idle,
	//an event has been attached to the attack animation itself on the final frame.
	public void ChangeState(EnemyState newState)
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
		if(myCanvas.gameObject.activeSelf == true)
		{
			myCanvas.gameObject.SetActive(false);
			ChangeState(EnemyState.Idle);
		}
		else
		{
			//Debug.Log("Test.");
			myCanvas.gameObject.SetActive(true);
			ChangeState(EnemyState.Dialogue);
		}
	}

	//Visualize the range of player detection
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(DetectionPoint.position, playerDetectRange);
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