using UnityEngine;

public class ClueInventoryUIManager : MonoBehaviour
{
    public GameObject[] itemSlots; // add in inspector
    private ClueInventory clueInventory; // don't need to drag
    public int cluesGathered;

    void Start()
    {
        clueInventory = new ClueInventory(); // initializw
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
