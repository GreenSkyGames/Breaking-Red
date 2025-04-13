using System.Collections.Generic;
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
    public GameObject movingPlatformPrefab;
    public GameObject movingPlatformTilePrefab;
    public GameObject slidingDoorPrefab;
    private Dictionary<string, bool> loadedLevels = new Dictionary<string, bool>();
    public static LevelManager Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        LoadLevel("L1");
        //levelLoader = new Level1();
        //levelLoader.LoadLevel(normalPassagePrefab, lockedPassagePrefab, damagingEnvPrefab, movingPlatformPrefab, slidingDoorPrefab);
        //InitializeLevel();
    }

    public void tryLoadLevel(string tag)
    {
        if (!loadedLevels.ContainsKey(tag) || !loadedLevels[tag])
        {
            Debug.Log("Loading " + tag);
            LoadLevel(tag);
        }
    }

    private void LoadLevel(string levelTag)
    {
        LevelLoader levelLoader = null;
        switch (levelTag)
        {
            case "L1":
                levelLoader = new Level1();

                break;
            case "L2":
                Debug.Log("Successfully Loaded Level 2");
                levelLoader = new Level2();
                break;
        }
        if (levelLoader != null)
        {
            levelLoader.loadLevel(normalPassagePrefab, lockedPassagePrefab, damagingEnvPrefab, movingPlatformPrefab, movingPlatformTilePrefab, slidingDoorPrefab);
            loadedLevels[levelTag] = true;
        }
        else
        {
            Debug.LogWarning($"No loader defined for leveltag: {levelTag}");
        }
    }
}