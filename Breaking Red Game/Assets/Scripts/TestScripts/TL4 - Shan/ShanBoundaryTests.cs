using UnityEngine;

public class ShanBoundaryTests : MonoBehaviour
{
    private InventoryManager inventoryManager;
    public PlayerController playerController;
    public int numberOfTests = 1000;  // Number of rapid health changes to simulate
    public int healthBoostAmount = 1;  // Amount the Golden Apple boosts health by

    void runInventoryBoundaryTest()
    {
        // get the InventoryManager instance
        inventoryManager = InventoryManager.sInstance;

        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager is not found.");
            return;
        }

        // Test 1: Add 4 items to the inventory
        Debug.Log("Running Inventory Capacity Test");

        inventoryManager.addToInventory("Item1", null);  // Add first item
        inventoryManager.addToInventory("Item2", null);  // Add second item
        inventoryManager.addToInventory("Item3", null);  // Add third item

        // try adding a fourth item, should be rejected as the inventory is full
        inventoryManager.addToInventory("Item4", null);  // This should log "Inventory is full!"
        int totalItems = inventoryManager.getItemCount();
        int expectedItemCount = 3;  // still want only 3 items in test after

        if (totalItems == expectedItemCount)
        {
            Debug.Log("Inventory Boundary Test Passed: Inventory correctly rejected the fourth item.");
        }
        else
        {
            Debug.LogError($"Inventory Boundary Test Failed: Expected {expectedItemCount} items, but found {totalItems}.");
        }
    }

    public void runHealthOverflowTest()
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
        playerHealth.changeHealth(3);

        // apply golden apple (increases health)
        PowerUp goldenApple = new GameObject("GoldenApple").AddComponent<PowerUp>(); // dynamically create PowerUp instance
        goldenApple.itemType = PowerUp.itemName.GoldenApple;  // set type to GoldenApple
        goldenApple.effectAmount = 5; // set effect amount for health boost
        goldenApple.sprite = null;

        goldenApple.v_applyEffect(playerController);
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

    void runStressTest()
    {
        if (playerController == null)
        {
            Debug.LogError("PlayerController is not assigned.");
            return;
        }

        // Create a GoldenApple power-up instance to apply rapidly
        PowerUp goldenApple = new GameObject("GoldenApple").AddComponent<PowerUp>();
        goldenApple.itemType = PowerUp.itemName.GoldenApple;
        goldenApple.effectAmount = healthBoostAmount;
        goldenApple.sprite = null;  // No sprite needed for this test

        // Stress test: Apply the Golden Apple power-up multiple times
        Debug.Log("Running Health System Stress Test");

        for (int i = 0; i < numberOfTests; i++)
        {
            goldenApple.v_applyEffect(playerController);  // Apply the Golden Apple effect
        }

        // After stress test, check health and print the result
        PlayerHealth playerHealth = playerController.GetComponent<PlayerHealth>();
        Debug.Log($"Final Health: {playerHealth.currentHealth}");

        if (playerHealth.currentHealth <= playerHealth.maxHealth)
        {
            Debug.Log("Stress Test Passed: Health is within the valid range.");
        }
        else
        {
            Debug.LogError("Stress Test Failed: Health exceeded the maximum limit!");
        }
    }

    public void runShanTests()
    {
        runInventoryBoundaryTest();
        runHealthOverflowTest();
        runStressTest();
    }
}


