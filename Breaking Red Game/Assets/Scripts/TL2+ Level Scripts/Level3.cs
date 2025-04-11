using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Level3 : LevelLoader
{
    public GameObject normalPassagePrefab;
    public GameObject lockedPassagePrefab;
    public GameObject damagingEnvPrefab;
    public GameObject slidingDoorPrefab;
    public GameObject movingPlatformTilePrefab;
    public override void LoadLevel(GameObject normalPassagePrefab, GameObject lockedPassagePrefab, GameObject damagingEnvPrefab, GameObject movingPlatformPrefab, GameObject movingPlatformTilePrefab, GameObject slidingDoorPrefab)
    {
        //CreateNormalPassage(normalPassagePrefab, "House_tileset_30", new Vector2(-0.83f, -3.14f), "L2");
        //GameObject door1 = CreateSlidingDoor(slidingDoorPrefab, "Tree_45", new Vector3(9.3881f, -7.380007f, 0), 0, 2f, 1f);
        //GameObject door2 = CreateSlidingDoor(slidingDoorPrefab, "Tree_45", new Vector3(9.3881f, -9.220007f, 0), 0, -2f, 1f);
        //CreateLockedPassage(lockedPassagePrefab, "cavetiles_2107", new Vector3(11.4997f, -8.70017f, 0), door1, door2, "L2");
        //CreateDamagingEnv(damagingEnvPrefab, "Furniture_39", new Vector3(-110.5f, -10.703f, 0));
        CreateNormalPassage(normalPassagePrefab, "", new Vector2(283.3861f, 0.01999998f), "L2");
        CreateNormalPassage(normalPassagePrefab, "", new Vector2(283.386f, -1f), "L3");
        CreateNormalPassage(normalPassagePrefab, "", new Vector2(277.1261f, -25.92f), "L4");
        CreateNormalPassage(normalPassagePrefab, "", new Vector2(277.1261f, -26.92f), "L4");
        CreateNormalPassage(normalPassagePrefab, "cavetiles_2107", new Vector2(364.266f, 13.3f), "S2");

        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(346.4f, -7.74f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(346.4f, -7.23f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(346.4f, -6.72f, 0));
        var L3toL2 = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(L3toL2, -3, -2, -1, 3);
        AddRectTiles(L3toL2, -1, 3, -1, 1);

        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(283.3561f, 0.2499995f, 0f), L3toL2, 7f, 0f, 4f);

        var I2toI3 = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(I2toI3, -3, 3, 0, 1);
        AddRectTiles(I2toI3, 0, 3, -2, -1);

        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(302.3161f, -29.73f, 0f), I2toI3, 8f, 0f, 4f);

        var L3toL4 = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(L3toL4, -3, 0 , -1, 2);
        AddRectTiles(L3toL4, -3, -2, 3, 4);

        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(285.3361f, -26.723f, 0f), L3toL4, -7f, 0f, 3f);

        var L2toL4 = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(L2toL4, -3, -3, 0, 2);
        AddRectTiles(L2toL4, -2, -1, -1, 3);
        AddRectTiles(L2toL4, 0, 2, 0, 3);
        AddRectTiles(L2toL4, 3, 3, 1, 3);

        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(197.443f, -35.031f, 0f), L2toL4, 0f, -10f, 4f);

        var I3toI4T = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(I3toI4T, -3, -1, -4, 2);
        AddRectTiles(I3toI4T, 0, 5, -4, 0);
        AddRectTiles(I3toI4T, 2, 5, -9, -5);

        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(188.368f, 6.954f, 0f), I3toI4T, 0f, -10f, 4f);

        var I1toI2 = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(I1toI2, -3, -2, -2, 2);
        AddRectTiles(I1toI2, -1, 1, -4, 2);
        AddRectTiles(I1toI2, 0, 2, 3, 4);
        AddRectTiles(I1toI2, 2, 3, -1, 2);

        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(89.53f, -4.06f, 0f), I1toI2, 0f, 9f, 4f);

        var I2toI3 = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(I2toI3, -3, -2, -2, 2);
        AddRectTiles(I2toI3, -1, -1, -3, 2);
        AddRectTiles(I2toI3, 0, 3, 0, 3);

        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(102.4f, 19.821f, 0f), I2toI3, 23f, 0f, 11f);

        var I1toI4 = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(I1toI4, -3, 0, -5, 3);
        AddRectTiles(I1toI4, -1, 10, 4, 5);
        AddRectTiles(I1toI4, 1, 10, -2, 3);
        AddRectTiles(I1toI4, 4, 7, 6, 8);
        AddRectTiles(I1toI4, 6, 10, -4, -3);
        AddRectTiles(I1toI4, 11, 12, -3, 5);

        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(103.45f, -24.02f, 0f), I1toI4, 60f, 0f, 20f);

    }



}