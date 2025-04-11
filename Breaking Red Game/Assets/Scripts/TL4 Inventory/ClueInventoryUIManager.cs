using UnityEngine;

public class ClueInventoryUIManager : MonoBehaviour
{
    public GameObject[] itemSlots; // All slots assigned in Unity
    public ClueInventory clueInventory;
    public int cluesGathered;

    void Start()
    {
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
