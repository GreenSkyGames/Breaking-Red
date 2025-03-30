using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

/*
 * Name: Hengyi Tian
 * Role: TL5-- AI Specialist
 * 
 * This file contains the definition for the AudioManager class.
 * The AudioManager class manages audio playing, including background musics and sound effects
 * It have different audio controling such as play, pause, stop, fade in, fade out, and restoring audio states.
 */

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Static instance of the AudioManager class
    public AudioType[] AudioTypes; // Array to hold the audio types
    public List<AudioSource> allAudioSources = new List<AudioSource>(); // List to store all active AudioSources
    public List<bool> audioSourceStates = new List<bool>(); // List to store the state, play or pause

    // Awake method is called when the object is initialized. Checking if the game is already have a Audiomanager
    // Ensure that the AudioManager is a singleton across scenes.
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ensures AudioManager persists across scenes
            Debug.Log("AudioManager instance initialized.");
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate AudioManager if one already exists
            return;
        }
    }

    // Start method initializes each audio source with settings from the AudioTypes array.
    // Assigning clips, name, volume, pitch and loop 
    private void Start()
    {
        foreach (var type in AudioTypes)
        {
            //type = new AudioTypeBoss();
            type.Source = gameObject.AddComponent<AudioSource>(); // Add AudioSource component to the game object
            type.Source.clip = type.Clip;
            type.Source.name = type.Name;
            type.Source.volume = type.Volume;
            type.Source.pitch = type.Pitch;
            type.Source.loop = type.Loop;

            if(type.Group != null)
            {
                type.Source.outputAudioMixerGroup = type.Group;
            }
        }
        AudioManager.instance.Play("MenuBGM"); // Play background music of main menu
    }

    // This function to get the AudioSource by the audio name
    public AudioSource GetAudioSource(string name)
    {
        foreach (AudioType type in AudioTypes)
        {
            if (type.Name == name)
            {
                return type.Source;
            }
        }
        Debug.LogWarning("Can not find audio named " + name + "!");
        return null;
    }

    // This function plays an audio clip by its string name
    public void Play(string name)
    {
        foreach(AudioType type in AudioTypes)
        {
            if (type.Name == name)
            {
                type.Source.Play();
                return;
            }
        }
        Debug.LogWarning("Can not find audio named " + name + "!");
    }

    // This function pause an audio clip by its string name
    public void Pause(string name)
    {
        foreach (AudioType type in AudioTypes)
        {
            if (type.Name == name)
            {
                type.Source.Pause();
                return;
            }
        }
        Debug.LogWarning("Can not find audio named " + name + "!");
    }

    // This function stops an audio clip by its string name
    public void Stop(string name)
    {
        foreach (AudioType type in AudioTypes)
        {
            if (type.Name == name)
            {
                type.Source.Stop();
                return;
            }
        }
        Debug.LogWarning("Can not find audio named " + name + "!");
    }

    // This function fades out an audio over a duration. Gradually decreases the volume to 0 before stopping the audio 
    public IEnumerator FadeOut(string name, float duration)
    {
        AudioSource audioSource = GetAudioSource(name);
        if (audioSource != null)
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0) // Gradually decrease the volume
            {
                audioSource.volume -= startVolume * Time.deltaTime / duration;
                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume; // Reset volume for next time
        }
    }

    // This function fades in an audio over a duration. Gradually increases the volume from 0 to target volume
    public IEnumerator FadeIn(string name, float duration)
    {
        AudioSource audioSource = GetAudioSource(name);
        if (audioSource != null)
        {
            audioSource.Play();
            audioSource.volume = 0; // Start with zero volume

            float targetVolume = 0.1f;

            while (audioSource.volume < targetVolume) // Gradually increase the volume to 0.1f
            {
                audioSource.volume += Time.deltaTime / duration;
                yield return null;
            }
            audioSource.volume = targetVolume;
        }
    }

    // This function pauses all audio sources that is playing in the current scene and stores the state of each audio sources
    public IEnumerator PauseAllAudioSources()
    {
        allAudioSources.Clear();
        audioSourceStates.Clear();

        #pragma warning disable CS0618
        foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>()) // Find all AudioSources in the scene
        {
            allAudioSources.Add(audioSource);
            audioSourceStates.Add(audioSource.isPlaying);

            audioSource.Pause();  // Pause the audio
        }
        #pragma warning restore CS0618

        yield return null;
    }

    // This function restores all audio sources that were paused before
    public IEnumerator RestoreAudioStates()
    {
        for (int i = 0; i < allAudioSources.Count; i++)
        {
            AudioSource audioSource = allAudioSources[i];

            if (audioSourceStates[i])
            {
                audioSource.UnPause();  // Unpause the audio
            }
        }

        // Clear the stored states after restoring
        allAudioSources.Clear();
        audioSourceStates.Clear();

        yield return null;
    }
}