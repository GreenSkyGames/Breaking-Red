using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioType[] AudioTypes;

    public List<AudioSource> allAudioSources = new List<AudioSource>(); // To store all active AudioSources
    public List<bool> audioSourceStates = new List<bool>(); // To store the state, play or pause

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("AudioManager instance initialized.");
        }

        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        foreach (var type in AudioTypes)
        {
            type.Source = gameObject.AddComponent<AudioSource>();

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
        AudioManager.instance.Play("MenuBGM");
    }

    // Helper function to get the AudioSource by the audio name
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

    // Fade out the audio over a duration
    public IEnumerator FadeOut(string name, float duration)
    {
        AudioSource audioSource = GetAudioSource(name);
        if (audioSource != null)
        {
            float startVolume = audioSource.volume;

            // Gradually decrease the volume
            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / duration;
                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume; // Reset volume for next time
        }
    }

    // Fade in the audio over a duration
    public IEnumerator FadeIn(string name, float duration)
    {
        AudioSource audioSource = GetAudioSource(name);
        if (audioSource != null)
        {
            audioSource.Play();
            audioSource.volume = 0; // Start with zero volume

            float targetVolume = 0.1f;

            // Gradually increase the volume
            while (audioSource.volume < targetVolume)
            {
                audioSource.volume += Time.deltaTime / duration;
                yield return null;
            }

            audioSource.volume = targetVolume;
        }
    }

    // Save the state of all audio sources
    public IEnumerator PauseAllAudioSources()
    {
        allAudioSources.Clear();
        audioSourceStates.Clear();

        // Find all AudioSources in the scene and pause them
        #pragma warning disable CS0618
        foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>())
        {
            allAudioSources.Add(audioSource);
            audioSourceStates.Add(audioSource.isPlaying);

            audioSource.Pause();  // Pause the audio
        }
        #pragma warning restore CS0618

        yield return null;
    }

    // Restore the state of all audio sources
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