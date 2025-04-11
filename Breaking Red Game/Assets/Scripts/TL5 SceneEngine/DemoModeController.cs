using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

/*
 * Name: Hengyi Tian
 * Role: TL5-- AI Specialist
 * 
 * This file contains the definition for the DemoModeController class.
 * The DemoModeController manages the demo mode, including demo video playback and user idle detection.
 * It starts and stops demo mode based on user activity and manages the fading of the video canvas.
 */

public class DemoModeController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoCanvas; // Canvas using RawImage for play demo video
    public float idleTimeLimit = 30f; // If user inactivates more than idle time, demo mode is triggered
    public float fadeDuration = 0.5f; // Duration of fade in or fade out the canvas

    private float idleTime = 0f; // Save idle time of user
    private bool isDemoModeActive = false;
    private CanvasGroup canvasGroup;

    void Start()
    {
        // Initialize the canvasGroup from the videoCanvas and set it to be fully transparent initially
        if (videoCanvas != null)
        {
            canvasGroup = videoCanvas.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f; // Set alpha as 0 initially
            videoCanvas.SetActive(false);
        }
    }

    void Update()
    {
        // Check for user input (any key or mouse click) to reset idle time and stop demo mode if it is active
        if (Input.anyKey || Input.GetMouseButton(0))
        {
            if (isDemoModeActive)
            {
                StopDemoMode(); // Stop demo mode if user input
            }
            idleTime = 0f; // Reset idle time
        }
        else
        {
            idleTime += Time.deltaTime; // Add idle time if no user input is detected
        }

        // If idle time exceeds the limit and demo mode is not active, start demo mode
        if (idleTime >= idleTimeLimit && !isDemoModeActive)
        {
            StartDemoMode(); // Trigger demo mode
        }
    }

    // This function is called when start the demo mode
    // It starts the demo mode, plays the demo video, and freezs the game time
    void StartDemoMode()
    {
        isDemoModeActive = true; // Mark demo mode as active
        videoCanvas.SetActive(true); // Active the video canvas
        videoPlayer.Play(); // Play the demo video
        Time.timeScale = 0f; // Freeze game time while demo mode is active, preventing gameplay

        videoPlayer.loopPointReached += OnVideoEnd;

        // Fade in the video canvas
        StartCoroutine(FadeCanvas(0f, 1f));
    }

    // This function is called when stop the demo mode
    // It stops the demo mode, stops the demo video, and resets the game time
    void StopDemoMode()
    {
        isDemoModeActive = false; // Mark demo mode as inactive
        videoPlayer.Stop(); // Stop the video playback
        videoPlayer.loopPointReached -= OnVideoEnd;
        Time.timeScale = 1f; // Resume normal game time

        // Fade out and then disable canvas
        StartCoroutine(FadeCanvas(1f, 0f, disableAfterFade: true));
    }

    // This function is called when the video ends and stop demo mode
    void OnVideoEnd(VideoPlayer vp)
    {
        StopDemoMode(); // Stop demo mode when the video reaches the end
    }

    // This function is called when the fade out the video canvas
    // It gradually change the alpha value of the canvas group to create a fade effect
    IEnumerator FadeCanvas(float from, float to, bool disableAfterFade = false)
    {
        float timer = 0f; // Fade duration
        // Gradually change the alpha value from 'from' to 'to' over the fade duration
        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(from, to, timer / fadeDuration);
            if (canvasGroup != null)
                canvasGroup.alpha = alpha;
            yield return null;
        }

        if (canvasGroup != null)
            canvasGroup.alpha = to;

        if (disableAfterFade && videoCanvas != null)
        {
            videoCanvas.SetActive(false);
        }
    }
}
