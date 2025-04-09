using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    // Array to store 7 images (Make sure the images are imported and dragged to the Inspector)
    public Sprite[] sceneImages;

    // UI Image component used to display the image
    public Image sceneImage;

    // CanvasGroup component for controlling transparency
    private CanvasGroup canvasGroup;

    // Reference to PlayerController to get the player's kill count
    private PlayerController playerController;

    void Start()
    {
        // Get the PlayerController component
        playerController = FindObjectOfType<PlayerController>();
        // Debug.Log("Player's Kill List Count: " + playerController.killList.Count);

        // Initialize CanvasGroup to control opacity
        canvasGroup = sceneImage.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = sceneImage.gameObject.AddComponent<CanvasGroup>();  // Add CanvasGroup if not already attached
        }

        // Initially hide the scene image
        sceneImage.gameObject.SetActive(false);
    }

    // This method updates the scene image based on the number of NPCs killed by the player
    public void UpdateSceneImage()
    {
        // Get the number of NPCs killed by the player
        int npcKillCount = DialogueManager.Instance.killList.Count;
        Debug.Log("Killed NPC count: " + npcKillCount);  // Debug log to check if kill count is increasing

        // Choose a different image based on the kill count
        if (npcKillCount >= 0 && npcKillCount < sceneImages.Length)
        {
            sceneImage.sprite = sceneImages[npcKillCount];
        }
        else
        {
            // If the kill count is greater than 6, show the last image
            sceneImage.sprite = sceneImages[sceneImages.Length - 1];
        }

        // Show the image with smooth fade-in
        ShowImageWithFade();

        StartCoroutine(AudioManager.instance.FadeIn("KillSound", 1.0f));

        StartCoroutine(HideImageAfterDelay(4f));
    }

    // Display the scene image with fade-in effect
    private void ShowImageWithFade()
    {
        sceneImage.gameObject.SetActive(true);
        StartCoroutine(FadeInImage());
    }

    // Coroutine to gradually increase image opacity (fade-in effect)
    private IEnumerator FadeInImage()
    {
        float time = 0f;
        float fadeDuration = 1f; // Duration of the fade-in effect

        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, time / fadeDuration);  // Gradually increase alpha from 0 to 1
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f;  // Ensure fully visible at the end
    }

    // Hide the image after the specified delay (3 seconds)
    private IEnumerator HideImageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the specified time

        // Hide the scene image with fade-out effect
        StartCoroutine(FadeOutImage());
    }

    // Coroutine to gradually decrease image opacity (fade-out effect)
    private IEnumerator FadeOutImage()
    {
        float time = 0f;
        float fadeDuration = 1f; // Duration of the fade-out effect

        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);  // Gradually decrease alpha from 1 to 0
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;  // Ensure fully hidden at the end
        sceneImage.gameObject.SetActive(false);  // Hide the image after fading out
    }
}
