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
        AddRectTiles(L2toL4, -3, 11, -4, -1);
        AddRectTiles(L2toL4, 5, 11, 0, 5);
        AddRectTiles(L2toL4, 12, 15, 1, 5);
        AddRectTiles(L2toL4, 16, 17, 3, 5);

        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(149.38f, -8.97f, 0f), L2toL4, 18f, 0f, 7f);

        var I3toI4T = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(I3toI4T, -3, 11, -4, -1);
        AddRectTiles(I3toI4T, 5, 11, 0, 5);
        AddRectTiles(I3toI4T, 12, 15, 1, 5);
        AddRectTiles(I3toI4T, 16, 17, 3, 5);

        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(149.38f, -8.97f, 0f), I3toI4T, 18f, 0f, 7f);

        var I1toI2 = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(I1toI2, -3, 11, -4, -1);
        AddRectTiles(I1toI2, 5, 11, 0, 5);
        AddRectTiles(I1toI2, 12, 15, 1, 5);
        AddRectTiles(I1toI2, 16, 17, 3, 5);

        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(149.38f, -8.97f, 0f), I1toI2, 18f, 0f, 7f);

        var L2toL3 = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(L2toL3, -3, 11, -4, -1);
        AddRectTiles(L2toL3, 5, 11, 0, 5);
        AddRectTiles(L2toL3, 12, 15, 1, 5);
        AddRectTiles(L2toL3, 16, 17, 3, 5);

        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(149.38f, -8.97f, 0f), L2toL3, 18f, 0f, 7f);

        var I2toI3 = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(I2toI3, -3, 11, -4, -1);
        AddRectTiles(I2toI3, 5, 11, 0, 5);
        AddRectTiles(I2toI3, 12, 15, 1, 5);
        AddRectTiles(I2toI3, 16, 17, 3, 5);

        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(149.38f, -8.97f, 0f), I2toI3, 18f, 0f, 7f);

        var I1toI4 = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(I1toI4, -3, 11, -4, -1);
        AddRectTiles(I1toI4, 5, 11, 0, 5);
        AddRectTiles(I1toI4, 12, 15, 1, 5);
        AddRectTiles(I1toI4, 16, 17, 3, 5);

        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(149.38f, -8.97f, 0f), I1toI4, 18f, 0f, 7f);

    }

    

}