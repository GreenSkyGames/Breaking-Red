using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
            return;
        }
        // Listen for scene changes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Pause all BGM when a new scene is loaded
        //StartCoroutine(PauseAllBGM(1.0f));
        // After pausing, change the background music based on the scene tag
        ChangeBackgroundMusic(scene.name);
    }

    public void ChangeBackgroundMusic(string tag)
    {
        Debug.Log("Changing background music for tag: " + tag); // Log to check the tag received

        // StartCoroutine(PauseAllBGM(0.8f));

        // Play new music based on the tag
        switch (tag)
        {
            case "IL1.1":
                StartCoroutine(AudioManager.instance.FadeOut("L1BGM", 1.0f));
                StartCoroutine(AudioManager.instance.FadeOut("WolfSound", 1.0f));
                StartCoroutine(AudioManager.instance.FadeIn("CabinBGM", 1.0f)); // Fade in level 1.1 music
                StartCoroutine(AudioManager.instance.FadeIn("FireSound", 1.0f));
                break;

            case "IL1":
                StartCoroutine(AudioManager.instance.FadeOut("CabinBGM", 1.0f));
                StartCoroutine(AudioManager.instance.FadeOut("FireSound", 1.0f));
                StartCoroutine(AudioManager.instance.FadeIn("L1BGM", 1.0f)); // Fade in level 1 music
                StartCoroutine(AudioManager.instance.FadeIn("WolfSound", 1.0f));
                break;

            case "Level 2":
                StartCoroutine(AudioManager.instance.FadeOut("L1BGM", 1.0f));
                StartCoroutine(AudioManager.instance.FadeOut("WolfSound", 1.0f));
                StartCoroutine(AudioManager.instance.FadeIn("L2BGM", 1.0f)); // Fade in level 2 music
                StartCoroutine(AudioManager.instance.FadeIn("WolfSound", 1.0f));
                StartCoroutine(AudioManager.instance.FadeIn("BatSound", 1.0f));
                break;

            case "Level 3":
                StartCoroutine(AudioManager.instance.FadeOut("L2BGM", 1.0f));
                StartCoroutine(AudioManager.instance.FadeOut("WolfSound", 1.0f));
                StartCoroutine(AudioManager.instance.FadeOut("BatSound", 1.0f));
                StartCoroutine(AudioManager.instance.FadeIn("L3BGM", 1.0f));  // Fade in level 3 music
                StartCoroutine(AudioManager.instance.FadeIn("BatSound", 1.0f));
                break;

            case "Level 4":
                StartCoroutine(AudioManager.instance.FadeOut("L3BGM", 1.0f));
                StartCoroutine(AudioManager.instance.FadeOut("BatSound", 1.0f));
                StartCoroutine(AudioManager.instance.FadeIn("L4BGM", 1.0f));  // Fade in level 4 music
                StartCoroutine(AudioManager.instance.FadeIn("BatSound", 1.5f));
                break;

            case "Level 5":
                StartCoroutine(AudioManager.instance.FadeOut("L4BGM", 1.0f));
                StartCoroutine(AudioManager.instance.FadeIn("L5BGM", 1.0f));  // Fade in level 5 music
                StartCoroutine(AudioManager.instance.FadeIn("BatSound", 1.0f));
                break;

            // Add more cases for other levels as needed

            default:
                break;
        }
    }
    private IEnumerator PauseAllBGM(float fadeDuration)
    {
        // Use the predefined BGaudioNames list
        List<string> BGaudioNames = new List<string> { "CabinBGM", "L1BGM", "L2BGM", "L3BGM", "L4BGM", "L5BGM", "WolfSound", "BatSound" };

        // Gradually reduce the volume of each audio source that matches the specified names
        foreach (string audioName in BGaudioNames)
        {
            // Find the corresponding AudioSource for the given name
            AudioSource audioSource = AudioManager.instance.GetAudioSource(audioName);
            if (audioSource != null && audioSource.isPlaying)
            {
                float startVolume = audioSource.volume; // Save the initial volume
                float elapsed = 0f;

                // Gradually lower the volume
                while (elapsed < fadeDuration)
                {
                    audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / fadeDuration); // Linear interpolation for volume reduction
                    elapsed += Time.deltaTime;
                    yield return null; // Wait for the next frame
                }

                audioSource.volume = 0f; // Ensure the volume is set to 0
                audioSource.Pause(); // Pause the audio source
            }
        }

        yield return null;
    }
}
