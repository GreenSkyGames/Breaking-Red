using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // For scene loading
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public Sprite[] sceneImages;    // Images for different kill counts
    public Image sceneImage;        // UI image component

    private CanvasGroup canvasGroup;  // For fading effects

    void Start()
    {
        // Get or add CanvasGroup
        canvasGroup = sceneImage.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = sceneImage.gameObject.AddComponent<CanvasGroup>();
        }

        sceneImage.gameObject.SetActive(false);  // Hide initially
    }

    public void UpdateSceneImage()
    {
        StartCoroutine(HandleSceneTransition());
    }

    private IEnumerator HandleSceneTransition()
    {
        // Get the current kill count
        int npcKillCount = DialogueManager.Instance.killList.Count;
        Debug.Log("Killed NPC count: " + npcKillCount);

        // Step 1: Choose and play different sounds based on kill count
        if (npcKillCount < 7)
        {
            yield return StartCoroutine(AudioManager.instance.FadeIn("KillSound", 1.0f));
        }
        else
        {
            yield return StartCoroutine(AudioManager.instance.FadeIn("MadSound", 1.0f));
        }

        // Step 2: Set the appropriate scene image
        if (npcKillCount >= 0 && npcKillCount < sceneImages.Length)
        {
            sceneImage.sprite = sceneImages[npcKillCount];
        }
        else
        {
            sceneImage.sprite = sceneImages[sceneImages.Length - 1];
        }

        // Step 3: Fade in the image
        sceneImage.gameObject.SetActive(true);
        yield return StartCoroutine(FadeInImage());

        // Step 4: Wait for a moment
        yield return new WaitForSeconds(2f);

        // Step 5: Fade out the image
        yield return StartCoroutine(FadeOutImage());

        // Step 6: Decide whether to continue the game or go to GameOver
        if (npcKillCount >= 7)
        {
            Debug.Log("Player became mad!");
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            Debug.Log("Player continues the game.");
        }
    }

    // Fade in the image
    private IEnumerator FadeInImage()
    {
        float time = 0f;
        float fadeDuration = 1f;

        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    // Fade out the image
    private IEnumerator FadeOutImage()
    {
        float time = 0f;
        float fadeDuration = 1f;

        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        sceneImage.gameObject.SetActive(false);
    }
}
