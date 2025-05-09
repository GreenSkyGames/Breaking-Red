/*Name: Alex Senst
 * Role: Team Lead 2+ -- Software Architect
 * 
 * This file contains the code for the PlayerController class
 * The class handles the basic player movements and interactions with NPC's
 * It also contains a dramatic death sequence if the player falls off the map
 * It inherets from MonoBehaviour */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; //mobile controls 

public class PlayerController : MonoBehaviour
{
    public CanvasGroup fadePanel;
    public float speed = 5.0f;
    public float scaleSpeed = 2.0f;
    public float seedHeight = 1.0f;
    public float shrunkHeight = 0f;
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
    public int attackDamage = 1;

	public List<string> killList = new List<string>();

    /* The update function is called once per frame
     * It checks if the player interacts with an NPC and reacts if the player has
     * It interacts with the displayDialogueBox() function and the triggerDialogue() function */
    private void Update()
    {
        if (Input.GetButtonDown("PlayerActivate") || (Gamepad.current != null && Gamepad.current.selectButton.wasPressedThisFrame)) //Set to F
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
        if (Input.GetKeyDown(KeyCode.Space) || (Gamepad.current != null && Gamepad.current.buttonWest.wasPressedThisFrame))
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

        //mobile controls 
        if(Gamepad.current != null){
            //player movement tied to joystick 
            if (Gamepad.current != null)
            {
                Vector2 inputVector = Gamepad.current.leftStick.ReadValue();
                if (inputVector != Vector2.zero)
                {
                    horizontal = inputVector.x;
                    vertical = inputVector.y;
                }
            }
        }

        if (horizontal > 0 && transform.localScale.x < 0 ||
            horizontal < 0 && transform.localScale.x > 0)
        {
            flip();
        }

        anim.SetFloat("horizontal", Mathf.Abs(horizontal));
        anim.SetFloat("vertical", Mathf.Abs(vertical));

        rb.linearVelocity = new Vector2(horizontal, vertical) * speed;

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

    private bool isOnPlatform = false;  // Flag to check if the player is on a platform
    private bool isOnBoundary = false;  // Flag to check if the player is on a boundary
    private bool justTeleported = false;
    private bool grounded = true;
    // When the player exits the platform's trigger area

    // When player enters the platform's area (trigger zone)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MovingPlatform") || other.CompareTag("BCBridge"))
        {
            Debug.Log("Platforms and bridges on");
            isOnPlatform = true;
        }
        else if (other.CompareTag("IL2") || other.CompareTag("L2.1") || other.CompareTag("L2.2") || other.CompareTag("L4.1") || other.CompareTag("L4.2") || other.CompareTag("L3.1") || other.CompareTag("L3") || other.CompareTag("IL3") || other.CompareTag("L4") || other.CompareTag("L5") )
        {
            isOnBoundary = false;
            isOnPlatform = true;
            justTeleported = true;
            StartCoroutine(ResetTeleportFlag());
        }
        else if (other.CompareTag("Ground"))
        {
            grounded = true;
        }
        else if (other.CompareTag("Boundary"))
        {
            Debug.Log("on platform: " + isOnPlatform + " teleported: " + justTeleported + " grounded: " + grounded);
            grounded = false;
            isOnBoundary = true;
            if (!isOnPlatform && !justTeleported && !grounded)
            {
                scales();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            Debug.Log("Off the platform");
            isOnPlatform = false;
            // Check if player is now on the boundary layer after stepping off the platform
            /*if (isOnBoundary && !justTeleported && !grounded)
            {
                Debug.Log("onBoundary: " + isOnBoundary + " teleported: " + justTeleported + " grounded: " + grounded + " platform: " + isOnPlatform);
                scales();
            }*/
        }
        if(other.CompareTag("Ground")){

            grounded = false;
            Debug.Log("Grounded " + grounded);
        }
        else if (other.CompareTag("Boundary"))
        {
            isOnBoundary = false;  // Ensure the player isn't marked on the boundary if they leave
        }
    }
    
    private IEnumerator ResetTeleportFlag()
    {
        yield return new WaitForSeconds(2f);
        justTeleported = false;
    }



    void Attack()
    {
        // Play attack animation
        anim.SetTrigger("Attack");

        AudioManager.instance.Play("AttackSound"); // play attacking sound effect

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayer);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);

            //This is for calling enemy damage:
			enemy.GetComponent<NPCManager>().changeHealth(-attackDamage);
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

    /*private IEnumerator fadeToBlack(float duration)
    {
        if (fadePanel == null) yield break;

        float elapsed = 0;
        while (elapsed < duration)
        {
            fadePanel.alpha = Mathf.Lerp(0, 1, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        fadePanel.alpha = 1;
        SceneManager.LoadScene("GameOver");
    }*/

    IEnumerator scaleObj()
    {
        if (BCMODE.Instance.IsBCModeActive())
        {
            Debug.Log("BC Mode Is On So Scaling Is Off");
        }
        else
        {
            Debug.Log("BC Mode: " + BCMODE.Instance.IsBCModeActive());
            isScaling = true;
            float elapsedTime = 0f;
            float duration = scaleSpeed;
            Debug.Log("isScaling: " + isScaling);

            Vector3 startScale = transform.localScale;
            float direction = Mathf.Sign(startScale.x); // +1 or -1 depending on facing
            Vector3 targetScale = new Vector3(shrunkHeight * direction, shrunkHeight, startScale.z);

            while (elapsedTime < duration)
            {
                StartCoroutine(AudioManager.instance.PauseAllAudioSources());

                // play game over sound effect
                AudioManager.instance.Play("GameoverSound");

                //float t = elapsedTime / duration; // Normalized time (0 to 1)
                //float newScale = Mathf.Lerp(seedHeight, finalHeight, t);
                float t = elapsedTime / duration;
                t = 1f - Mathf.Pow(1f - t, 3); // ease-out cubic
                //float newScale = Mathf.Lerp(Mathf.Abs(startScale), shrunkHeight, t);
                //transform.localScale = new Vector3(newScale, newScale, 1);
                transform.localScale = Vector3.Lerp(startScale, targetScale, t);


                elapsedTime += Time.deltaTime; // Increment elapsed time
                //Debug.Log("elapsed time: " + elapsedTime + " duration: " + duration);
                yield return null; // Wait for the next frame
            }



            //yield return StartCoroutine(fadeToBlack(0.5f)); // Fade out
            Debug.Log("Player died moving over an edge!");
            // SceneManager.LoadScene("GameOver");
            SceneManager.LoadScene("GameOver");
            //Application.Quit();//quitting the game 
            //UnityEditor.EditorApplication.isPlaying = false;
            //float newScale = Mathf.Lerp(_seedHeight, _finalHeight, Time.deltaTime / _scaleSpeed);
            //transform.localScale = new Vector3(newScale, newScale, 1);
        }
    }
	//Function to add enemy tag to the clue list
	public void addKill(string killTag)
	{
		if (!killList.Contains(killTag))
		{
			killList.Add(killTag);
			Debug.Log("All kills: " + string.Join(", ", killList));
        }
	}

    public void removeKill()
    {
        if (killList.Count > 0)
        {
            string lastKill = killList[killList.Count - 1];
            killList.RemoveAt(killList.Count - 1);
            Debug.Log("Removed last kill: " + lastKill);
            Debug.Log("Remaining kills: " + string.Join(", ", killList));
        }
        else
        {
            Debug.Log("Kill list is empty — nothing to remove.");
        }
    }
}
