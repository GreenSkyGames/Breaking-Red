using UnityEngine;
using System.Collections.Generic;

public abstract class LevelLoader
{
    /*public List<TerrainObjects> terrainObjects;
    public LevelLoader()
    {
        terrainObjects = new List<TerrainObjects>();
    }

    public virtual void LoadLevel(GameObject levelParent)
    {
    }*/
    /*public List<TerrainObjects> terrainList = new List<TerrainObjects>();
    public virtual void LoadLevel()
    {
        Debug.Log("Error: auto Load level 1");
    }*/

    public abstract void LoadLevel(GameObject prefab);
};
