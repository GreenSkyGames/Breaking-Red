/*Name: Alex Senst
 * Role: Team Lead 2+ -- Software Architect
 * 
 * This file contains the code for the PlayerController class
 * The class handles the basic player movements and interactions with NPC's
 * It also contains a dramatic death sequence if the player falls off the map
 * It inherets from MonoBehaviour */
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

    // Attack variables
    public Transform attackPoint; // Where the attack hitbox will be
    public float attackRange = 0.5f;
    public LayerMask attackLayer; // What layers can be attacked

    /* The update function is called once per frame
     * It checks if the player interacts with an NPC and reacts if the player has
     * It interacts with the displayDialogueBox() function and the triggerDialogue() function */
    private void Update()
    {
        if (Input.GetButtonDown("PlayerActivate")) //Set to F
        {
			//Debug.Log("test");
            //This checks if there is an enemy in range of the ActivatePoint gameobject that is bound to the player.
            Collider2D[] enemies = Physics2D.OverlapCircleAll(ActivatePoint.position, activateRange, enemyLayer);
			//Debug.Log("Button test");
            //If it finds enemies, they're on the list.  If there is an enemy, it displays its dialogue box.
            if (enemies.Length > 0)
            {
                enemies[0].GetComponent<NPCManager>().displayDialogueBox();
                enemies[0].GetComponent<NPCDialogueTrigger>().triggerDialogue();
				//Debug.Log("NPC tag is: " + enemies[0].tag);
            }
        }

        // Attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    /*Fixed update updates at intervals
     * This function checks if the player is currently falling and slows their velocity and stops their collider box as well as their sprite-renderer from affecting other objects
     * It also flips the direction of the player if they begin moving in the opposite direction
     * Additionally it plays footsteps if the player is moving
     * It accomplishes all this by using the flip function to change its direction and the play and stop functions from the AudioManager to play or stop playing the music
     */
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
            flip();
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

    void Attack()
    {
        // Play attack animation
        anim.SetTrigger("Attack");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayer);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            //Add damage here.
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    /*Handles the actual flipping of the player
     * Changes the direction the player is turned and reverses their x local scale value 
     * This function is called by the FixedUpdate() function to flip the player */
    void flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    /* Calls the scaleObject CoRoutine
     * This is used by the game to react when a player steps over an edge*/
    public void scales()
    {
        StartCoroutine(scaleObj());
    }
    /* This function actually scales the object size down
     * This function slowly transforms the object by making it get smaller and smaller until it disappears using math's Lerp function
     * It also quit's the application after this happens as a natural quit to the game*/
    IEnumerator scaleObj() {
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
