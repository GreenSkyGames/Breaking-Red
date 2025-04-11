using UnityEngine;

public class ClueInventoryUIManager : MonoBehaviour
{
    public GameObject[] itemSlots; // Assigned in Unity
    private ClueInventory clueInventory; // No need to drag in Inspector
    public int cluesGathered;

    void Start()
    {
        clueInventory = new ClueInventory(); // Initialize it manually
        updateSlotVisibility();
    }

    public void updateSlotVisibility()
    {
        int size = clueInventory.v_getInventorySize(cluesGathered);

        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].SetActive(i < size);
        }
    }
}
