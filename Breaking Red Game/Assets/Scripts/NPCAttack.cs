using UnityEngine;

public class NPCAttack : MonoBehaviour
{
	public int damage = 1;
	public Transform AttackPoint;
	public float weaponRange = 1;
	public float knockbackForce = 3;
	public float stunTime = 1;
	public LayerMask playerLayer;

/*
	//This function is useful for enemies or objects that deal damage on touch.
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
		}
	}
*/

	//Check if the player is in attack range by checking the player layer.
	//If something is found, the hits array is > 0, and damage and knockback are dealt.
	public void Attack()
	{
		Collider2D[] hits = Physics2D.OverlapCircleAll(AttackPoint.position, weaponRange, playerLayer);

		if(hits.Length > 0)
		{
			//hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);
			//hits[0].GetComponent<PlayerController>().Knockback(transform, knockbackForce, stunTime);
			
		}
	}

	//Visualize attack range of enemy.
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		//Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
		Gizmos.DrawWireSphere(AttackPoint.position, weaponRange);
	}
}
