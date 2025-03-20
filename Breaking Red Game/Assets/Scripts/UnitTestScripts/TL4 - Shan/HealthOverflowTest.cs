using UnityEngine;

public class HealthOverflowTest : MonoBehaviour
{
    public PlayerController playerController;

    public void RunHealthOverflowTest()
    {
        if (playerController == null)
        {
            Debug.LogError("PlayerController is not assigned.");
            return;
        }

        PlayerHealth playerHealth = playerController.GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth is not attached.");
            return;
        }

        // Test 2: Check health overflow
        Debug.Log("Running Health Overflow Test");

        // set the player's health to 3 (max health)
        playerHealth.ChangeHealth(3);

        // apply golden apple (increases health)
        PowerUp goldenApple = new GameObject("GoldenApple").AddComponent<PowerUp>(); // dynamically create PowerUp instance
        goldenApple.type = PowerUp.itemName.GoldenApple;  // set type to GoldenApple
        goldenApple.effectAmount = 5; // set effect amount for health boost
        goldenApple.sprite = null;

        goldenApple.ApplyEffect(playerController);
        Debug.Log($"Health after applying: {playerHealth.currentHealth}");

        // check if the health has exceeded the limit (3)
        if (playerHealth.currentHealth > playerHealth.maxHealth)
        {
            Debug.LogError("Health Overflow: Health has exceeded the limit!");
        }
        else
        {
            Debug.Log("Health Overflow Test Passed: Health is within the limit.");
        }
    }
}
