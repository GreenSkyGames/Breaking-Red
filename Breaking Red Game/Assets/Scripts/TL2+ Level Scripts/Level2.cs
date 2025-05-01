using UnityEngine;

public class Level2 : LevelLoader
{
    public override void loadLevel(GameObject normalPassagePrefab, GameObject lockedPassagePrefab, GameObject damagingEnvPrefab, GameObject movingPlatformPrefab, GameObject movingPlatformTilePrefab, GameObject slidingDoorPrefab)
    {
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
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(154.11f, 49.06f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(153.3796f, 48.34472f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(154.6815f, 48.2993f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(152.7059f, 47.55753f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(155.9606f, 47.41371f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(177.2077f, 39.19798f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(177.0039f, 38.53856f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(176.968f, 37.87915f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(177.2677f, 37.15979f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(199.29f, 45.6f, 0), -90f);
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(198.57f, 45.6f, 0), -90f);
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(197.85f, 45.6f, 0), -90f);
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(146.6695f, 58.96919f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(146.5954f, 59.76537f, 0));
        CreateDamagingEnv(damagingEnvPrefab, "2_0", new Vector3(146.2807f, 60.61709f, 0));
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
        var L2toL3 = new System.Collections.Generic.List<Vector2>();
        AddRectTiles(L2toL3, -3, -2, -1, 3);
        AddRectTiles(L2toL3, -1, 3, -1, 1);
        CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, new Vector3(211.43f, -18.03f, 0f), L2toL3, 10f, 0f, 4f);
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

    public new void StaticMethod(GameObject lockedPassagePrefab)
    {
        Vector3 position = new Vector3(-3.5003f, -10.29983f, 0);
        GameObject obj = Object.Instantiate(lockedPassagePrefab, position, Quaternion.identity);
        obj.tag = "IL1.1";
        obj.GetComponent<TerrainObjects>().setSprite("House_tileset_30");
    }

}