using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

/*
 * Name: Hengyi Tian
 * Role: TL5-- AI Specialist
 * 
 * This file contains the definition for the Background class.
 * The Background class manages background musics changes during scene transitions.
 * It listens for scene changes and adjusts the background music accordingly, fading in and out different tracks.
 */

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance; // Static instance of the Background class

    // Awake method is called when the object is initialized. Ensure the singleton pattern is applied and the instance
    // not destroyed on scene load. This method is also subscribes to scene change events.
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ensure Background object persists across new scenes
            Debug.Log("BackgroundMusic instance initialized.");
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // Listen for scene changes
    }

    // This function is called when a new scene is loaded.
    // It pauses any background music and changes the music based on the scene name.
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeBackgroundMusic(scene.name);
    }

    // This function changes the background music based on the provided scene tag.
    // It fades out the current music and fades in the new music based on the tag.
    public void ChangeBackgroundMusic(string tag)
    {
        Debug.Log("Changing background music for tag: " + tag); // Log to check the tag received
        
        switch (tag) // Play new music based on the tag
        {
            case "StartMenu":
                StartCoroutine(AudioManager.instance.PauseAllAudioSources());
                StartCoroutine(AudioManager.instance.FadeIn("MenuBGM", 1.0f));  // Fade in main menu music
                break;
            case "IL1.1":
                StartCoroutine(AudioManager.instance.FadeOut("L1BGM", 1.0f)); // Fade out last scene music
                StartCoroutine(AudioManager.instance.FadeOut("WolfSound", 1.0f));
                StartCoroutine(AudioManager.instance.FadeIn("CabinBGM", 1.0f)); // Fade in level 1.1 music
                StartCoroutine(AudioManager.instance.FadeIn("FireSound", 1.0f));
                break;
            case "IL1":
                StartCoroutine(AudioManager.instance.FadeOut("CabinBGM", 1.0f)); // Fade out last scene music
                StartCoroutine(AudioManager.instance.FadeOut("FireSound", 1.0f));
                StartCoroutine(AudioManager.instance.FadeIn("L1BGM", 1.0f)); // Fade in level 1 music
                StartCoroutine(AudioManager.instance.FadeIn("WolfSound", 1.0f));
                break;
            case "L2":
                StartCoroutine(AudioManager.instance.FadeOut("L1BGM", 1.0f)); // Fade out last scene music
                StartCoroutine(AudioManager.instance.FadeOut("WolfSound", 1.0f));
                StartCoroutine(AudioManager.instance.FadeIn("L2BGM", 1.0f)); // Fade in level 2 music
                StartCoroutine(AudioManager.instance.FadeIn("WolfSound", 1.0f));
                StartCoroutine(AudioManager.instance.FadeIn("BatSound", 1.0f));
                break;
            case "L3":
                StartCoroutine(AudioManager.instance.FadeOut("L2BGM", 1.0f)); // Fade out last scene music
                StartCoroutine(AudioManager.instance.FadeOut("WolfSound", 1.0f));
                StartCoroutine(AudioManager.instance.FadeOut("BatSound", 1.0f));
                StartCoroutine(AudioManager.instance.FadeIn("L3BGM", 1.0f));  // Fade in level 3 music
                StartCoroutine(AudioManager.instance.FadeIn("WindSound", 1.0f));
                StartCoroutine(AudioManager.instance.FadeIn("LightingSound", 1.5f));
                break;
            case "L4":
                StartCoroutine(AudioManager.instance.FadeOut("L3BGM", 1.0f)); // Fade out last scene music
                StartCoroutine(AudioManager.instance.FadeOut("WindSound", 1.0f));
                StartCoroutine(AudioManager.instance.FadeIn("L4BGM", 1.0f));  // Fade in level 4 music
                StartCoroutine(AudioManager.instance.FadeIn("FrogSound", 1.5f));
                break;
            case "L5":
                StartCoroutine(AudioManager.instance.FadeOut("L4BGM", 1.0f)); // Fade out last scene music
                StartCoroutine(AudioManager.instance.FadeOut("FrogSound", 1.5f));
                StartCoroutine(AudioManager.instance.FadeIn("L5BGM", 1.0f));  // Fade in level 5 music
                break;
            case "GameOver":
                StartCoroutine(AudioManager.instance.PauseAllAudioSources());
                StartCoroutine(AudioManager.instance.FadeIn("GameoverSound", 1.0f));  // Fade in game over music
                break;
            case "Victory":
                StartCoroutine(AudioManager.instance.PauseAllAudioSources());
                StartCoroutine(AudioManager.instance.FadeIn("VictorySound", 1.0f));  // Fade in victory music
                break;

            // Add more cases for other levels as needed

            default:
                break;
        }
    }
}
