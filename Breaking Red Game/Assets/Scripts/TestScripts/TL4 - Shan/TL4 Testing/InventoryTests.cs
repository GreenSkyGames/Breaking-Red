//using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class InventoryTests: MonoBehaviour
{/*
    private InventoryManager inventoryManager;

    [SetUp]
    public void SetUp()
    {
        inventoryManager = InventoryManager.sInstance;
    }

    // ----- Logic Tests -----
    [Test]
    public void InventoryBaseReturnsDefaultSize()
    {
        InventoryBase inv = new InventoryBase();
        Assert.AreEqual(3, inv.v_getInventorySize(0));
        Assert.AreEqual(3, inv.v_getInventorySize(10));
    }

    [Test]
    public void ClueInventoryReturnsCorrectSizes()
    {
        ClueInventory inv = new ClueInventory();
        Assert.AreEqual(3, inv.v_getInventorySize(0));
        Assert.AreEqual(4, inv.v_getInventorySize(2));
        Assert.AreEqual(5, inv.v_getInventorySize(4));
    }

    [Test]
    public void BaseReferenceToSubclassUsesOverrideMethod()
    {
        InventoryBase inv = new ClueInventory(); // now polymorphism applies
        Assert.AreEqual(5, inv.v_getInventorySize(5)); // should use override logic
        Assert.AreEqual(4, inv.v_getInventorySize(2));
        Assert.AreEqual(3, inv.v_getInventorySize(0));
    }

    [Test]
    public void AddToInventoryIncreasesItemCount()
    {
        var sprite = Sprite.Create(Texture2D.blackTexture, new Rect(0, 0, 10, 10), Vector2.zero);
        inventoryManager.addToInventory("TestItem", sprite, "Test description");

        Assert.AreEqual(1, inventoryManager.getItemCount());
    }

    [Test]
    public void InventoryDoesNotExceedMax()
    {
        var sprite = Sprite.Create(Texture2D.blackTexture, new Rect(0, 0, 10, 10), Vector2.zero);
        int maxSlots = inventoryManager.itemSlot.Length;

        for (int i = 0; i < maxSlots + 2; i++)  // try to overfill
        {
            inventoryManager.addToInventory("OverfillItem", sprite, "Overflow test");
        }

        Assert.AreEqual(maxSlots, inventoryManager.getItemCount());
    }*/
}
