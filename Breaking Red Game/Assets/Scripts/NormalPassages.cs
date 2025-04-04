/*Name: Alex Senst
 * Role: Team Lead 2+ -- Software Architect
 * 
 * This file contains the definition for the Passageways Class
 * This class allows transitions between places in the level and between levels
 * It inherets from TerrainObjects
 */
using System.Collections;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NormalPassage : TerrainObjects
{
    public CanvasGroup fadePanel;
    /* This code checks the tag of an object when it collides with a passageway and plays a sound upon impact if it is a player
     * It also chooses the next destination to transport the user to based on the current tag of the passageway and sends that to the getDestination function*/
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            AudioManager.instance.Play("DoorSound"); // Play the open door sound

            Vector2 newPosition = getDestination();
            if(newPosition != Vector2.zero)
            {
                // Log the tag to check the value
                Debug.Log("Current tag: " + gameObject.tag); // Print the tag of the current door
                
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero;
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                }
                //other.transform.position = newPosition;
                StartCoroutine(teleportWithFade(other, newPosition, rb));
            }
        }
    }
    /* This code sets up an Enumerator function that will allow a teleporting effect betweeen locations with a canvas that temporarily makes the player fade to black
     * It begins the Coroutine fadeToBlack for half a second at first, sets a small delay for how long the screen remains black, then returns to normal
     * It also changes the player's position during the time the screen is black to provide smooth movements
     * It utilizes the ChangeBackgroundMUsic function as well to create a specific sound for passageway movement
     * */
    private IEnumerator teleportWithFade(Collider2D player, Vector2 newPosition, Rigidbody2D rb)
    {
        yield return StartCoroutine(fadeToBlack(0.5f)); // Fade out
        BackgroundMusic.instance.ChangeBackgroundMusic(gameObject.tag); // Change BGM
        player.transform.position = newPosition; // Move player
        yield return new WaitForSeconds(0.3f); // Small delay at black screen
        yield return StartCoroutine(fadeFromBlack(0.5f)); // Fade back in
              
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    /* This sets an enumerator to cause the screen to fade to a black panel naturally for a moment in the game
     * It applies the Lerp function from the math library to cause the natural fade out for a specific elapsed time*/
    private IEnumerator fadeToBlack(float duration)
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
    }

    /* This sets an enumerator to cause the screen to return back to normal from a black panel naturally in the game
     * It applies the Lerp function from the math library to cause the natural fade in for a specific elapsed time*/
    private IEnumerator fadeFromBlack(float duration)
    {
        if (fadePanel == null) yield break;

        float elapsed = 0;
        while (elapsed < duration)
        {
            fadePanel.alpha = Mathf.Lerp(1, 0, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        fadePanel.alpha = 0;
    }
    /* This function determines where a player should go based on the tag of the passageway they make contact with
     * It returns a value back to the original OnTriggerEnter2D function from above and properly changes the scenes as necessary for some levels*/
    private Vector2 getDestination()
    {
        switch(gameObject.tag)
        {
            case "L1":
                //SceneManager.LoadScene("Level 1");
                return new Vector2(10.5f, -8.43f);
            case "IL1":
                return new Vector2(-1, -0.5f);
            case "IL1.1":
                return new Vector2(-110, -14.5f);
            case "L2":
                //SceneManager.LoadScene("Level 2");
                //return new Vector2(-19.7f, -21.2f);
                return new Vector2(79.71f, -24.84f);
            case "IL2":
                return new Vector2(181.2f, -14.6f);
            case "L3":
                //SceneManager.LoadScene("Level 3");
                return new Vector2(296.16f, -1.03f); 
                //return new Vector2(-19.7f, -21.2f);
            case "IL3":
                return new Vector2(317.29f, -61.84f);
            case "L4":
                //SceneManager.LoadScene("Level 4");
                //return new Vector2(-19.7f, -21.2f);
                return new Vector2(223.76f, -69.58f);
            case "L5":
                //SceneManager.LoadScene("Level 5");
                //return new Vector2(-19.7f, -21.2f);
                return new Vector2(30.36f, -117.72f);
            default:
                Debug.LogWarning("No destination set for tag: " + gameObject.tag);
                return Vector2.zero;
        }
    }
}
