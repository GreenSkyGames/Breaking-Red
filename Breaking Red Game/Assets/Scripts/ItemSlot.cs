using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/* Name: Shan Peck
*	Role: Team Lead 4 -- Project Manager
*	
*	This file contains the definition for the ItemSlot class
*   It controls what the item slot looks like
*	It inherits from MonoBehaviour */

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    /* item data */
    public string itemName;
    public Sprite itemSprite;
    public bool isOccupied = false;
    public string itemDescription;
    public GameObject selectedShader;
    public bool thisItemSelected;
    
    public Image itemDescriptionImage;
    public TMP_Text ItemNameText;
    public TMP_Text itemDescriptionText;

    public Image itemImage; // item slot
    private InventoryManager inventoryManager;
    
    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }
    /* This function attachs the item name and sprite to the slot so we can see it
     * If an item is added, it makes isOccupied true */
    public void updateInventoryUI(string itemName, Sprite itemSprite, string itemDescription)
    {
        if (itemImage == null) // Check if the image is null (destroyed)
        {
            Debug.LogWarning("Item image is missing or has been destroyed!");
            return; // Prevent further execution if the itemImage is null
        }
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isOccupied = true;

        itemImage.sprite = itemSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;
        ItemNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite;

        inventoryManager.selectedSlot = this;

    }

    public void OnRightClick()
    {

    }
}
