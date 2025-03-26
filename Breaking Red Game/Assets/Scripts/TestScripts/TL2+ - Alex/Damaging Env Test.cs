using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class DamagingEnvTest : MonoBehaviour
{
    public GameObject player;
    private PlayerHealth playerHealth;
    private Vector3 startPosition;
    private Vector3 testPos = new Vector3(-0.34f, -9f, 0);
    private int dmg = 9;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player not assigned!");
            return;
        }

        playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth component missing!");
            return;
        }

        startPosition = player.transform.position; // Store player's original position
        Debug.Log($"DamagingEnv script initialized on {gameObject.name}");
    }

    public void StartBoundaryTest()
    {
        Debug.Log("Starting Player Boundary Test...");
        StartCoroutine(RunDamageOverTimeTest());
    }

    private IEnumerator RunDamageOverTimeTest()
    {
        playerHealth.changeHealth(100); // Reset player health before testing
        Debug.Log("Player health reset to 100.");

        // Move player into the testing zone
        player.transform.position = testPos;
        Debug.Log("Player moved into testing zone.");
        // Start moving the player downwards

        int rounds = 0;
        while (rounds < 13)
        {
            yield return new WaitForSeconds(1f); // Wait for damage tick
            if (playerHealth.currentHealth - dmg <= 0)
            {
                Debug.Log($"Round {rounds + 1}: Player Health = {playerHealth.currentHealth}");
                Debug.Log("Test Completed Successfully: Player died upon long interaction with dangerous environment.");
            }
            else
            {
                Debug.Log($"Round {rounds + 1}: Player Health = {playerHealth.currentHealth}");
            }
            rounds++;
        }

        Debug.LogError("Test Failed: Player survived beyond expected boundary.");
    }
}
