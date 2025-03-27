using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem.Controls;

public class PlatformTest : MonoBehaviour
{
    public GameObject player;
    public MovingPlatform platform;
    private Vector3 originalPosition;
    private Vector3 originalPlatformPos;
    private PlayerHealth playerHealth;
    public Tilemap tilemap;
    private float timeOnPlatform = 0f;  // Time the player stays on the platform
    private bool hasEnteredPlatform = false;

    [SerializeField] private float timeToMoveOff = 8f;  // Time before player is forced off the platform

    private void Start()
    {
        if (player == null || platform == null)
        {
            Debug.LogError("Player or platform not assigned!");
            return;
        }

        playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth component missing!");
            return;
        }

        originalPosition = player.transform.position; // Store the original spawn position
        originalPlatformPos = platform.transform.position;
        Debug.Log("Player Platform Test Initialized.");
    }

    public void StartTest()
    {
        Debug.Log("Starting Player Platform Test...");
        StartCoroutine(RunPlatformTest());
    }

    private IEnumerator RunPlatformTest()
    {
        if (tilemap != null && tilemap.gameObject.activeSelf)
        {
            tilemap.gameObject.SetActive(false); // Hide boundary layer
            Debug.Log("Layer turned off");
        }
        // Reset player health and position
        playerHealth.changeHealth(100); // Reset health before test
        player.transform.position = platform.transform.position + new Vector3(0, 0, 0); // Spawn player on top of the platform
        Debug.Log("Player spawned on platform at start position.");

        timeOnPlatform = 0f;
        yield return new WaitForSeconds(1f);
        while (true)
        {
            //If player is not recognized by platform as being on the platform, turn on boundary layer which should kill the player
            if (!hasEnteredPlatform)
            {
                tilemap.gameObject.SetActive(true); // Show boundary layer
                Debug.Log("Layer turned on");
                Debug.LogError("Test Failed: Player not properly placed on platform.");
            }

            // Wait for the specified time before moving the player off the platform
            if (timeOnPlatform >= timeToMoveOff)
            {
                yield return null;  // Wait one frame to continue checking
                break;
            }

            timeOnPlatform += Time.deltaTime;
            yield return null;
        }

        // Check if player health is intact
        if (playerHealth.currentHealth > 0)
        {
            Debug.Log("Test Passed: Player survived and properly interacted with platform after teleportation");
        }
        else
        {
            Debug.LogError("Test Failed: Player died during test.");
        }

        // Reset the player to the original position
        player.transform.position = originalPosition;
        Debug.Log("Test completed. Player reset to original position.");
    }

    public void OnPlayerEnterPlatform()
    {
        hasEnteredPlatform = true;
        Debug.Log("Player has entered the platform trigger zone.");
    }

    // Reset the entry flag when the player exits the platform's trigger
    public void OnPlayerExitPlatform()
    {
        hasEnteredPlatform = false;
        Debug.Log("Player has exited the platform trigger zone.");
    }
}
