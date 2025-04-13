using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class AllInventoryTests: MonoBehaviour
{
    // ----- InventoryBase and ClueInventory Class Definitions -----
    public class InventoryBase
    {
        public virtual int v_getInventorySize(int level)
        {
            return 3; // Default size
        }
    }

    public class ClueInventory : InventoryBase
    {
        public override int v_getInventorySize(int level)
        {
            if (level == 0)
                return 3;
            else if (level == 2)
                return 4;
            else if (level == 4)
                return 5;
            return 3;
        }
    }

    // ----- UIManager Class Definition -----
    public class ClueInventoryUIManager : MonoBehaviour
    {
        public GameObject[] itemSlots;
        public int cluesGathered;

        public void updateSlotVisibility()
        {
            int size = getInventorySize(cluesGathered); // This logic uses the inventory size based on clues gathered
            for (int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i].SetActive(i < size);
            }
        }
    }
    // ----- Logic Tests -----
    [Test]
    public void InventoryBase_ReturnsDefaultSize()
    {
        InventoryBase inv = new InventoryBase();
        Assert.AreEqual(3, inv.v_getInventorySize(0));
        Assert.AreEqual(3, inv.v_getInventorySize(10));
    }

    [Test]
    public void ClueInventory_ReturnsCorrectSizes()
    {
        ClueInventory inv = new ClueInventory();
        Assert.AreEqual(3, inv.v_getInventorySize(0));
        Assert.AreEqual(4, inv.v_getInventorySize(2));
        Assert.AreEqual(5, inv.v_getInventorySize(4));
    }

    [Test]
    public void BaseReferenceToSubclass_UsesOverrideMethod()
    {
        InventoryBase inv = new ClueInventory(); // now polymorphism applies
        Assert.AreEqual(5, inv.v_getInventorySize(5)); // should use override logic
        Assert.AreEqual(4, inv.v_getInventorySize(2));
        Assert.AreEqual(3, inv.v_getInventorySize(0));
    }

    // ----- MonoBehaviour UI Slot Test -----
    [Test]
    public IEnumerator UIManager_SlotVisibilityReflectsInventorySize_WithPolymorphism()
    {
        GameObject[] dummySlots = new GameObject[5];
        for (int i = 0; i < dummySlots.Length; i++)
        {
            dummySlots[i] = new GameObject("Slot" + i);
            dummySlots[i].SetActive(false);
        }

        var go = new GameObject("UIManagerGO");
        var uiManager = go.AddComponent<ClueInventoryUIManager>();
        uiManager.itemSlots = dummySlots;
        uiManager.cluesGathered = 4; // should return 5 via override

        yield return null;

        uiManager.updateSlotVisibility();

        int activeCount = 0;
        foreach (var slot in dummySlots)
        {
            if (slot.activeSelf) activeCount++;
        }

        // Expect 5 active slots because override logic applies now
        Assert.AreEqual(5, activeCount);
    }
}
