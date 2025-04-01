using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class DemoModeController : MonoBehaviour
{
    public VideoPlayer videoPlayer;      // Reference to the VideoPlayer component that plays the demo video
    public float idleTimeLimit = 30f;    // The time limit (in seconds) before the demo video starts, when the player is idle
    private float idleTime = 0f;         // Tracks the amount of idle time (no player input)
    private bool isDemoModeActive = false;  // Flag to check whether demo mode is active or not

    void Update()
    {
        // Check if any key or mouse button is pressed
        if (Input.anyKey || Input.GetMouseButton(0))  // Checks for both keyboard or mouse input
        {
            // If demo mode is active, stop it and return to the game
            if (isDemoModeActive)
            {
                StopDemoMode();
            }
            idleTime = 0f;  // Reset the idle time when there is player input
        }
        else
        {
            idleTime += Time.deltaTime;  // Increment idle time if no player input is detected
        }

        // If the idle time exceeds the limit, start the demo mode
        if (idleTime >= idleTimeLimit && !isDemoModeActive)
        {
            StartDemoMode();  // Begin playing the demo video after the idle period
        }
    }

    // Starts the demo mode by playing the demo video
    void StartDemoMode()
    {
        isDemoModeActive = true;  // Mark that demo mode is active
        videoPlayer.Play();  // Play the demo video

        // Add an event listener to detect when the video ends
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // Stops the demo mode and returns the player to the game
    void StopDemoMode()
    {
        isDemoModeActive = false;  // Mark demo mode as inactive
        videoPlayer.Stop();  // Stop playing the demo video

        // Resume the game after demo mode
        Time.timeScale = 1f;  // Restore the game time (in case it was paused during demo mode)
    }

    // Callback method when the video reaches its end
    void OnVideoEnd(VideoPlayer vp)
    {
        StopDemoMode();  // Stop demo mode and return to the game when the video ends
    }
}
