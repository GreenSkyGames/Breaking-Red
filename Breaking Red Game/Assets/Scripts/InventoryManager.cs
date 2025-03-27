using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    public ItemSlot[] itemSlot;
    public static InventoryManager sInstance;

    private bool menuActivated;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            toggleInventory();
        }
    }

    // when player collides
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // play power up sound effect tbd
            //AudioManager.instance.Play("PowerUpSound");
            
            // get powerUp and trigger PowerUpManager to handle the interaction
            PowerUp powerUp = other.GetComponent<PowerUp>();  
            PowerUpManager powerUpManager = other.GetComponent<PowerUpManager>();

            if (powerUpManager != null && powerUp != null)
            {
            powerUpManager.handlePowerUpInteraction(powerUp, other.GetComponent<PlayerController>());
            }
        }
    }

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

    public int getItemCount()
    {
        int count = 0;

        // Loop through each slot in the inventory and count the occupied ones
        foreach (var slot in itemSlot)
        {
            if (slot.isOccupied) // If the slot is occupied, increment the count
            {
                count++;
            }
        }

        return count;
    }   

    private void toggleInventory()
    {
        if(menuActivated)
        {
            Time.timeScale = 1;
            inventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if(!menuActivated)
        {
            Time.timeScale = 0;
            inventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }
}


