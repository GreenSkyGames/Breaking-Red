using UnityEngine;
using System.Collections.Generic;

public abstract class LevelLoader
{
    public abstract void LoadLevel(GameObject normalPassagePrefab, GameObject lockedPassagePrefab, GameObject damagingEnvPrefab, GameObject slidingDoorPrefab);
    public virtual LevelLoader GetNextLevel(string tag)
    {
        if(tag == "IL1.1")
        {
            return new Level1();
        }
        else
        {
            return new Level1();
        }
    }
};
