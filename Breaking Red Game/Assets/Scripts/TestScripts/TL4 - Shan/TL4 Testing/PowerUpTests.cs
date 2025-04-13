using NUnit.Framework;
using UnityEngine;

public class PowerUpTests
{
    private GameObject playerObject;
    private PlayerController playerController;
    private PlayerHealth playerHealth;
    private InventoryManager inventoryManager;

    [SetUp]
    public void SetUp()
    {
        playerObject = new GameObject();
        playerController = playerObject.AddComponent<PlayerController>();
        playerHealth = playerObject.AddComponent<PlayerHealth>();
        inventoryManager = InventoryManager.sInstance;
    }

    // --- Golden Apple Tests ---
    [Test]
    public void GoldenAppleIncreasesHealth()
    {
        playerHealth.currentHealth = 90;
        GoldenApple goldenApple = new GameObject().AddComponent<GoldenApple>();
        goldenApple.effectAmount = 10;

        goldenApple.applyEffect(playerController);

        Assert.AreEqual(playerHealth.currentHealth, playerHealth.maxHealth);
    }

    [Test]
    public void GoldenAppleDoesNotIncreaseWhenHealthFull()
    {
        playerHealth.currentHealth = playerHealth.maxHealth;
        GoldenApple goldenApple = new GameObject().AddComponent<GoldenApple>();
        goldenApple.effectAmount = 10;

        goldenApple.applyEffect(playerController);

        Assert.AreEqual(playerHealth.maxHealth, playerHealth.currentHealth);
    }

    // --- Poison Apple Tests ---
    [Test]
    public void PoisonAppleDecreasesHealth()
    {
        int initialHealth = playerHealth.currentHealth;
        PoisonApple poisonApple = new GameObject().AddComponent<PoisonApple>();
        poisonApple.effectAmount = 10;

        poisonApple.applyEffect(playerController);

        Assert.AreEqual(initialHealth - 10, playerHealth.currentHealth);
    }

    [Test]
    public void PoisonAppleDoesNotGoNegative()
    {
        playerHealth.currentHealth = 5;
        PoisonApple poisonApple = new GameObject().AddComponent<PoisonApple>();
        poisonApple.effectAmount = 10;

        poisonApple.applyEffect(playerController);

        Assert.AreEqual(0, playerHealth.currentHealth);
    }

    // --- Red Shoes Tests (Speed Boost) ---
    [Test]
    public void RedShoesIncreasesSpeed()
    {
        float initialSpeed = playerController.speed;
        RedShoes redShoes = new GameObject().AddComponent<RedShoes>();
        redShoes.effectAmount = 5;

        redShoes.applyEffect(playerController);

        Assert.AreEqual(initialSpeed + 5, playerController.speed);
    }

    // --- Berserker Brew Tests (Damage Boost) ---
    [Test]
    public void BerserkerBrewIncreasesDamage()
    {
        float initialDamage = playerController.attackDamage;
        BerserkerBrew berserkerBrew = new GameObject().AddComponent<BerserkerBrew>();
        berserkerBrew.effectAmount = 10;

        berserkerBrew.applyEffect(playerController);

        Assert.AreEqual(initialDamage + 10, playerController.attackDamage);
    }

     // --- Owls Wing Tests (Inventory Management) ---
    [Test]
    public void OwlsWingIncreasesInventoryCount()
    {
        OwlsWing owlsWingPrefab = Resources.Load<OwlsWing>("TL4 Items/OwlsWing");

        Assert.IsNotNull(owlsWingPrefab);

        OwlsWing owlsWing = Object.Instantiate(owlsWingPrefab);

        int initialCount = inventoryManager.getItemCount();

        inventoryManager.addToInventory(owlsWing.itemType.ToString(), owlsWing.sprite, owlsWing.itemDescription);

        Assert.AreEqual(initialCount + 1, inventoryManager.getItemCount());
    }

    [Test]
    public void OwlsWing_InventoryFull_DoesNotAddItem()
    {
        OwlsWing owlsWingPrefab = Resources.Load<OwlsWing>("TL4 Items/OwlsWing");

        Assert.IsNotNull(owlsWingPrefab);

        for (int i = 0; i < inventoryManager.itemSlot.Length; i++)
        {
            OwlsWing fillerOwlsWing = Object.Instantiate(owlsWingPrefab);
            inventoryManager.addToInventory(fillerOwlsWing.itemType.ToString(), fillerOwlsWing.sprite, fillerOwlsWing.itemDescription);
        }

        int initialCount = inventoryManager.getItemCount();

        OwlsWing owlsWing = Object.Instantiate(owlsWingPrefab);
        inventoryManager.addToInventory(owlsWing.itemType.ToString(), owlsWing.sprite, owlsWing.itemDescription);

        Assert.AreEqual(initialCount, inventoryManager.getItemCount());
    }

    // --- Can of Tuna Tests (Inventory and Health Effect) ---
    [Test]
    public void CanOfTunaAddedToInventory()
    {
        CanOfTuna tunaPrefab = Resources.Load<CanOfTuna>("TL4 Items/CanOfTuna");

        Assert.IsNotNull(tunaPrefab);

        CanOfTuna tuna = Object.Instantiate(tunaPrefab);

        int initialInventoryCount = inventoryManager.getItemCount();

        inventoryManager.addToInventory(tuna.itemType.ToString(), tuna.sprite, tuna.itemDescription);

        Assert.AreEqual(initialInventoryCount + 1, inventoryManager.getItemCount());
    }

    [Test]
    public void CanOfTunaDoesNotAddItemWhenInventoryFull()
    {
        inventoryManager.itemSlot = new ItemSlot[3];
        CanOfTuna tunaPrefab = Resources.Load<CanOfTuna>("TL4 Items/CanOfTuna");
        
        Assert.IsNotNull(tunaPrefab);

        for (int i = 0; i < inventoryManager.itemSlot.Length; i++)
        {
            CanOfTuna fillerTuna = Object.Instantiate(tunaPrefab);
            inventoryManager.addToInventory(fillerTuna.itemType.ToString(), fillerTuna.sprite, fillerTuna.itemDescription);
        }

        int initialCount = inventoryManager.getItemCount();

        CanOfTuna tuna = Object.Instantiate(tunaPrefab);
        inventoryManager.addToInventory(tuna.itemType.ToString(), tuna.sprite, tuna.itemDescription);

        Assert.AreEqual(initialCount, inventoryManager.getItemCount());
    }   

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(playerObject);
    }
}

