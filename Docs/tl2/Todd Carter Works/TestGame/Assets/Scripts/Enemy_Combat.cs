using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
	public int damage = 1;
	public Transform attackPoint;
	public float weaponRange;
	public float knockbackForce;
	public float stunTime;
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
		Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);

		if(hits.Length > 0)
		{
			hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);
			hits[0].GetComponent<PlayerMovement>().Knockback(transform, knockbackForce, stunTime);
			
		}
	}

	//Visualize attack range of enemy.
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		//Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
		Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
	}
}
