using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	public Transform attackPoint;
	public float weaponRange = 1;
	public float knockbackForce = 8;
	public float stunTime = 0.25f;
	public LayerMask enemyLayer;
	public int damage = 1;

    public Animator anim;

	public float cooldown = 1f;
	private float timer; //This var is for cooldown timing between attack animations.

	//Update is currently used for decreasing cooldown timer.
	private void Update()
	{
		if(timer > 0)
		{
			timer -= Time.deltaTime;
		}
	}

	//Enter attack animation.
	public void Attack()
	{
		if(timer <= 0)
		{
			anim.SetBool("isAttacking", true);

			timer = cooldown;
		}
	}

	//Check for collision and deal damage.
	//This is bound to the attack animation as an animation event.
	public void DealDamage()
	{
		Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayer);

		if(enemies.Length > 0)
		{
			enemies[0].GetComponent<Enemy_Health>().ChangeHealth(-damage);
			enemies[0].GetComponent<Enemy_Knockback>().Knockback(transform, knockbackForce, stunTime);
		}
	}
		

	//Exit attack animation.
	public void FinishAttacking()
	{
		anim.SetBool("isAttacking", false);
	}

	//Visualize player attack range.
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
	}
}
