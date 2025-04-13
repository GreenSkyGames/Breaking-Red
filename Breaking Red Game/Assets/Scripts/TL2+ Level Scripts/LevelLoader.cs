using UnityEngine;
using System.Collections.Generic;

public class LevelLoader
{
    protected void CreateNormalPassage(GameObject prefab, string spriteName, Vector3 position, string tag, float rotationZ = 0f)
    {
        GameObject obj = Object.Instantiate(prefab, position, Quaternion.Euler(0f, 0f, rotationZ));
        obj.tag = tag;
        obj.GetComponent<TerrainObjects>().setSprite(spriteName);
    }

    protected void CreateLockedPassage(GameObject prefab, string spriteName, Vector3 position, GameObject door1, GameObject door2, string tag)
    {
        GameObject obj = Object.Instantiate(prefab, position, Quaternion.identity);
        obj.tag = tag;
        obj.GetComponent<TerrainObjects>().setSprite(spriteName);
        LockedPassage passage = obj.GetComponent<LockedPassage>();
        if (passage != null)
        {
            passage.AssignSlidingDoors(door1, door2);
        }
    }

    protected void CreateDamagingEnv(GameObject prefab, string spriteName, Vector3 position, float rotationZ = 0f)
    {
        GameObject obj = Object.Instantiate(prefab, position, Quaternion.Euler(0f, 0f, rotationZ));
        obj.GetComponent<TerrainObjects>().setSprite(spriteName);
    }

    protected GameObject CreateSlidingDoor(GameObject prefab, string spriteName, Vector3 position, float horGoal, float vertGoal, float moveTime)
    {
        GameObject obj = Object.Instantiate(prefab, position, Quaternion.identity);
        obj.GetComponent<TerrainObjects>().setSprite(spriteName);
        //obj.GetComponent<SlidingDoor>().Initialize(verticalGoal, moveTime);
        SlidingDoor slidingDoor = obj.GetComponent<SlidingDoor>();
        if (slidingDoor != null)
        {
            slidingDoor.SetMoveGoals(horGoal, vertGoal, 1.0f);
        }
        /* Vector3 goal = new Vector3(position.x, position.y + verticalGoal, position.z);
        door.startPosition = position;
        door.endPosition = goal;
        door.speed = Mathf.Abs(verticalGoal / moveTime);*/
        return obj;
    }

    public virtual void AddRectTiles(List<Vector2> list, int xMin, int xMax, int yMin, int yMax)
    {
        for (int x = xMin; x <= xMax; x++)
        {
            for (int y = yMin; y <= yMax; y++)
            {
                list.Add(new Vector2(x, y));
            }
        }
    }


    public virtual GameObject CreatePlatform(GameObject platformPrefab, GameObject tilePrefab, Vector3 worldPosition, System.Collections.Generic.List<Vector2> tileOffsets, float moveX, float moveY, float moveTime)
    {
        GameObject platform = GameObject.Instantiate(platformPrefab, worldPosition, Quaternion.identity);
        MovingPlatform mp = platform.GetComponent<MovingPlatform>();
        if (mp != null)
        {
            mp.tag = "MovingPlatform";
            Debug.Log("tag set to " + mp.tag);
            mp.SetMovementGoals(moveX, moveY, moveTime);
        }

        foreach (Vector2 offset in tileOffsets)
        {
            GameObject tile = GameObject.Instantiate(tilePrefab, platform.transform);
            tile.transform.localPosition = new Vector3(offset.x, offset.y, 0);

            tile.tag = "MovingPlatform";

            if (tile.GetComponent<Collider2D>() == null)
            {
                tile.AddComponent<BoxCollider2D>();
            }
        }

        return platform;
    }

    public virtual void loadLevel(GameObject normalPassagePrefab, GameObject lockedPassagePrefab, GameObject damagingEnvPrefab, GameObject movingPlatformPrefab, GameObject movingPlatformTilePrefab, GameObject slidingDoorPrefab)
    {
        // This will auto-generate Level 1 when dynamically bound
        Debug.Log("Dynamically binding to LoadLevel: Loading Level 1.");

        // Creating Level 1 objects
        CreateNormalPassage(normalPassagePrefab, "House_tileset_30", new Vector3(-107.5f, -18.700003f, 0), "IL1");
        CreateNormalPassage(normalPassagePrefab, "House_Red_4", new Vector3(-1.5003f, 0.29983f, 0), "IL1.1");
        CreateNormalPassage(normalPassagePrefab, "House_red_5", new Vector3(-0.5003f, 0.29983f, 0), "IL1.1");

        // Example objects for Level 1
        GameObject door1 = CreateSlidingDoor(slidingDoorPrefab, "Tree_45", new Vector3(9.3881f, -7.380007f, 0), 0, 2f, 1f);
        GameObject door2 = CreateSlidingDoor(slidingDoorPrefab, "Tree_45", new Vector3(9.3881f, -9.220007f, 0), 0, -2f, 1f);

        CreateLockedPassage(lockedPassagePrefab, "cavetiles_2107", new Vector3(11.4997f, -8.70017f, 0), door1, door2, "L2");
        CreateDamagingEnv(damagingEnvPrefab, "Furniture_39", new Vector3(-110.5f, -10.703f, 0));

        // Additional Level 1 creations could go here...
        Debug.Log("Level 1 objects created.");
    }

    public void staticMethod(GameObject lockedPassagePrefab)
    {
        Vector3 position = new Vector3(-0.5003f, -10.29983f, 0);
        GameObject obj = Object.Instantiate(lockedPassagePrefab, position, Quaternion.identity);
        obj.tag = "L2";
        obj.GetComponent<TerrainObjects>().setSprite("cavetiles_2107");
    }

};