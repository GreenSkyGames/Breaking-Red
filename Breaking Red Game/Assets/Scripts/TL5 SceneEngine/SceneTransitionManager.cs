using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // For scene loading
using System.Collections;

/*
 * Name:  Hengyi Tian
 * Role:   TL5 -- AI Specialist
 * This file contains the definition for the SceneTransitionManager class.
 * The SceneTransitionManager class handles scene transitions, including updating scene images, 
 * playing sound effects and managing fade effects.
 */

public class SceneTransitionManager : MonoBehaviour
{
    public Sprite[] sceneImages; // Array of images for different kill counts
    public Image sceneImage; // UI Image component to display the scene image

    private CanvasGroup canvasGroup; // CanvasGroup for controlling fading effects

    void Start()
    {
        // Add CanvasGroup component to handle fade effects
        canvasGroup = sceneImage.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = sceneImage.gameObject.AddComponent<CanvasGroup>(); // Add CanvasGroup if not already present
        }

        sceneImage.gameObject.SetActive(false); // Initially hide the scene image
    }

    // This function is called when update the scene image
    public void UpdateSceneImage()
    {
        StartCoroutine(HandleSceneTransition()); 
    }

    // This function is called when start the scene transition based on the player's kill count
    private IEnumerator HandleSceneTransition()
    {
        // Get the current kill count
        int npcKillCount = DialogueManager.Instance.killList.Count;
        Debug.Log("Killed NPC count: " + npcKillCount);

        // Step 1: Choose and play different sounds based on kill count
        if (npcKillCount < 7)
        {
            yield return StartCoroutine(AudioManager.instance.FadeIn("KillSound", 1.0f)); // Play a sound for < 7 kills
        }
        else
        {
            yield return StartCoroutine(AudioManager.instance.FadeIn("MadSound", 1.0f)); // Play a mad sound for >= 7 kills
        }

        // Step 2: Show the killing scene image based on the kill count
        if (npcKillCount >= 0 && npcKillCount < sceneImages.Length)
        {
            sceneImage.sprite = sceneImages[npcKillCount];
        }

        // Step 3: Fade in the image
        sceneImage.gameObject.SetActive(true); // Make the scene image visible
        yield return StartCoroutine(FadeInImage()); // Start fading in the image

        // Step 4: Wait for 2 seconds before transitioning out
        yield return new WaitForSeconds(2f);

        // Step 5: Fade out the image
        yield return StartCoroutine(FadeOutImage());

        // Step 6: Decide whether to continue the game or go to GameOver scene
        if (npcKillCount >= 7)
        {
            Debug.Log("Player became mad!");
            SceneManager.LoadScene("GameOver"); // Load the GameOver scene
        }
        else
        {
            Debug.Log("Player continues the game.");
        }
    }

    // This function is called when fade in the image over a specified duration
    private IEnumerator FadeInImage()
    {
        float time = 0f;
        float fadeDuration = 1f; // Duration for fade in

        // Gradually increase the alpha value to fade in the image
        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, time / fadeDuration); // Interpolate alpha from 0 to 1
            time += Time.deltaTime; // Increment time
            yield return null; // Wait for the next frame
        }

        canvasGroup.alpha = 1f;
    }

    // This function is called when fade out the image over a specified duration
    private IEnumerator FadeOutImage()
    {
        float time = 0f;
        float fadeDuration = 1f; // Wait for the next frame
        
        // Gradually decrease the alpha value to fade out the image
        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, time / fadeDuration); // Interpolate alpha from 1 to 0
            time += Time.deltaTime; // Increment time
            yield return null; // Wait for the next frame
        }

        canvasGroup.alpha = 0f;
        sceneImage.gameObject.SetActive(false); // Hide the scene image after fade out
    }
}
