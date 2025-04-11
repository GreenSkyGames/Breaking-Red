using UnityEngine;
using System.Collections.Generic;

public abstract class LevelLoader
{
    protected void CreateNormalPassage(GameObject prefab, string spriteName, Vector3 position, string tag, float rotationZ = 0f)
    {
        GameObject obj = Object.Instantiate(prefab, position, Quaternion.Euler(0f, 0f, rotationZ));
        obj.tag = tag;
        obj.GetComponent<TerrainObjects>().SetSprite(spriteName);
    }

    protected void CreateLockedPassage(GameObject prefab, string spriteName, Vector3 position, GameObject door1, GameObject door2, string tag)
    {
        GameObject obj = Object.Instantiate(prefab, position, Quaternion.identity);
        obj.tag = tag;
        obj.GetComponent<TerrainObjects>().SetSprite(spriteName);
        LockedPassage passage = obj.GetComponent<LockedPassage>();
        if (passage != null)
        {
            passage.AssignSlidingDoors(door1, door2);
        }
    }

    protected void CreateDamagingEnv(GameObject prefab, string spriteName, Vector3 position, float rotationZ = 0f)
    {
        GameObject obj = Object.Instantiate(prefab, position, Quaternion.Euler(0f, 0f, rotationZ));
        obj.GetComponent<TerrainObjects>().SetSprite(spriteName);
    }

    protected GameObject CreateSlidingDoor(GameObject prefab, string spriteName, Vector3 position, float horGoal, float vertGoal, float moveTime)
    {
        GameObject obj = Object.Instantiate(prefab, position, Quaternion.identity);
        obj.GetComponent<TerrainObjects>().SetSprite(spriteName);
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
        platform.tag = "MovingPlatform";
        MovingPlatform mp = platform.GetComponent<MovingPlatform>();
        if (mp != null)
        {
            mp.SetMovementGoals(moveX, moveY, moveTime);
        }

        foreach (Vector2 offset in tileOffsets)
        {
            GameObject tile = GameObject.Instantiate(tilePrefab, platform.transform);
            tile.transform.localPosition = new Vector3(offset.x, offset.y, 0);

            //tile.tag = "MovingPlatform";

            if (tile.GetComponent<Collider2D>() == null)
            {
                tile.AddComponent<BoxCollider2D>();
            }
        }

        return platform;
    }

    public abstract void LoadLevel(GameObject normalPassagePrefab, GameObject lockedPassagePrefab, GameObject damagingEnvPrefab, GameObject movingPlatformPrefab, GameObject movingPlatformTilePrefab, GameObject slidingDoorPrefab);
};