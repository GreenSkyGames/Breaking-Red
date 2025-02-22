using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
	public float speed;
	public float attackRange = 1;
	public float attackCooldown = 1;
	public float playerDetectRange = 5;
	public Transform detectionPoint;
	//public Transform attackPoint;
	public LayerMask playerLayer;
	//private bool isChasing;

	private float attackCooldownTimer;
	private int facingDirection = -1;
	private EnemyState enemyState;

	private Rigidbody2D rb;
	private Transform player;
	private Animator anim;

	
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
	// - call function to check if player is in range
	// - provide cooldown window for attack animation
	// - call chase animation.
    void Update()
    {
		if(enemyState != EnemyState.Knockback)
		{
			CheckForPlayer();

			if(attackCooldownTimer > 0)
			{
				attackCooldownTimer -= Time.deltaTime;
			}
			if(enemyState == EnemyState.Chasing)
			{
				Chase();
			}
			else if(enemyState == EnemyState.Attacking)
			{
				//Nothing needs to go here, because using a Vector3 for enemy movement doesn't
				//require making them stop.  The use of deltaTime stops them automatically.
			}
		}
    }

	void Chase()
	{
		//NOTE:  THIS IS FOR ENEMIES THAT FACE RIGHT.
		//facingDirection NEEDS TO BE SIGN CHANGED FOR THOSE THAT FACE LEFT.
		if(player.position.x > transform.position.x && facingDirection == 1 ||
			player.position.x < transform.position.x && facingDirection == -1)
		{
			Flip();
		}
		//This is different from the tutorial, as a Vector2 here did not work.
		//Using a vector3 means the enemy automatically stops when the player is outside range.
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
	private void CheckForPlayer()
	{
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
		}
	}

	//Change the animations from idle to chasing to attacking.
	public void ChangeState(EnemyState newState)
	{
		//Exit the current animation.
		if(enemyState == EnemyState.Idle)
			anim.SetBool("isIdle", false);
		else if (enemyState == EnemyState.Chasing)
			anim.SetBool("isChasing", false);
		else if (enemyState == EnemyState.Attacking)
			anim.SetBool("isAttacking", false);

		//Update current state
		enemyState = newState;

		//Update the new animation
		if(enemyState == EnemyState.Idle)
			anim.SetBool("isIdle", true);
		else if (enemyState == EnemyState.Chasing)
			anim.SetBool("isChasing", true);
		else if (enemyState == EnemyState.Attacking)
			anim.SetBool("isAttacking", true);
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
}