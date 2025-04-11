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
    public int effectAmount;
    public int maxInventorySize;
    public GameObject inventoryMenu;
    public ClueInventory clueInventory;
    public ItemSlot[] itemSlot;
    public static InventoryManager sInstance;
    public ItemSlot selectedSlot;
    public PowerUpManager powerUpManager;
    public Sprite emptySprite;

    private bool _menuActivated;
    private PlayerController playerController;
    private DialogueManager dialogueManager;

    void Start()
    {
        int cluesGathered = FindObjectOfType<DialogueManager>().cluesGathered.Count;
        int maxInventorySize = clueInventory.v_getInventorySize(cluesGathered);
    }

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
            // Assuming this script is attached to the inventory manager, and the player is a separate object
            GameObject player = GameObject.FindWithTag("Player");  // Use tag to find the player GameObject
            if (player != null)
            {
                useSelectedItem(player.GetComponent<PlayerController>());  // Get the PlayerController component attached to the Player GameObject
            }
            else
            {
                Debug.LogError("Player GameObject not found. Make sure the player has the 'Player' tag.");
            }
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
    public bool addToInventory(string itemName, Sprite itemSprite, string itemDescription)
    {
        if (itemSlot.Length <= maxInventorySize)
        {
            for (int i = 0; i < itemSlot.Length; i++)
            {
                if(itemSlot[i] != null && itemSlot[i].isOccupied == false)
                {
                    itemSlot[i].updateInventoryUI(itemName, itemSprite, itemDescription);
                    return true;
                }
            }
        }
        Debug.Log("Inventory is full or slot invalid!");
        return false;
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

    public bool checkCollectibles()
    {
        bool isCollectible = false;
        foreach (var slot in itemSlot)
        {
            if (slot.itemName == "OwlsWing" || slot.itemName == "CanOfTuna")
            {
                isCollectible = true;
            }
        }
        foreach (var slot in itemSlot)
        {
            if (slot.isOccupied && ((slot.itemName == "OwlsWing") || (slot.itemName == "CanOfTuna")))
            {
                // Clear the item's data
                slot.itemName = "";
                slot.itemSprite = null;
                slot.itemDescription = "";
                slot.isOccupied = false;
                slot.itemImage.sprite = emptySprite;

                // Hide selection shader
                slot.selectedShader.SetActive(false);
                slot.thisItemSelected = false;

                // Clear description UI if it happens to be showing this item
                slot.ItemNameText.text = "";
                slot.itemDescriptionText.text = "";
                slot.itemDescriptionImage.sprite = null;
            }
        }
        return isCollectible ? true : false;
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

    public void deselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

    public void useSelectedItem(PlayerController playerController)
    {
        if (selectedSlot != null && selectedSlot.isOccupied)
        {
            if (selectedSlot != null && selectedSlot.isOccupied)
            {
                string itemName = selectedSlot.itemName;  // Get the item name from the selected slot
                applyPowerUpEffect(itemName, playerController);  // Apply the power-up effect based on the selected item
                removeFromInventory();
            }
            else
            {
                Debug.LogWarning("No item selected or item is empty.");
            }
        }
    }

    private void applyPowerUpEffect(string itemName, PlayerController playerController)
    {
        switch (itemName)
        {
            case "GoldenApple":
                Debug.Log("Applying Golden Apple effect");
                PlayerHealth playerHealth = playerController.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.changeHealth(10);  // Increases health
                }
                break;
            case "BerserkerBrew":
                Debug.Log("Applying Berserker Brew effect");
                playerController.attackDamage += 1;
                break;
            case "EnchantedBerry":
                Debug.Log("Applying Enchanted Berry effect");
                //playerController.sanity += effectAmount;
                break;
            case "RedShoes":
                playerController.speed += 2.0f;
                break;
            default:
                Debug.LogWarning("Unknown power-up: " + itemName);
                break;
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
}


