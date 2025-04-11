using System.Runtime.CompilerServices;
using UnityEngine;

public class Level2 : LevelLoader
{
    public GameObject normalPassagePrefab;
    public GameObject lockedPassagePrefab;
    public GameObject damagingEnvPrefab;
    public GameObject slidingDoorPrefab;
    public override void LoadLevel(GameObject normalPassagePrefab, GameObject lockedPassagePrefab, GameObject damagingEnvPrefab, GameObject movingPlatformPrefab, GameObject slidingDoorPrefab)
    {
        //NormalPassage normalPassage = NormalPassage();
        //normalPassage.Spawn(new Vector3(287.33f, -4.88f, 0), "cavetiles_2107")
        //terrainList.Add(new NormalPassage(new Vector3(287.33f, -4.88f, 0f), "Cavetiles_2107"));


        /*CreateNormalPassage(normalPassagePrefab, "House_tileset_30", new Vector3(-107.5f, -18.700003f, 0), "IL1");
        CreateNormalPassage(normalPassagePrefab, "House_Red_4", new Vector3(-1.5003f, 0.29983f, 0), "IL1.1");
        CreateNormalPassage(normalPassagePrefab, "House_red_5", new Vector3(-0.5003f, 0.29983f, 0), "IL1.1");

        CreateLockedPassage(lockedPassagePrefab, "cavetiles_2107", new Vector3(94.26f, 4.343f, 0));

        CreateDamagingEnv(damagingEnvPrefab, "Furniture_39", new Vector3(-110.5f, -10.703f, 0));

        CreateSlidingDoor(slidingDoorPrefab, "Tree_45", new Vector3(6.62f, 1.422f, 0), -2f, 1f);
        CreateSlidingDoor(slidingDoorPrefab, "Tree_45", new Vector3(6.58f, -0.417f, 0), 2f, 1f);*/
        CreateNormalPassage(normalPassagePrefab, "House_tileset_30", new Vector3(81.7f, -24.3f, 0), "IL1");
    }
}