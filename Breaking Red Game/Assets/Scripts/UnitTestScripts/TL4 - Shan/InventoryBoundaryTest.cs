using UnityEngine;

public class InventoryBoundaryTest : MonoBehaviour
{
    private InventoryManager inventoryManager;

    void Start()
    {
        // get the InventoryManager instance
        inventoryManager = InventoryManager.instance;

        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager is not found.");
            return;
        }

        // Test 1: Add 4 items to the inventory
        Debug.Log("Running Inventory Capacity Test");

        inventoryManager.AddToInventory("Item1", null);  // Add first item
        inventoryManager.AddToInventory("Item2", null);  // Add second item
        inventoryManager.AddToInventory("Item3", null);  // Add third item

        // try adding a fourth item, should be rejected as the inventory is full
        inventoryManager.AddToInventory("Item4", null);  // This should log "Inventory is full!"
    }
}

