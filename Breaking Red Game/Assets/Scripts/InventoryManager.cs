using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<PowerUp> inventory;  // list power ups
    public int maxSlots = 3;  // initial inventory size

    private void Awake()
    {
        // singleton pattern to ensure one instance of InventoryManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(gameObject);

        inventory = new List<PowerUp>(maxSlots);  // initialize inventory
    }

    public void AddToInventory(PowerUp powerUp)
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory is null");
            inventory = new List<PowerUp>(maxSlots);
        }
        if (inventory.Count < maxSlots)  // check if empty slot
        {
            inventory.Add(powerUp);  // Add the power-up to the inventory
            Debug.Log("Power-up added to inventory");
        }
        else
        {
            Debug.Log("Inventory full! Cannot add power-up.");
        }
    }

    // increase inventory later?
    public void IncreaseInventorySlots(int additionalSlots)
    {
        maxSlots += additionalSlots;
        Debug.Log("Inventory size increased to: " + maxSlots);
    }
}

