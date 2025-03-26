using System.Collections;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Passageways : MonoBehaviour
{
    public CanvasGroup fadePanel;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger!");
        if(other.CompareTag("Player"))
        {
            AudioManager.instance.Play("DoorSound"); // Play the open door sound

            Vector2 newPosition = getDest();
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
    private Vector2 getDest()
    {
        switch(gameObject.tag)
        {
            case "L1":
                SceneManager.LoadScene("Level 1");
                return new Vector2(-19.7f, -21.2f);
            case "IL1":
                return new Vector2(-1, 0);
            case "IL1.1":
                return new Vector2(-83.5f, -17.8f);
            case "L2":
                SceneManager.LoadScene("Level 2");
                return new Vector2(-19.7f, -21.2f);
            case "L3":
                SceneManager.LoadScene("Level 3");
                return new Vector2(-19.7f, -21.2f);
            case "L4":
                SceneManager.LoadScene("Level 4");
                return new Vector2(-19.7f, -21.2f);
            case "L5":
                SceneManager.LoadScene("Level 5");

                return new Vector2(-19.7f, -21.2f);
            default:
                Debug.LogWarning("No destination set for tag: " + gameObject.tag);
                return Vector2.zero;
        }
    }
}
