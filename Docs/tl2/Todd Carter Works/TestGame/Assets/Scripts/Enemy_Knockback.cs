using UnityEngine;
using System.Collections;

public class Enemy_Knockback : MonoBehaviour
{
	private Rigidbody2D rb;
	private Enemy_Movement enemyMovement;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		enemyMovement = GetComponent<Enemy_Movement>();
	}

    public void Knockback(Transform playerTransform, float knockbackForce, float stunTime)
	{
		enemyMovement.ChangeState(EnemyState.Knockback);
		StartCoroutine(StunTimer(stunTime));
		Vector2 direction = (transform.position - playerTransform.position).normalized;
		rb.linearVelocity = direction * knockbackForce;
		//Vector3 direction = (transform.position - playerTransform.position).normalized;
		//transform.position += direction * knockbackForce * Time.deltaTime;
		Debug.Log("knockback applied");
	}

	IEnumerator StunTimer(float stunTime)
	{
		yield return new WaitForSeconds(stunTime);
		//rb.linearVelocity = Vector2.zero;
		enemyMovement.ChangeState(EnemyState.Idle);
	}
}
