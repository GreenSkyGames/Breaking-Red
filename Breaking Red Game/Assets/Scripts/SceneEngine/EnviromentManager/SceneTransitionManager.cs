using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    // Array to store 7 images (Make sure the images are imported and dragged to the Inspector)
    public Sprite[] sceneImages;

    // UI Image component used to display the image
    public Image sceneImage;

    // Reference to PlayerController to get the player's kill count
    private PlayerController playerController;

    void Start()
    {
        // Get the PlayerController component
        playerController = FindObjectOfType<PlayerController>();
    }

    // This method updates the scene image based on the number of NPCs killed by the player
    public void UpdateSceneImage()
    {
        // Get the number of NPCs killed by the player
        int npcKillCount = playerController.killList.Count;

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

        // Start a coroutine to hide the image after 3 seconds
        StartCoroutine(HideImageAfterDelay(3f));
    }

    // Coroutine that hides the image after the specified delay and resumes the game
    private IEnumerator HideImageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the specified time

        // Hide the scene image
        sceneImage.gameObject.SetActive(false);

        // Here, you can restore the game state (e.g., allow the player to move again)
        // For example, you can resume paused UI components and re-enable player controls, etc.
    }

    // This method is used to manually show the scene image
    public void ShowImage()
    {
        sceneImage.gameObject.SetActive(true);
    }
}
