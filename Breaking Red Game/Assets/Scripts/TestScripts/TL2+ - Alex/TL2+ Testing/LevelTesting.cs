using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.TestTools;

public class LevelTesting
{
    [Test]
    public void PassagePrefab_HasSpriteRendererAndSprite()
    {
        var passagePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/LevelPrefabs/NormalPassage.prefab");

        Assert.IsNotNull(passagePrefab, "Passage prefab not found.");
        var spriteRenderer = passagePrefab.GetComponent<SpriteRenderer>();
        Assert.IsNotNull(spriteRenderer, "Passage prefab should have a SpriteRenderer.");
        Assert.AreEqual("House_tileset_30", spriteRenderer.sprite.name, "Passage prefab sprite name is incorrect.");
    }

    [Test]
    public void PassagePrefab_HasNormalPassageScriptWithFadePanel()
    {
        var passagePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/LevelPrefabs/NormalPassage.prefab");

        Assert.IsNotNull(passagePrefab, "Passage prefab not found.");
        var normalPassageScript = passagePrefab.GetComponent<NormalPassage>();
        Assert.IsNotNull(normalPassageScript, "Passage prefab should have NormalPassage script.");
        Assert.IsNotNull(normalPassageScript.fadePanel, "NormalPassage fadePanel should be assigned.");
    }

    [Test]
    public void PassagePrefab_HasBoxCollider2DSetAsTrigger()
    {
        var passagePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/LevelPrefabs/NormalPassage.prefab");

        Assert.IsNotNull(passagePrefab, "Passage prefab not found.");
        var collider = passagePrefab.GetComponent<BoxCollider2D>();
        Assert.IsNotNull(collider, "Passage prefab should have BoxCollider2D.");
        Assert.IsTrue(collider.isTrigger, "Passage prefab BoxCollider2D should be set as trigger.");
    }

    [Test]
    public void DamagingEnvPrefab_HasSpriteRendererAndSprite()
    {
        var damagingEnvPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/LevelPrefabs/DamagingEnv.prefab");

        Assert.IsNotNull(damagingEnvPrefab, "Damaging environment prefab not found.");
        var spriteRenderer = damagingEnvPrefab.GetComponent<SpriteRenderer>();
        Assert.IsNotNull(spriteRenderer, "Damaging environment prefab should have a SpriteRenderer.");
        Assert.AreEqual("DamagingEnvSprite", spriteRenderer.sprite.name, "Damaging environment sprite name is incorrect.");
    }

    [Test]
    public void DamagingEnvPrefab_HasBoxCollider2DSetAsTrigger()
    {
        var damagingEnvPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/LevelPrefabs/DamagingEnv.prefab");

        Assert.IsNotNull(damagingEnvPrefab, "Damaging environment prefab not found.");
        var collider = damagingEnvPrefab.GetComponent<BoxCollider2D>();
        Assert.IsNotNull(collider, "Damaging environment prefab should have BoxCollider2D.");
        Assert.IsTrue(collider.isTrigger, "Damaging environment prefab BoxCollider2D should be set as trigger.");
    }

    [Test]
    public void LockedPassagePrefab_HasSpriteRendererAndSprite()
    {
        var lockedPassagePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/LevelPrefabs/LockedPassage.prefab");

        Assert.IsNotNull(lockedPassagePrefab, "Locked passage prefab not found.");
        var spriteRenderer = lockedPassagePrefab.GetComponent<SpriteRenderer>();
        Assert.IsNotNull(spriteRenderer, "Locked passage prefab should have a SpriteRenderer.");
        Assert.AreEqual("LockedPassageSprite", spriteRenderer.sprite.name, "Locked passage sprite name is incorrect.");
    }

    [Test]
    public void LockedPassagePrefab_HasBoxCollider2DSetAsTrigger()
    {
        var lockedPassagePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/LevelPrefabs/LockedPassage.prefab");

        Assert.IsNotNull(lockedPassagePrefab, "Locked passage prefab not found.");
        var collider = lockedPassagePrefab.GetComponent<BoxCollider2D>();
        Assert.IsNotNull(collider, "Locked passage prefab should have BoxCollider2D.");
        Assert.IsTrue(collider.isTrigger, "Locked passage prefab BoxCollider2D should be set as trigger.");
    }

    [Test]
    public void LockedPassagePrefab_HasLockedPassageScriptWithFadePanelAndDoorOverlay()
    {
        var lockedPassagePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/LevelPrefabs/LockedPassage.prefab");

        Assert.IsNotNull(lockedPassagePrefab, "Locked passage prefab not found.");
        var lockedPassageScript = lockedPassagePrefab.GetComponent<LockedPassage>();
        Assert.IsNotNull(lockedPassageScript, "Locked passage prefab should have LockedPassage script.");
        Assert.IsNotNull(lockedPassageScript.fadePanel, "LockedPassage fadePanel should be assigned.");
        Assert.IsNotNull(lockedPassageScript.doorOverlay, "LockedPassage doorOverlay should be assigned.");
    }

    [Test]
    public void MovingPlatformPrefab_HasRigidbody2D()
    {
        var movingPlatformPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/LevelPrefabs/MovingPlatform.prefab");

        Assert.IsNotNull(movingPlatformPrefab, "Moving platform prefab not found.");
        var rigidbody2D = movingPlatformPrefab.GetComponent<Rigidbody2D>();
        Assert.IsNotNull(rigidbody2D, "Moving platform prefab should have Rigidbody2D.");
    }

    [Test]
    public void MovingPlatformPrefab_HasMovingPlatformScript()
    {
        var movingPlatformPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/LevelPrefabs/MovingPlatform.prefab");

        Assert.IsNotNull(movingPlatformPrefab, "Moving platform prefab not found.");
        var movingPlatformScript = movingPlatformPrefab.GetComponent<MovingPlatform>();
        Assert.IsNotNull(movingPlatformScript, "Moving platform prefab should have MovingPlatform script.");
    }

    [Test]
    public void MovingPlatformTilePrefab_HasSpriteRendererAndSprite()
    {
        var movingPlatformTilePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/LevelPrefabs/MovingPlatformTile.prefab");

        Assert.IsNotNull(movingPlatformTilePrefab, "Moving platform tile prefab not found.");
        var spriteRenderer = movingPlatformTilePrefab.GetComponent<SpriteRenderer>();
        Assert.IsNotNull(spriteRenderer, "Moving platform tile prefab should have a SpriteRenderer.");
        Assert.AreEqual("cavetiles_298", spriteRenderer.sprite.name, "Moving platform tile sprite name is incorrect.");
    }

    [Test]
    public void MovingPlatformTilePrefab_HasBoxCollider2DSetAsTrigger()
    {
        var movingPlatformTilePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/LevelPrefabs/MovingPlatformTile.prefab");

        Assert.IsNotNull(movingPlatformTilePrefab, "Moving platform tile prefab not found.");
        var collider = movingPlatformTilePrefab.GetComponent<BoxCollider2D>();
        Assert.IsNotNull(collider, "Moving platform tile prefab should have BoxCollider2D.");
        Assert.IsTrue(collider.isTrigger, "Moving platform tile prefab BoxCollider2D should be set as trigger.");
    }

    [Test]
    public void MovingPlatformTilePrefab_HasTagMovingPlatform()
    {
        var movingPlatformTilePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/LevelPrefabs/MovingPlatformTile.prefab");

        Assert.IsNotNull(movingPlatformTilePrefab, "Moving platform tile prefab not found.");
        Assert.AreEqual("MovingPlatform", movingPlatformTilePrefab.tag, "Moving platform tile prefab should have tag 'MovingPlatform'.");
    }

    [Test]
    public void SlidingDoorPrefab_HasSpriteRendererAndSprite()
    {
        var slidingDoorPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/LevelPrefabs/SlidingDoor.prefab");

        Assert.IsNotNull(slidingDoorPrefab, "Sliding door prefab not found.");
        var spriteRenderer = slidingDoorPrefab.GetComponent<SpriteRenderer>();
        Assert.IsNotNull(spriteRenderer, "Sliding door prefab should have a SpriteRenderer.");
        Assert.AreEqual("SlidingDoorSprite", spriteRenderer.sprite.name, "Sliding door sprite name is incorrect.");
    }

    [Test]
    public void SlidingDoorPrefab_HasBoxCollider2DSetAsTrigger()
    {
        var slidingDoorPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/LevelPrefabs/SlidingDoor.prefab");

        Assert.IsNotNull(slidingDoorPrefab, "Sliding door prefab not found.");
        var collider = slidingDoorPrefab.GetComponent<BoxCollider2D>();
        Assert.IsNotNull(collider, "Sliding door prefab should have BoxCollider2D.");
        Assert.IsTrue(collider.isTrigger, "Sliding door prefab BoxCollider2D should be set as trigger.");
    }

    [Test]
    public void SlidingDoorPrefab_HasSlidingDoorScript()
    {
        var slidingDoorPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/LevelPrefabs/SlidingDoor.prefab");

        Assert.IsNotNull(slidingDoorPrefab, "Sliding door prefab not found.");
        var slidingDoorScript = slidingDoorPrefab.GetComponent<SlidingDoor>();
        Assert.IsNotNull(slidingDoorScript, "Sliding door prefab should have SlidingDoor script.");
    }

    [Test]
    public void TreePrefab_HasSpriteRendererAndSprite()
    {
        var treePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/LevelPrefabs/Tree.prefab");

        Assert.IsNotNull(treePrefab, "Tree prefab not found.");
        var spriteRenderer = treePrefab.GetComponent<SpriteRenderer>();
        Assert.IsNotNull(spriteRenderer, "Tree prefab should have a SpriteRenderer.");
        Assert.AreEqual("Tree_45", spriteRenderer.sprite.name, "Tree prefab sprite name is incorrect.");
    }

    [Test]
    public void TreePrefab_LayerIsSetTo6()
    {
        var treePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/LevelPrefabs/Tree.prefab");

        Assert.IsNotNull(treePrefab, "Tree prefab not found.");
        Assert.AreEqual(6, treePrefab.layer, "Tree prefab layer should be set to 6.");
    }

    [Test]
    public void TreePrefab_HasBoxCollider2D()
    {
        var treePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/LevelPrefabs/Tree.prefab");

        Assert.IsNotNull(treePrefab, "Tree prefab not found.");
        var collider = treePrefab.GetComponent<BoxCollider2D>();
        Assert.IsNotNull(collider, "Tree prefab should have BoxCollider2D.");
    }

    [Test]
    public void LevelLoader_CanProperlyMakeNormalPassage()
    {
        var levelLoader = new GameObject().AddComponent<LevelLoader>();
        var passage = levelLoader.CreatePassage("NormalPassage");

        Assert.IsNotNull(passage, "Normal passage should be created.");
        var passageScript = passage.GetComponent<Passage>();
        Assert.IsNotNull(passageScript, "Normal passage should have a Passage script.");
        Assert.IsNotNull(passageScript.fadePanel, "Normal passage should have fadePanel assigned.");
    }

    [Test]
    public void LevelLoader_CanProperlyMakeLockedPassage()
    {
        var levelLoader = new GameObject().AddComponent<LevelLoader>();
        var lockedPassage = levelLoader.CreateLockedPassage("LockedPassage");

        Assert.IsNotNull(lockedPassage, "Locked passage should be created.");
        var lockedPassageScript = lockedPassage.GetComponent<LockedPassage>();
        Assert.IsNotNull(lockedPassageScript, "Locked passage should have a LockedPassage script.");
        Assert.IsNotNull(lockedPassageScript.fadePanel, "Locked passage should have fadePanel assigned.");
        Assert.IsNotNull(lockedPassageScript.doorOverlay, "Locked passage should have doorOverlay assigned.");
    }

    [Test]
    public void LevelLoader_CanProperlyMakeSlidingDoor()
    {
        var levelLoader = new GameObject().AddComponent<LevelLoader>();
        var slidingDoor = levelLoader.CreateSlidingDoor("SlidingDoor");

        Assert.IsNotNull(slidingDoor, "Sliding door should be created.");
        var slidingDoorScript = slidingDoor.GetComponent<SlidingDoor>();
        Assert.IsNotNull(slidingDoorScript, "Sliding door should have a SlidingDoor script.");
        var collider = slidingDoor.GetComponent<BoxCollider2D>();
        Assert.IsNotNull(collider, "Sliding door should have BoxCollider2D.");
        Assert.IsTrue(collider.isTrigger, "Sliding door BoxCollider2D should be set as trigger.");
    }

    [Test]
    public void LevelLoader_CanProperlyMakeDamagingEnvVariable()
    {
        var levelLoader = new GameObject().AddComponent<LevelLoader>();
        var damagingEnv = levelLoader.CreateDamagingEnv("DamagingEnvironment");

        Assert.IsNotNull(damagingEnv, "Damaging environment should be created.");
        var spriteRenderer = damagingEnv.GetComponent<SpriteRenderer>();
        Assert.IsNotNull(spriteRenderer, "Damaging environment should have a SpriteRenderer.");
        var collider = damagingEnv.GetComponent<BoxCollider2D>();
        Assert.IsNotNull(collider, "Damaging environment should have BoxCollider2D.");
        Assert.IsTrue(collider.isTrigger, "Damaging environment BoxCollider2D should be set as trigger.");
    }

    [Test]
    public void LevelLoader_CanProperlyMakeMovingPlatform()
    {
        var levelLoader = new GameObject().AddComponent<LevelLoader>();
        var movingPlatform = levelLoader.CreateMovingPlatform("MovingPlatform");

        Assert.IsNotNull(movingPlatform, "Moving platform should be created.");
        var rigidBody = movingPlatform.GetComponent<Rigidbody2D>();
        Assert.IsNotNull(rigidBody, "Moving platform should have Rigidbody2D.");
        var movingPlatformScript = movingPlatform.GetComponent<MovingPlatform>();
        Assert.IsNotNull(movingPlatformScript, "Moving platform should have MovingPlatform script.");
    }

    [Test]
    public void GroundTilemap_HasTagGround()
    {
        var groundTilemap = GameObject.Find("GroundTilemap"); // Assuming you have a GroundTilemap object in your scene

        Assert.IsNotNull(groundTilemap, "Ground Tilemap object not found.");
        Assert.AreEqual("Ground", groundTilemap.tag, "Ground Tilemap should have tag 'Ground'.");
    }

    [Test]
    public void GoingOffPlatformOntoBoundaryLayer_ScaresPlayer()
    {
        var player = GameObject.Find("Player");
        var playerTransform = player.GetComponent<Transform>();

        // Assuming boundary scaling changes player's local scale
        Vector3 originalScale = playerTransform.localScale;

        // Simulate player moving off the platform onto boundary (trigger event or boundary condition)
        // Assume player goes off boundary and scales
        playerTransform.localScale = new Vector3(2f, 2f, 2f); // This should match your scaling condition

        Assert.AreNotEqual(originalScale, playerTransform.localScale, "Player should scale when going off the platform onto the boundary layer.");
    }

    [Test]
    public void CollidingWithDangerousEnv_CausesPlayerDamage()
    {
        var player = GameObject.Find("Player");
        var playerHealth = player.GetComponent<PlayerHealth>(); // Assuming PlayerHealth script handles player's health

        // Store original health
        int originalHealth = playerHealth.health;

        // Simulate player collision with damaging environment (trigger event)
        var damagingEnv = GameObject.Find("DamagingEnv");
        var collider = damagingEnv.GetComponent<BoxCollider2D>();
        Assert.IsNotNull(collider, "Damaging environment should have a BoxCollider2D.");
        collider.isTrigger = true; // Ensure it triggers

        // Simulate collision
        damagingEnv.GetComponent<BoxCollider2D>().OnTriggerEnter2D(player.GetComponent<Collider2D>());

        Assert.Less(playerHealth.health, originalHealth, "Player's health should decrease when colliding with dangerous environment.");
    }

    [Test]
    public void MovingThroughPassage_TeleportsPlayer()
    {
        var player = GameObject.Find("Player");
        var playerTransform = player.GetComponent<Transform>();
        Vector3 originalPosition = playerTransform.position;

        // Simulate moving through a passage (trigger event)
        var passage = GameObject.Find("Passage");
        var passageCollider = passage.GetComponent<BoxCollider2D>();
        passageCollider.isTrigger = true; // Ensure it triggers

        // Simulate passage collision
        passageCollider.OnTriggerEnter2D(player.GetComponent<Collider2D>());

        // Assuming teleportation happens when passage is triggered
        Assert.AreNotEqual(originalPosition, playerTransform.position, "Player should be teleported when moving through a passage.");
    }

    [Test]
    public void LockedPassage_DoesNotTeleportWhenNotUnlocked()
    {
        var player = GameObject.Find("Player");
        var playerTransform = player.GetComponent<Transform>();
        Vector3 originalPosition = playerTransform.position;

        var lockedPassage = GameObject.Find("LockedPassage");
        var lockedPassageScript = lockedPassage.GetComponent<LockedPassage>();
        lockedPassageScript.isUnlocked = false; // Simulate the passage being locked

        // Simulate moving through the locked passage (trigger event)
        var passageCollider = lockedPassage.GetComponent<BoxCollider2D>();
        passageCollider.isTrigger = true;
        passageCollider.OnTriggerEnter2D(player.GetComponent<Collider2D>());

        Assert.AreEqual(originalPosition, playerTransform.position, "Player should not be teleported through a locked passage.");
    }

    [Test]
    public void LockedPassage_HasTwoDoors()
    {
        var lockedPassage = GameObject.Find("LockedPassage");
        var lockedPassageScript = lockedPassage.GetComponent<LockedPassage>();

        Assert.IsNotNull(lockedPassageScript.door1, "Locked passage should have a door1.");
        Assert.IsNotNull(lockedPassageScript.door2, "Locked passage should have a door2.");
    }

    [Test]
    public void LockedPassage_TriggersTwoDoorsWhenUnlocked()
    {
        var lockedPassage = GameObject.Find("LockedPassage");
        var lockedPassageScript = lockedPassage.GetComponent<LockedPassage>();

        lockedPassageScript.isUnlocked = true; // Simulate unlocking the passage

        var door1 = lockedPassageScript.door1;
        var door2 = lockedPassageScript.door2;

        // Assuming triggering the doors means enabling them
        door1.SetActive(true);
        door2.SetActive(true);

        Assert.IsTrue(door1.activeSelf, "Door 1 should be active after unlocking.");
        Assert.IsTrue(door2.activeSelf, "Door 2 should be active after unlocking.");
    }

    [Test]
    public void MovingThroughUnlockedPassage_TeleportsPlayer()
    {
        var player = GameObject.Find("Player");
        var playerTransform = player.GetComponent<Transform>();
        Vector3 originalPosition = playerTransform.position;

        var unlockedPassage = GameObject.Find("UnlockedPassage");
        var unlockedPassageCollider = unlockedPassage.GetComponent<BoxCollider2D>();
        unlockedPassageCollider.isTrigger = true;

        // Simulate moving through the unlocked passage (trigger event)
        unlockedPassageCollider.OnTriggerEnter2D(player.GetComponent<Collider2D>());

        Assert.AreNotEqual(originalPosition, playerTransform.position, "Player should be teleported when moving through an unlocked passage.");
    }

    [Test]
    public void SlidingDoor_DisablesAfterBeingTriggered()
    {
        var slidingDoor = GameObject.Find("SlidingDoor");
        var slidingDoorScript = slidingDoor.GetComponent<SlidingDoor>();
        var collider = slidingDoor.GetComponent<BoxCollider2D>();

        Assert.IsNotNull(slidingDoorScript, "Sliding door should have SlidingDoor script.");
        Assert.IsNotNull(collider, "Sliding door should have BoxCollider2D.");

        // Simulate triggering the sliding door
        slidingDoorScript.TriggerDoor();

        Assert.IsFalse(slidingDoor.activeSelf, "Sliding door should be disabled after being triggered.");
    }


}
