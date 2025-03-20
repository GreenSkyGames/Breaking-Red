using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

public class TestScripts : MonoBehaviour
{
    // Boundary Test 1: Map Edge Test (Camera will follow Player & the Player ends in the same position it started in)
    public IEnumerator MapEdgeTest(float moveDistance = 1000f)
    {
        Debug.Log("Starting Map Edge Test...");

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player GameObject not found. Ensure it has the 'Player' tag.");
            yield break;
        }

        Vector3 originalPosition = player.transform.position;

        player.transform.position = originalPosition + new Vector3(moveDistance, 0, 0);
        yield return new WaitForSeconds(1f);

        player.transform.position = originalPosition + new Vector3(-moveDistance, 0, 0);
        yield return new WaitForSeconds(1f);

        player.transform.position = originalPosition + new Vector3(0, moveDistance, 0);
        yield return new WaitForSeconds(1f);

        player.transform.position = originalPosition + new Vector3(0, -moveDistance, 0);
        yield return new WaitForSeconds(1f);

        player.transform.position = originalPosition;

        Debug.Log("Map Edge Test Completed.");
    }

    // Boundary Test 2: Audio Source Limit Test (play different sounds without breaking the game)
    public IEnumerator AudioSourceLimitTest(int maxAudioSources = 100)
    {
        Debug.Log("Starting Audio Source Limit Test (Folder)...");

        string audioFolderPath = "TestAudio"; // Name of your audio folder in Resources
        Object[] audioClips = Resources.LoadAll(audioFolderPath, typeof(AudioClip));

        if (audioClips == null || audioClips.Length == 0)
        {
            Debug.LogError($"No audio clips found in folder: {audioFolderPath}");
            yield break;
        }

        for (int i = 0; i < maxAudioSources + 50; i++)
        {
            AudioClip clip = (AudioClip)audioClips[Random.Range(0, audioClips.Length)]; // Select a random clip
            AudioSource.PlayClipAtPoint(clip, transform.position);
            yield return null;
        }

        Debug.Log("Audio Source Limit Test (Folder) Completed.");

        yield return null;
    }

    public void RunTests()
    {
        StartCoroutine(MapEdgeTest());
        StartCoroutine(AudioSourceLimitTest());
    }
}