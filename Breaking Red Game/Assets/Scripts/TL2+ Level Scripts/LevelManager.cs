using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    /*private LevelLoader currentLevelLoader;
    public GameObject levelParent;

    public void StartLevel1()
    {
        StartLevel1 level1 = newLevel1();
        level1.normalPassageSprite = Resources.Load<Sprite>("Sprites/NormalPassage");
        level1.lockedPassageSprite = Resources.Load<Sprite>("Sprites/LockedPassage");

        level1.LoadLevel(levelParent);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
    /*
    public Transform levelParent;
    private void Start()
    {
        LoadLevel(new Level1());
    }

    public void LoadLevel(LevelLoader level)
    {
        level.LoadLevel();
        foreach (TerrainObjects obj in level.terrainList)
        {
            GameObject go = new GameObject("TerrainObject");
            go.transform.parent = levelParent;

            go.transform.position = obj.position;
            SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
            renderer.sprite = Resources.Load<Sprite>("Sprites/" + obj.spriteName);
            go.transform.parent = levelParent;

            if(obj is NormalPassage)
            {
                go.AddComponent<NormalPassage>();
            }
        }
    }*/

    public LevelLoader levelLoader;
    public GameObject player;
    public GameObject normalPassagePrefab;
    public GameObject lockedPassagePrefab;
    public GameObject damagingEnvPrefab;
    public GameObject slidingDoorPrefab;

    private static Dictionary<string, LevelLoader> loadedLevels = new Dictionary<string, LevelLoader>();

    public static LevelLoader GetOrCreateLevel(LevelLoader requestedLevel)
    {
        string levelID = requestedLevel.LevelID;
        if (loadedLevels.ContainsKey(levelID))
        {
            return loadedLevels[levelID];
        }
        else
        {
            loadedLevels[levelID] = requestedLevel;
            return requestedLevel;
        }
    }
    private void Start()
    {
        levelLoader = new Level1();
        levelLoader.LoadLevel(normalPassagePrefab, lockedPassagePrefab, damagingEnvPrefab, slidingDoorPrefab);
    }

    public void TrasnitionToNextLevel(string tag)
    {
        levelLoader = levelLoader.GetNextLevel(tag);
        levelLoader.LoadLevel(normalPassagePrefab, lockedPassagePrefab, damagingEnvPrefab, slidingDoorPrefab);
    }
}
