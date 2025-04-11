/*// ===========================
// Edit Mode Tests
// ===========================
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;


using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EditModeTests
{
    [Test]
    public void TreeSprite_HasCorrectTag()
    {
        GameObject tree = new GameObject("Tree");
        tree.tag = "Tree";
        Assert.AreEqual("Tree", tree.tag);
    }

    [Test]
    public void TreeSprite_HasSpriteRenderer()
    {
        GameObject tree = new GameObject("Tree");
        var renderer = tree.AddComponent<SpriteRenderer>();
        Assert.IsNotNull(tree.GetComponent<SpriteRenderer>());
    }

    [Test]
    public void TreeSprite_PrefabAssigned()
    {
        GameObject tree = new GameObject("Tree");
        PrefabUtility.SaveAsPrefabAsset(tree, "Assets/Tree.prefab");
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Tree.prefab");
        Assert.IsNotNull(prefab);
    }

    [Test]
    public void Tilemap_HasCollider()
    {
        GameObject tilemap = new GameObject("Tilemap");
        tilemap.AddComponent<TilemapCollider2D>();
        Assert.IsNotNull(tilemap.GetComponent<TilemapCollider2D>());
    }

    [Test]
    public void Tilemap_HasCorrectLayer()
    {
        GameObject tilemap = new GameObject("Tilemap");
        tilemap.layer = LayerMask.NameToLayer("Ground");
        Assert.AreEqual(LayerMask.NameToLayer("Ground"), tilemap.layer);
    }

    [Test]
    public void Tilemap_IsStatic()
    {
        GameObject tilemap = new GameObject("Tilemap");
        GameObjectUtility.SetStaticEditorFlags(tilemap, StaticEditorFlags.NavigationStatic);
        Assert.IsTrue(GameObjectUtility.GetStaticEditorFlags(tilemap).HasFlag(StaticEditorFlags.NavigationStatic));
    }

    [Test]
    public void Spike_HasDangerTag()
    {
        GameObject spike = new GameObject("Spike");
        spike.tag = "Danger";
        Assert.AreEqual("Danger", spike.tag);
    }

    [Test]
    public void Spike_HasCorrectSprite()
    {
        GameObject spike = new GameObject("Spike");
        var sr = spike.AddComponent<SpriteRenderer>();
        sr.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Spike.png");
        Assert.IsNotNull(sr.sprite);
    }

    [Test]
    public void SpikePrefab_IsAssignedCorrectly()
    {
        GameObject spike = new GameObject("Spike");
        PrefabUtility.SaveAsPrefabAsset(spike, "Assets/Spike.prefab");
        var loaded = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Spike.prefab");
        Assert.IsNotNull(loaded);
    }

    [Test]
    public void Passageway_HasTriggerCollider()
    {
        GameObject passage = new GameObject("Passage");
        var collider = passage.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        Assert.IsTrue(collider.isTrigger);
    }

    [Test]
    public void Passageway_HasCorrectScript()
    {
        GameObject passage = new GameObject("Passage");
        var script = passage.AddComponent<MonoBehaviour>();
        Assert.IsNotNull(script);
    }

    [Test]
    public void Passageway_HasCorrectTag()
    {
        GameObject passage = new GameObject("Passage");
        passage.tag = "Passage";
        Assert.AreEqual("Passage", passage.tag);
    }

    [Test]
    public void Platform_HasKinematicBody()
    {
        GameObject platform = new GameObject("Platform");
        var rb = platform.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        Assert.AreEqual(RigidbodyType2D.Kinematic, rb.bodyType);
    }

    [Test]
    public void Platform_HasBoxCollider()
    {
        GameObject platform = new GameObject("Platform");
        var collider = platform.AddComponent<BoxCollider2D>();
        Assert.IsNotNull(collider);
    }

    [Test]
    public void PlatformPrefab_IsAssigned()
    {
        GameObject platform = new GameObject("Platform");
        PrefabUtility.SaveAsPrefabAsset(platform, "Assets/Platform.prefab");
        var loaded = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Platform.prefab");
        Assert.IsNotNull(loaded);
    }
}*/

// ===========================
// Play Mode Tests
// ===========================

/*public class PlayModeTests
{
    //[UnityTest]
    //public IEnumerator TreeSprite_RemainsStaticOverTime() { /* ... see above ... */ //yield return null; }
    //[UnityTest]
    //public IEnumerator TreeBlocksPlayer_PlayerCannotPassThrough() { /* ... */ yield return null; }
    //[UnityTest]
    //public IEnumerator TreeDestroysOnFireTagCollision() { /* ... */ yield return null; }

    //[UnityTest]
    //public IEnumerator PlayerWalksOnTilemap_DoesNotFallThrough() { /* ... */ yield return null; }
    //[UnityTest]
    //public IEnumerator TileDisappears_OnTrigger() { /* ... */ yield return null; }
    //[UnityTest]
    //public IEnumerator TilemapBlocksMovement_PlayerStops() { /* ... */ yield return null; }

    //[UnityTest]
    //public IEnumerator PlayerDies_OnContactWithSpike() { /* ... */ yield return null; }
    //[UnityTest]
    //public IEnumerator Spike_IsStationaryDuringPlay() { /* ... */ yield return null; }
    //[UnityTest]
    //public IEnumerator SpikeLaunches_TriggersPhysicsForce() { /* ... */ yield return null; }

    //[UnityTest]
    //public IEnumerator PassagewayTrigger_TriggersSceneLoad() { /* ... */ yield return null; }
    //[UnityTest]
    //public IEnumerator PassagewayCooldown_PreventsDoubleTrigger() { /* ... */ yield return null; }
    //[UnityTest]
    //public IEnumerator PlayerTeleports_OnPassagewayEntry() { /* ... */ yield return null; }

    //[UnityTest]
    //public IEnumerator PlayerMovesWithPlatform_StaysOnTop() { /* ... */ yield return null; }
    //[UnityTest]
    //public IEnumerator PlatformStopsAtLimit() { /* ... */ yield return null; }
    //[UnityTest]
    //public IEnumerator PlatformReturnsToStart() { /* ... */ yield return null; }
//}
