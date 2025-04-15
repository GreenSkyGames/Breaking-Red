using UnityEngine;

public class ClueInventoryUIManager : MonoBehaviour
{
    public GameObject[] itemSlots; // add in inspector
    private InventoryBase inventoryBase; // don't need to drag
    public int cluesGathered;

    void Start()
    {
        inventoryBase = new ClueInventory(); // initialize
        updateSlotVisibility();
    }

    public int GetCurrentClueInventorySize()
    {
        return inventoryBase.v_getInventorySize(cluesGathered);
    }

    public void updateSlotVisibility()
    {
        int size = inventoryBase.v_getInventorySize(cluesGathered);

        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].SetActive(i < size);
        }
        InventoryManager.sInstance.maxInventorySize = size;
        Debug.Log($"changed maxInventorySize to " + InventoryManager.sInstance.maxInventorySize);
    }
}
