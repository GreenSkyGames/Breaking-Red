using UnityEngine;

/* Name: Todd Carter
 * Role: Team Lead 2 -- Software Architect
 *
 *	This is the script that handles the NPC's offense attacking.
 *	It takes data from the AttackPoint object that is bound to the NPC,
 *	checks if the player is within that space, and applies damage.
 *
*/
public class NPCAttack : MonoBehaviour
{
	public int damage = 1;
	public Transform AttackPoint;
	public float weaponRange = 1;
	public float knockbackForce = 3;
	public float stunTime = 1;
	public LayerMask playerLayer;

	//This function is useful for enemies or objects that deal damage on touch.
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			//collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
		}
	}


	//Check if the player is in attack range by checking the player layer.
	//If something is found, the hits array is > 0, and damage and knockback are dealt.
	public void attack()
	{
		Collider2D[] hits = Physics2D.OverlapCircleAll(AttackPoint.position, weaponRange, playerLayer);

		if(hits.Length > 0)
		{
			hits[0].GetComponent<PlayerHealth>().changeHealth(-damage);
            //hits[0].GetComponent<PlayerController>().Knockback(transform, knockbackForce, stunTime);
            
			// Play attack sound
			AudioManager.instance.Play("AttackSound");
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
