using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
    }

    public void ChangeBackgroundMusic(string tag)
    {
        Debug.Log("Changing background music for tag: " + tag); // Log to check the tag received

        // List of audio names to pause
        List<string> BGaudioNames = new List<string> { "CabinBGM", "L1BGM", "L2BGM", "L3BGM", "L4BGM", "L5BGM", "WolfSound", "BatSound" };
        StartCoroutine(AudioManager.instance.PauseAllMusic(1.5f, BGaudioNames));

        // Play new music based on the tag
        switch (tag)
        {
            case "IL1.1":
                StartCoroutine(AudioManager.instance.FadeIn("CabinBGM", 1.5f));  // Fade in
                break;

            case "IL1":
                StartCoroutine(AudioManager.instance.FadeIn("L1BGM", 1.5f));  // Fade in level 1 music
                StartCoroutine(AudioManager.instance.FadeIn("WolfSound", 1.5f)); // Fade in
                break;

            case "L2":
                StartCoroutine(AudioManager.instance.FadeIn("L2BGM", 1.5f));  // Fade in level 2 music
                StartCoroutine(AudioManager.instance.FadeIn("WolfSound", 1.5f)); // Fade in
                StartCoroutine(AudioManager.instance.FadeIn("BatSound", 1.5f)); // Fade in
                break;

            case "L3":
                StartCoroutine(AudioManager.instance.FadeIn("L3BGM", 1.5f));  // Fade in level 3 music
                StartCoroutine(AudioManager.instance.FadeIn("BatSound", 1.5f)); // Fade in
                break;

            case "L4":
                StartCoroutine(AudioManager.instance.FadeIn("L4BGM", 1.5f));  // Fade in level 4 music
                // StartCoroutine(AudioManager.instance.FadeIn("BatSound", 1.5f)); // Fade in
                break;

            case "L5":
                StartCoroutine(AudioManager.instance.FadeIn("L5BGM", 1.5f));  // Fade in level 5 music
                StartCoroutine(AudioManager.instance.FadeIn("BatSound", 1.5f)); // Fade in
                break;

            // Add more cases for other levels as needed

            default:
                break;
        }
    }
}
