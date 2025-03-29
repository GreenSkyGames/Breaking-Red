using UnityEngine;
using System.Collections;
using NUnit.Framework;

// TL5 -- Hengyi Tian
// Audio System Testing: 1 stress test and 2 boundary tests

namespace AudioTest
{
    public class AudioSystemTest : MonoBehaviour
    {
        // Stress Test 1: Play multiple sounds simultaneously
        [Test]
        public IEnumerator StressTest_PlayMultipleSounds(int numSounds = 100)
        {
            Debug.Log("Starting Stress Test for Audio System...");

            AudioType[] audioTypes = AudioManager.sinstance.audioTypes;

            if (audioTypes.Length == 0)
            {
                Debug.LogError("No audio types found in AudioManager!");
                yield break;
            }

            for (int i = 0; i < numSounds; i++)
            {
                AudioType audioType = audioTypes[Random.Range(0, audioTypes.Length)];
                AudioManager.sinstance.Play(audioType.Name);
                yield return null;
            }

            Debug.Log("Stress Test for Audio System Completed.");
            StopAllAudio();
        }

        // Boundary Test 1: Incrementally sound volume to check upper limits
        [Test]
        public IEnumerator BoundaryTest_VolumeLimits()
        {
            Debug.Log("Starting Volume Boundary Test...");

            AudioSource bgmSource = AudioManager.sinstance.getAudioSource("MenuBGM");

            if (bgmSource == null)
            {
                Debug.LogError("MenuBGM audio source not found!");
                yield break;
            }

            bgmSource.volume = 0f; // Initial volume
            yield return new WaitForSeconds(0.5f);

            float volumeIncrement = 0.15f; // Increase 0.15 volume each time
            while (bgmSource.volume < 1f)
            {
                bgmSource.volume = Mathf.Min(bgmSource.volume + volumeIncrement, 1f);
                Debug.Log($"Volume set to: {bgmSource.volume}");
                yield return new WaitForSeconds(0.5f);
            }

            Debug.Log("Volume Boundary Test Completed.");
            StopAllAudio();
        }

        // Boundary Test 2: Test pausing and restoring sounds
        [Test]
        public IEnumerator BoundaryTest_RestoreAudioStates()
        {
            Debug.Log("Starting Restore Audio States Boundary Test...");
            #pragma warning disable CS0618
            AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
            #pragma warning disable CS0618
            foreach (AudioSource audioSource in allAudioSources)
            {
                audioSource.Pause();
            }

            yield return null;

            foreach (AudioSource audioSource in allAudioSources)
            {
                audioSource.UnPause();
            }

            Debug.Log("Restore Audio States Boundary Test Completed.");
            StopAllAudio();
        }

        // Stop all playing sounds after test completion
        void StopAllAudio()
        {
            foreach (AudioType type in AudioManager.sinstance.audioTypes)
            {
                AudioManager.sinstance.Stop(type.Name);
            }
        }

        // Run all defined tests
        public void RunTests()
        {
            StartCoroutine(StressTest_PlayMultipleSounds());
            StartCoroutine(BoundaryTest_VolumeLimits());
            StartCoroutine(BoundaryTest_RestoreAudioStates());
        }
    }
}