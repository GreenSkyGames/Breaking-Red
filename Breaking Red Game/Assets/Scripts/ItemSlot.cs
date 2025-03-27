using UnityEngine;
using UnityEngine.UI;

/* Name: Shan Peck
*	Role: Team Lead 4 -- Project Manager
*	
*	This file contains the definition for the ItemSlot class
*   It controls what the item slot looks like
*	It inherits from MonoBehaviour */

public class ItemSlot : MonoBehaviour
{
    /* item data */
    public string itemName;
    public Sprite itemSprite;
    public bool isOccupied = false;
    
    [SerializeField] private Image itemImage; // item slot
    
    /* This function attachs the item name and sprite to the slot so we can see it
     * If an item is added, it makes isOccupied true */
    public void updateInventoryUI(string itemName, Sprite itemSprite)
    {
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        isOccupied = true;

        itemImage.sprite = itemSprite;
    }
}
