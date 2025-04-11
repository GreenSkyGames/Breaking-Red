using UnityEngine;
using System.Collections.Generic;

public abstract class LevelLoader
{
    protected void CreateNormalPassage(GameObject prefab, string spriteName, Vector3 position, string tag)
    {
        GameObject obj = Object.Instantiate(prefab, position, Quaternion.identity);
        obj.tag = tag;
        obj.GetComponent<TerrainObjects>().SetSprite(spriteName);
    }

    protected void CreateLockedPassage(GameObject prefab, string spriteName, Vector3 position, GameObject door1, GameObject door2)
    {
        GameObject obj = Object.Instantiate(prefab, position, Quaternion.identity);
        obj.GetComponent<TerrainObjects>().SetSprite(spriteName);
        LockedPassage passage = obj.GetComponent<LockedPassage>();
        if (passage != null)
        {
            passage.AssignSlidingDoors(door1, door2);
        }
    }

    protected void CreateDamagingEnv(GameObject prefab, string spriteName, Vector3 position)
    {
        GameObject obj = Object.Instantiate(prefab, position, Quaternion.identity);
        obj.GetComponent<TerrainObjects>().SetSprite(spriteName);
    }

    protected GameObject CreateSlidingDoor(GameObject prefab, string spriteName, Vector3 position, float verticalGoal, float moveTime)
    {
        GameObject obj = Object.Instantiate(prefab, position, Quaternion.identity);
        obj.GetComponent<TerrainObjects>().SetSprite(spriteName);
        //obj.GetComponent<SlidingDoor>().Initialize(verticalGoal, moveTime);
        /*SlidingDoor slidingDoor = obj.GetComponent<SlidingDoor>();
        if (slidingDoor != null)
        {
            slidingDoor.Initialize(verticalGoal, moveTime);
        }*/
        /* Vector3 goal = new Vector3(position.x, position.y + verticalGoal, position.z);
        door.startPosition = position;
        door.endPosition = goal;
        door.speed = Mathf.Abs(verticalGoal / moveTime);*/
        return obj;
    }
    public abstract void LoadLevel(GameObject normalPassagePrefab, GameObject lockedPassagePrefab, GameObject damagingEnvPrefab, GameObject movingPlatformPrefab, GameObject slidingDoorPrefab);
};