using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Name: Shan Peck
*	Role: Team Lead 4 -- Project Manager
*	
*	This file contains the definition for the InventoryManager class
*   It manages showing inventory and adding things to it
*	It inherits from MonoBehaviour */

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    public ItemSlot[] itemSlot;
    public static InventoryManager sInstance;
    public ItemSlot selectedSlot;
    public PowerUpManager powerUpManager;
    public Sprite emptySprite;

    private bool _menuActivated;

    /* This function updates the scene by toggling the inventory if the player clicks I
     *	It only uses the function toggleInventory() */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            toggleInventory();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            useSelectedItem();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            removeFromInventory();
        }

    }

    /* This functions function sets the instance of InventoryManager
     * Singleton pattern to ensure one instance of InventoryManager */
    private void Awake()
    {
        if (sInstance == null)
        {
            sInstance = this;  // set the instance
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);  // prevent duplicates
        }


    }

    /* This function adds an item to the inventory using its name and sprite
     * It looks at the item slots I created in UI b/c I attached them in Inspector
     * Checks if there is an unoccupied slot and if not, put the item in
     * Uses isOccupied bool and updateInventoryUI() from ItemSlot script */
    public void addToInventory(string itemName, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if(itemSlot[i] != null && itemSlot[i].isOccupied == false)
            {
                itemSlot[i].updateInventoryUI(itemName, itemSprite, itemDescription);
                return;
            }
        }
        Debug.Log("Inventory is full or slot invalid!");
    }

    /* This function loops through each slot in inventory and counts occupied ones 
     * Is currently only used by boundary test */
    public int getItemCount()
    {
        int count = 0;

        foreach (var slot in itemSlot)
        {
            if (slot.isOccupied) // if the slot is occupied, increment the count
            {
                count++;
            }
        }

        return count;
    }   

    /* This function toggles the inventory by turned the menu on or off
     * Uses InventoryMenu object and bool menuActivated
     * The game time is paused when inventory is on 
     * Uses built in Unity function SetActive() */
    private void toggleInventory()
    {
        if (_menuActivated) // if I clicked when menu is on, turn it off
        {
            Time.timeScale = 1;
            inventoryMenu.SetActive(false);
            _menuActivated = false;
        }
        else if(!_menuActivated) // if I clicked when menu is off, turn it on
        {
            Time.timeScale = 0;
            inventoryMenu.SetActive(true);
            _menuActivated = true;
        }
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
        selectedSlot = null;
    }

    public void useSelectedItem()
    {
        if (selectedSlot != null && selectedSlot.isOccupied)
        {
            string itemName = selectedSlot.itemName;
        
            // Find the PowerUp type based on the item name
            PowerUp itemToUse = FindPowerUpByName(itemName);
        
            if (itemToUse != null && powerUpManager != null)
            {
                powerUpManager.ApplyPowerUpEffect(itemToUse, selectedSlot.gameObject.GetComponent<PlayerController>());
                removeFromInventory();  // Remove the item from inventory after use
            }
            else
            {
                Debug.Log("No matching PowerUp found for: " + itemName);
            }
        }
        else
        {
            Debug.Log("No item selected to use.");
        }
    }

    public void removeFromInventory()
    {
        if (selectedSlot != null && selectedSlot.isOccupied)
        {
            // Clear the item's data
            selectedSlot.itemName = "";
            selectedSlot.itemSprite = null;
            selectedSlot.itemDescription = "";
            selectedSlot.isOccupied = false;
            selectedSlot.itemImage.sprite = emptySprite;

            // Hide selection shader & clear selection
            selectedSlot.selectedShader.SetActive(false);
            selectedSlot.thisItemSelected = false;

            // Clear description UI
            selectedSlot.ItemNameText.text = "";
            selectedSlot.itemDescriptionText.text = "";
            selectedSlot.itemDescriptionImage.sprite = null;

            selectedSlot = null;
        }
        else
        {
            Debug.Log("No item selected to remove.");
        }
    }

    private PowerUp FindPowerUpByName(string itemName)
    {
        // Loop through your power-up definitions (assuming they are stored or defined somewhere)
        PowerUp[] allPowerUps = FindObjectsOfType<PowerUp>();  // Find all power-ups in the scene

        foreach (PowerUp powerUp in allPowerUps)
        {
            if (powerUp.itemType.ToString() == itemName)
            {
                return powerUp;
            }
        }

        return null; // Return null if no matching PowerUp is found
    }
}


