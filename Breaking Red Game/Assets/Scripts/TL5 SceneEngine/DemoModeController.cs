using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class DemoModeController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoCanvas;          // Canvas that contains RawImage
    public float idleTimeLimit = 30f;
    public float fadeDuration = 0.5f;         // Duration of fade in/out

    private float idleTime = 0f;
    private bool isDemoModeActive = false;
    private CanvasGroup canvasGroup;

    void Start()
    {
        // Get the CanvasGroup component from the videoCanvas
        if (videoCanvas != null)
        {
            canvasGroup = videoCanvas.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
            videoCanvas.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.anyKey || Input.GetMouseButton(0))
        {
            if (isDemoModeActive)
            {
                StopDemoMode();
            }
            idleTime = 0f;
        }
        else
        {
            idleTime += Time.deltaTime;
        }

        if (idleTime >= idleTimeLimit && !isDemoModeActive)
        {
            StartDemoMode();
        }
    }

    void StartDemoMode()
    {
        isDemoModeActive = true;
        videoCanvas.SetActive(true);
        videoPlayer.Play();
        Time.timeScale = 0f;

        videoPlayer.loopPointReached += OnVideoEnd;

        // Fade in
        StartCoroutine(FadeCanvas(0f, 1f));
    }

    void StopDemoMode()
    {
        isDemoModeActive = false;
        videoPlayer.Stop();
        videoPlayer.loopPointReached -= OnVideoEnd;
        Time.timeScale = 1f;

        // Fade out and then disable canvas
        StartCoroutine(FadeCanvas(1f, 0f, disableAfterFade: true));
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        StopDemoMode();
    }

    IEnumerator FadeCanvas(float from, float to, bool disableAfterFade = false)
    {
        float timer = 0f;
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
