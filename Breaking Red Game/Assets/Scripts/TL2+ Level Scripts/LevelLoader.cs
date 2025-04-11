using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public abstract class LevelLoader
{
    public virtual void LoadLevel(GameObject normalPassagePrefab, GameObject lockedPassagePrefab, GameObject damagingEnvPrefab, GameObject slidingDoorPrefab)
    {
        Debug.Log("Default Level Loader used");
    }
    public virtual LevelLoader GetNextLevel(string tag)
    {
        switch (tag)
        {
            case "IL1": 
                return new Level1();
            case "L2": 
                return new Level2();
            default: 
                Debug.LogWarning("Unknown level tag: " + tag); 
                return this;
        }
    }
};
