using System.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    public float scaleSpeed = 2.0f;
    public float seedHeight = 1.0f;
    public float finalHeight = 0f;
    public bool isScaling = false;
    public int facingDirection = 1;
    public Rigidbody2D rb;
    public CapsuleCollider2D Collider;
    public SpriteRenderer spriteRenderer;
    public Animator anim;

    //These functions serve to display the dialogue box.
    public float activateRange = 1;
    public LayerMask enemyLayer;
    public Transform ActivatePoint;

    private bool isPlayingFootstep = false;

    private void Update()
    {
        if (Input.GetButtonDown("PlayerActivate")) //This is set to the "f" key currently
        {
            //This checks if there is an enemy in range of the ActivatePoint gameobject that is bound to the player.
            Collider2D[] enemies = Physics2D.OverlapCircleAll(ActivatePoint.position, activateRange, enemyLayer);
            //If it finds enemies, they're on the list.  If there is an enemy, it displays its dialogue box.
            if (enemies.Length > 0)
            {
                enemies[0].GetComponent<NPCManager>().displayDialogueBox();
                enemies[0].GetComponent<NPCDialogueTrigger>().TriggerDialogue();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isScaling)
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, Time.deltaTime * scaleSpeed);
            Collider.enabled = false;
            spriteRenderer.sortingOrder = -10;
            return;
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal > 0 && transform.localScale.x < 0 ||
            horizontal < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        anim.SetFloat("horizontal", Mathf.Abs(horizontal));
        anim.SetFloat("vertical", Mathf.Abs(vertical));

        rb.linearVelocity = new Vector2(horizontal, vertical) * _speed;

        // Check if the player is moving and play footstep sound
        if (horizontal != 0 || vertical != 0)
        {
            if (!isPlayingFootstep) // If the footstep sound is not playing, then play the sound
            {
                AudioManager.instance.Play("FootstepSound");

                isPlayingFootstep = true; // Mark sound is playing
            }
        }
        else
        {
            if (isPlayingFootstep)
            {
                AudioManager.instance.Stop("FootstepSound");
                isPlayingFootstep = false;
            }
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    public void Scales()
    {
        StartCoroutine(ScaleObj());
    }
    IEnumerator ScaleObj() {
        isScaling = true;
        float elapsedTime = 0f;
        float duration = scaleSpeed;
        Debug.Log("isScaling: " + isScaling);
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration; // Normalized time (0 to 1)
            float newScale = Mathf.Lerp(seedHeight, finalHeight, t);
            transform.localScale = new Vector3(newScale, newScale, 1);

            elapsedTime += Time.deltaTime; // Increment elapsed time
            yield return null; // Wait for the next frame
        }
        Debug.Log("Player died moving over an edge!");
        Application.Quit();//quitting the game 
        UnityEditor.EditorApplication.isPlaying = false;
        //float newScale = Mathf.Lerp(_seedHeight, _finalHeight, Time.deltaTime / _scaleSpeed);
        //transform.localScale = new Vector3(newScale, newScale, 1);
    }
}
