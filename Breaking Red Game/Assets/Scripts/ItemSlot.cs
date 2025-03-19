using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    // item data
    public string itemName;
    public Sprite itemSprite;
    public bool isFull;

    //item slot
    [SerializeField] private Image itemImage;
    
    public void AddToInventory(string itemName, Sprite itemSprite)
    {
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        isFull = true;

        itemImage.sprite = itemSprite;
    }
}
