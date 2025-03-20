using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;
using NUnit.Framework;

// 为了解决命名空间冲突，可以考虑修改命名空间或者脚本名称
namespace AudioTest
{
    public class AudioSystemTest : MonoBehaviour
    {
        // TL5: Stress Testing 1 of Audio System
        [Test]
        public IEnumerator StressTest_PlayMultipleSounds(int numSounds = 100)
        {
            Debug.Log("Starting Stress Test for Audio System ...");

            // Load all audio clips
            Object[] audioClips = Resources.LoadAll("TestAudio", typeof(AudioClip));

            if (audioClips.Length == 0)
            {
                Debug.LogError("No audio clips found!");
                yield break;
            }

            // Play multiple audio clips
            for (int i = 0; i < numSounds; i++)
            {
                AudioClip clip = (AudioClip)audioClips[Random.Range(0, audioClips.Length)];
                AudioSource.PlayClipAtPoint(clip, transform.position);
                yield return null;
            }

            Debug.Log("Stress Test for Audio System Completed.");
        }

        public IEnumerator BoundaryTest_VolumeLimits()
        {
            Debug.Log("Starting Volume Boundary Test...");

            // Set volume to 0 and 1
            AudioManager.instance.GetAudioSource("MenuBGM").volume = 0f; // Minimum volume
            yield return null; // Yield to allow the audio change to take effect

            AudioManager.instance.GetAudioSource("MenuBGM").volume = 1f; // Maximum volume
            yield return null; // Yield to allow the audio change to take effect

            Debug.Log("Volume Boundary Test Completed.");
        }

        public IEnumerator BoundaryTest_ExceedAudioSources(int maxAudioSources = 100)
        {
            Debug.Log("Starting Audio Source Boundary Test...");

            // Load all audio clips from the Resources folder
            Object[] audioClips = Resources.LoadAll("TestAudio", typeof(AudioClip));

            // Check if there are any audio clips available
            if (audioClips == null || audioClips.Length == 0)
            {
                Debug.LogError("No audio clips found in the folder!");
                yield break;  // Stop if no clips are found
            }

            // Simulate playing beyond the limit of audio sources
            for (int i = 0; i < maxAudioSources; i++)
            {
                // Select a random audio clip from the loaded clips
                AudioClip clip = (AudioClip)audioClips[Random.Range(0, audioClips.Length)];

                // Play the selected audio clip at the current position of the GameObject
                AudioSource.PlayClipAtPoint(clip, transform.position);

                // Wait for one frame before continuing the next iteration
                yield return null;
            }

            Debug.Log("Audio Source Boundary Test Completed.");
        }

        public IEnumerator BoundaryTest_RestoreAudioStates()
        {
            Debug.Log("Starting Restore Audio States Boundary Test...");

            // Find all AudioSource components in the scene
            AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

            // Pause all the audio sources
            foreach (AudioSource audioSource in allAudioSources)
            {
                audioSource.Pause();
            }

            // Wait for one frame before proceeding
            yield return null;

            // Resume all the paused audio sources
            foreach (AudioSource audioSource in allAudioSources)
            {
                audioSource.UnPause();
            }

            Debug.Log("Restore Audio States Boundary Test Completed.");
        }

        // 为了避免方法重复定义错误，确保在同一个类中没有重复定义相同名称和参数类型的方法
        public void RunTests()
        {
            StartCoroutine(StressTest_PlayMultipleSounds());
            StartCoroutine(BoundaryTest_VolumeLimits());
            StartCoroutine(BoundaryTest_ExceedAudioSources());
            //StartCoroutine(BoundaryTest_RestoreAudioStates());
        }
    }
}
