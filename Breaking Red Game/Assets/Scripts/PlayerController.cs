using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    public int facingDirection = 1;
    public Rigidbody2D rb;
    public Animator anim;

	//These functions serve to display the dialogue box.
	public float activateRange = 1;
	public LayerMask enemyLayer;
	public Transform ActivatePoint;

    public float health = 100.0f;

	private void Update()
	{
		if(Input.GetButtonDown("PlayerActivate")) //This is set to the "f" key currently
		{
			//This checks if there is an enemy in range of the ActivatePoint gameobject that is bound to the player.
			Collider2D[] enemies = Physics2D.OverlapCircleAll(ActivatePoint.position, activateRange, enemyLayer);
			//If it finds enemies, they're on the list.  If there is an enemy, it displays its dialogue box.
			if(enemies.Length > 0)
			{
				enemies[0].GetComponent<NPCManager>().displayDialogueBox();
			}
		}
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(horizontal > 0 && transform.localScale.x < 0 ||
            horizontal < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        anim.SetFloat("horizontal", Mathf.Abs(horizontal));
        anim.SetFloat("vertical", Mathf.Abs(vertical));

        rb.linearVelocity = new Vector2(horizontal, vertical) * _speed;
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
