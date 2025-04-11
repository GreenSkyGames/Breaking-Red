using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Level2 : LevelLoader
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
        CreateNormalPassage(normalPassagePrefab, "", new Vector2(220.5f, -18f), "L3");
        CreateNormalPassage(normalPassagePrefab, "", new Vector2(220.5f, -19f), "L3");
        CreateNormalPassage(normalPassagePrefab, "", new Vector2(194.5397f, -42.043f), "L4");
        CreateNormalPassage(normalPassagePrefab, "", new Vector2(195.5397f, -42.043f), "L4");
        CreateNormalPassage(normalPassagePrefab, "", new Vector2(196.5397f, -42.043f), "L4");
        CreateNormalPassage(normalPassagePrefab, "", new Vector2(197.5397f, -42.043f), "L4");
        CreateNormalPassage(normalPassagePrefab, "", new Vector2(198.5397f, -42.043f), "L4");
        CreateNormalPassage(normalPassagePrefab, "", new Vector2(199.5397f, -42.043f), "L4");
        CreateNormalPassage(normalPassagePrefab, "cavetiles_2107", new Vector2(78.35967f, -25.06817f), "L1", 180f);
        CreateNormalPassage(normalPassagePrefab, "cavetiles_179", new Vector2(185.2867f, 66.96183f), "S1");

        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(32.78864f, 38.74082f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(32.05822f, 38.02554f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(33.36012f, 37.98012f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(31.38456f, 37.23834f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(34.63929f, 37.09453f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(55.88639f, 28.8788f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(55.68f, 28.219f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(55.64f, 27.555f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(55.94f, 26.84f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(77.965f, 35.28f, 0), -90f);
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(77.243f, -35.28f, 0), -90f);
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(76.52f, 35.28f, 0), -90f);

        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(25.34f, 48.65f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(25.274f, 49.44f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(-24.95f, 50.29f, 0));

        var I3toI4 = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(I3toI4, -3, 11, -4, -1);
        AddRectTiles(I3toI4, 5, 11, 0, 5);
        AddRectTiles(I3toI4, 12, 15, 1, 5);
        AddRectTiles(I3toI4, 16, 17, 3, 5);

        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(149.38f, -8.97f, 0f), I3toI4, 18f, 0f, 7f);

        var L2toL4 = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(L2toL4, -3, -3, 0, 2);
        AddRectTiles(L2toL4, -2, -1, -1, 3);
        AddRectTiles(L2toL4, 0, 2, 0, 3);
        AddRectTiles(L2toL4, 3, 3, 1, 3);

        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(76.113f, -45.34f, 0f), L2toL4, 0f, -10f, 4f);

        var I3toI4T = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(I3toI4T, -3, -1, -4, 2);
        AddRectTiles(I3toI4T, 0, 5, -4, 0);
        AddRectTiles(I3toI4T, 2, 5, -9, -5);

        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(67.113f, -3.349f, 0f), I3toI4T, 0f, -10f, 4f);

        var I1toI2 = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(I1toI2, -3, -2, -2, 2);
        AddRectTiles(I1toI2, -1, 1, -4, 2);
        AddRectTiles(I1toI2, 0, 2, 3, 4);
        AddRectTiles(I1toI2, 2, 3, -1, 2);

        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(-31.78f, -14.34f, 0f), I1toI2, 8f, 9f, 4f);

        var L2toL3 = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(L2toL3, -3, -2, -1, 3);
        AddRectTiles(L2toL3, -1, 3, -1, 1);

        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(90.113f, -28.34f, 0f), L2toL3, 10f, 0f, 4f);

        var I2toI3 = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(I2toI3, -3, -2, -2, 2);
        AddRectTiles(I2toI3, -1, -1, -3, 2);
        AddRectTiles(I2toI3, 0, 3, 0, 3);

        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(-18.93f, 9.501f, 0f), I2toI3, 23f, 0f, 11f);

        var I1toI4 = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(I1toI4, -3, 0, -5, 3);
        AddRectTiles(I1toI4, -1, 10, 4, 5);
        AddRectTiles(I1toI4, 1, 10, -2, 3);
        AddRectTiles(I1toI4, 4, 7, 6, 8);
        AddRectTiles(I1toI4, 6, 10, -4, -3);
        AddRectTiles(I1toI4, 11, 12, -3, 5);

        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(-17.88f, -34.34f, 0f), I1toI4, 60f, 0f, 20f);

    }

    

}