using UnityEditor.Tilemaps;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    public int facingDirection = 1;
    public Rigidbody2D rb;
    public Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
