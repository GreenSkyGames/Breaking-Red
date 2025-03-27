using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    // item data
    public string itemName;
    public Sprite itemSprite;
    public bool isOccupied = false;

    //item slot
    [SerializeField] private Image itemImage;
    
    public void updateInventoryUI(string itemName, Sprite itemSprite)
    {
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        isOccupied = true;

        itemImage.sprite = itemSprite;
    }
}
