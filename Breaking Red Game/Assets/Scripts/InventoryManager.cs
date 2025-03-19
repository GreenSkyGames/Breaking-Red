using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public GameObject InventoryMenu;
    private bool menuActivated;
    //public List<InventoryItem> inventory;  // list power ups
    //public int maxSlots = 3;  // initial inventory size
    public ItemSlot[] itemSlot;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && menuActivated)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if(Input.GetKeyDown(KeyCode.I) && !menuActivated)
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

        //inventory = new List<InventoryItem>(maxSlots);  // initialize inventory
    }

    public void AddToInventory(string itemName, Sprite itemSprite)
    {
        /*
        if (powerUp == null)
        {
            Debug.LogError("Inventory is null");
            //inventory = new List<InventoryItem>(maxSlots);
            return;
        }
    
        if (inventory.Count < maxSlots)  // check if empty slot
        {
            inventory.Add(new InventoryItem(itemName, itemSprite));  // Add the power-up to the inventory
            Debug.Log("Item added to inventory");
        }
        else
        {
            Debug.Log("Inventory full! Cannot add item.");
        }
        */
        for (int i = 0; i < 3; i++)
        {
            if(itemSlot[i].isFull == false)
            {
                itemSlot[i].AddToInventory(itemName, itemSprite);
                return;
            }
        }
    }

    /* increase inventory later?
    public void IncreaseInventorySlots(int additionalSlots)
    {
        maxSlots += additionalSlots;
        Debug.Log("Inventory size increased to: " + maxSlots);
    }
    */
}

