using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Level2 : LevelLoader
{
    /*public GameObject normalPassagePrefab;
    public GameObject lockedPassagePrefab;
    public GameObject damagingEnvPrefab;
    public GameObject slidingDoorPrefab;*/
    public GameObject movingPlatformTilePrefab;
    public override void LoadLevel(GameObject normalPassagePrefab, GameObject lockedPassagePrefab, GameObject damagingEnvPrefab, GameObject movingPlatformPrefab, GameObject movingPlatformTilePrefab, GameObject slidingDoorPrefab)
    {
        var tileOffsets = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(tileOffsets, -3, 11, -4, -1);    // Base platform
        AddRectTiles(tileOffsets, 5, 11, 0, 5);       // Raised section
        AddRectTiles(tileOffsets, 12, 15, 1, 5);      // Top-right section
        AddRectTiles(tileOffsets, 16, 17, 3, 5);      // Tiny end piece

        CreateCompositePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(13.465f, 0.9112f, 0f), tileOffsets, 19f, 0f, 7f);
    }

    private void AddRectTiles(System.Collections.Generic.List<Vector2> list, int xMin, int xMax, int yMin, int yMax)
    {
        for (int x = xMin; x <= xMax; x++)
        {
            for (int y = yMin; y <= yMax; y++)
            {
                list.Add(new Vector2(x, y));
            }
        }
    }


    public GameObject CreateCompositePlatform(GameObject platformPrefab, GameObject tilePrefab, Vector3 worldPosition, System.Collections.Generic.List<Vector2> tileOffsets, float moveX, float moveY, float moveTime)
    {
        GameObject platform = GameObject.Instantiate(platformPrefab, worldPosition, Quaternion.identity);

        MovingPlatform mp = platform.GetComponent<MovingPlatform>();
        if (mp != null)
        {
            mp.SetMovementGoals(moveX, moveY, moveTime);
        }

        foreach (Vector2 offset in tileOffsets)
        {
            GameObject tile = GameObject.Instantiate(tilePrefab, platform.transform);
            tile.transform.localPosition = new Vector3(offset.x, offset.y, 0);
        }

        return platform;
    }

}