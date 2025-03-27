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

    private bool menuActivated;

    /* This function updates the scene by toggling the inventory if the player clicks I
     *	It only uses the function toggleInventory() */
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            toggleInventory();
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
    public void addToInventory(string itemName, Sprite itemSprite)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if(itemSlot[i].isOccupied == false)
            {
                itemSlot[i].updateInventoryUI(itemName, itemSprite);
                return;
            }
        }
        Debug.Log("Inventory is full!");
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
        if(menuActivated) // if I clicked when menu is on, turn it off
        {
            Time.timeScale = 1;
            inventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if(!menuActivated) // if I clicked when menu is off, turn it on
        {
            Time.timeScale = 0;
            inventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }
}


