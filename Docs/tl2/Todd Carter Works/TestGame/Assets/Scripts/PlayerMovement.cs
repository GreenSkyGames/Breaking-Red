using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float speed = 5;
	public int facingDirection = 1;
	public Rigidbody2D rb;
	public Animator anim;



    // Update is called once per frame
	//FixedUpdate runs 50 times per second, which makes it more reliable for physics
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

		if(horizontal > 0 && transform.localScale.x < 0 || 
			horizontal < 0 && transform.localScale.x > 0){
			Flip();
		}

		anim.SetFloat("horizontal", Mathf.Abs(horizontal));
		anim.SetFloat("vertical", Mathf.Abs(vertical));
		
		rb.linearVelocity = new Vector2(horizontal, vertical) * speed;
    }

	void Flip()
	{
		facingDirection *= -1;
		transform.localScale = new Vector3 (transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
	}
}
