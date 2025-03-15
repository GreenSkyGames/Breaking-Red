using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioType[] AudioTypes;

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
    public IEnumerator PauseAllMusic(float fadeDuration, List<string> audioNamesToPause)
    {
        // Gradually reduce the volume of each audio source that matches the specified names
        foreach (AudioType type in AudioTypes)
        {
            // Check if the current audio's name is in the specified list
            if (audioNamesToPause.Contains(type.Name) && type.Source.isPlaying)
            {
                float startVolume = type.Source.volume; // Save the initial volume
                float elapsed = 0f;

                // Gradually lower the volume
                while (elapsed < fadeDuration)
                {
                    type.Source.volume = Mathf.Lerp(startVolume, 0f, elapsed / fadeDuration); // Linear interpolation for volume reduction
                    elapsed += Time.deltaTime;
                    yield return null; // Wait for the next frame
                }

                type.Source.volume = 0f; // Ensure the volume is set to 0
                type.Source.Pause(); // Pause the audio source
            }
        }

        yield return null;
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

    // Helper function to get the AudioSource by the audio name
    private AudioSource GetAudioSource(string name)
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

}
