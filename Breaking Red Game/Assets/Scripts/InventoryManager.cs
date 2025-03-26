using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public GameObject InventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            toggleInventory();
        }
    }

    private void toggleInventory()
    {
        if(menuActivated)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if(!menuActivated)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

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
}

