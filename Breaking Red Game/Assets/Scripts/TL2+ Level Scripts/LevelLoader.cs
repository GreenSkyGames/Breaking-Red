using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public abstract class LevelLoader
{
    public virtual void LoadLevel(GameObject normalPassagePrefab, GameObject lockedPassagePrefab, GameObject damagingEnvPrefab, GameObject slidingDoorPrefab)
    {
        Debug.Log("Default Level Loader used");
    }

    public virtual string LevelID => "Base";
    public virtual LevelLoader GetNextLevel(string tag)
    {
        return new Level1();
    }

    public virtual Vector2 GetTagPosition(string tag) {
        switch (tag)
        {
            case "L1":
                return new Vector2(10.5f, -8.43f);
            case "IL1":
                return new Vector2(-1, -1f);
            case "IL1.1":
                return new Vector2(-107.66f, -17.32f);
            case "L2":
                return new Vector2(79.71f, -24.84f);
            case "IL2":
                return new Vector2(181.2f, -14.6f);
            case "L3":
                return new Vector2(287.23f, -0.26f);
            case "IL3":
                return new Vector2(317.29f, -61.84f);
            case "L4":
                return new Vector2(221.87f, -70.49f);
            case "L5":
                return new Vector2(30.21f, -118.86f);
            default:
                Debug.LogWarning("No destination set for tag: " + tag);
                return Vector2.zero;
        }
    }
};
