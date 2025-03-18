using System.Collections;
using System.Collections.Generic;
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

            Vector2 newPosition = GetDest();
            if(newPosition != Vector2.zero)
            {
                List<string> BGaudioNames = new List<string> { "CabinBGM", "L1BGM", "L2BGM", "L3BGM", "L4BGM", "L5BGM", "WolfSound", "BatSound" };
                StartCoroutine(HandleMusicTransition(BGaudioNames));

                // Log the tag to check the value
                Debug.Log("Current tag: " + gameObject.tag); // Print the tag of the current door
                
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero;
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                }
                //other.transform.position = newPosition;
                StartCoroutine(TeleportWithFade(other, newPosition, rb));
            }
        }
    }
    private IEnumerator TeleportWithFade(Collider2D player, Vector2 newPosition, Rigidbody2D rb)
    {
        yield return StartCoroutine(FadeToBlack(0.5f)); // Fade out
        player.transform.position = newPosition; // Move player
        yield return new WaitForSeconds(0.3f); // Small delay at black screen
        yield return StartCoroutine(FadeFromBlack(0.5f)); // Fade back in

        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private IEnumerator FadeToBlack(float duration)
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

    private IEnumerator FadeFromBlack(float duration)
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
    private Vector2 GetDest()
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

    private IEnumerator HandleMusicTransition(List<string> BGaudioNames)
    {
        // Pause all music gradually
        yield return StartCoroutine(AudioManager.instance.PauseAllAudioSources());

        // Check the tag of the current door to change BGmusic
        if (gameObject.CompareTag("IL1.1"))
        {
            StartCoroutine(AudioManager.instance.FadeIn("CabinBGM", 1.5f, 0.05f));  // Fade in CabinBGM
        }
        else if (gameObject.CompareTag("IL1"))
        {
            StartCoroutine(AudioManager.instance.FadeIn("L1BGM", 1.5f, 0.1f));  // Fade in L1BGM
            StartCoroutine(AudioManager.instance.FadeIn("WolfSound", 1.5f, 0.1f)); // Fade in WolfSound
        }
        else if (gameObject.CompareTag("L2"))
        {
            StartCoroutine(AudioManager.instance.FadeIn("L2BGM", 1.5f, 1.5f));  // Fade in L2BGM
            StartCoroutine(AudioManager.instance.FadeIn("BatSound", 1.5f, 1.0f)); // Fade in BatSound
        }
        else if (gameObject.CompareTag("L3"))
        {
            StartCoroutine(AudioManager.instance.FadeIn("L3BGM", 1.5f, 1.0f));  // Fade in L3BGM
        }
        else if (gameObject.CompareTag("L4"))
        {
            StartCoroutine(AudioManager.instance.FadeIn("L4BGM", 1.5f, 1.0f));  // Fade in L4BGM
        }
        else if (gameObject.CompareTag("L5"))
        {
            StartCoroutine(AudioManager.instance.FadeIn("L5BGM", 1.5f, 1.0f));  // Fade in L5BGM
        }
    }
}

