/*using System.Runtime.CompilerServices;
using UnityEngine;

public class Level1 : LevelLoader
{
    public override void loadLevel(GameObject normalPassagePrefab, GameObject lockedPassagePrefab, GameObject damagingEnvPrefab, GameObject movingPlatformPrefab, GameObject movingPlatformTilePrefab, GameObject slidingDoorPrefab)
    {
        //NormalPassage normalPassage = NormalPassage();
        //normalPassage.Spawn(new Vector3(287.33f, -4.88f, 0), "cavetiles_2107")
        //terrainList.Add(new NormalPassage(new Vector3(287.33f, -4.88f, 0f), "Cavetiles_2107"));


        CreateNormalPassage(normalPassagePrefab, "House_tileset_30", new Vector3(-107.5f, -18.700003f, 0), "IL1");
        CreateNormalPassage(normalPassagePrefab, "House_Red_4", new Vector3(-1.5003f, 0.29983f, 0), "IL1.1");
        CreateNormalPassage(normalPassagePrefab, "House_red_5", new Vector3(-0.5003f, 0.29983f, 0), "IL1.1");
        //CreateNormalPassage(normalPassagePrefab, "House_tileset_30", new Vector2(-0.83f, -3.14f), "L2");

        GameObject door1 = CreateSlidingDoor(slidingDoorPrefab, "Tree_45", new Vector3(9.3881f, -7.380007f, 0), 0, 2f, 1f);
        GameObject door2 = CreateSlidingDoor(slidingDoorPrefab, "Tree_45", new Vector3(9.3881f, -9.220007f, 0), 0, -2f, 1f);

        CreateLockedPassage(lockedPassagePrefab, "cavetiles_2107", new Vector3(11.4997f, -8.70017f, 0), door1, door2, "L2");

        CreateDamagingEnv(damagingEnvPrefab, "Furniture_39", new Vector3(-110.5f, -10.703f, 0));
    }
}*/
using System.Collections.Generic;
using UnityEngine;

public class Level1 : LevelLoader
{
    interface ILevelComponent
    {
        void Build(LevelLoader loader,
                   GameObject normalPassagePrefab,
                   GameObject lockedPassagePrefab,
                   GameObject damagingEnvPrefab,
                   GameObject movingPlatformPrefab,
                   GameObject movingPlatformTilePrefab,
                   GameObject slidingDoorPrefab);
    }

    class NormalPassageComponent : ILevelComponent
    {
        Vector3 position;
        string sprite;
        string label;

        public NormalPassageComponent(string sprite, Vector3 position, string label)
        {
            this.sprite = sprite;
            this.position = position;
            this.label = label;
        }

        public void Build(LevelLoader loader,
                          GameObject normalPassagePrefab,
                          GameObject lockedPassagePrefab,
                          GameObject damagingEnvPrefab,
                          GameObject movingPlatformPrefab,
                          GameObject movingPlatformTilePrefab,
                          GameObject slidingDoorPrefab)
        {
            ((Level1)loader).CreateNormalPassage(normalPassagePrefab, sprite, position, label);
        }
    }

    class SlidingDoorComponent : ILevelComponent
    {
        string sprite;
        Vector3 position;
        float moveX, moveY, speed;
        public GameObject doorResult; // stores the created door

        public SlidingDoorComponent(string sprite, Vector3 position, float moveX, float moveY, float speed)
        {
            this.sprite = sprite;
            this.position = position;
            this.moveX = moveX;
            this.moveY = moveY;
            this.speed = speed;
        }

        public void Build(LevelLoader loader,
                          GameObject normalPassagePrefab,
                          GameObject lockedPassagePrefab,
                          GameObject damagingEnvPrefab,
                          GameObject movingPlatformPrefab,
                          GameObject movingPlatformTilePrefab,
                          GameObject slidingDoorPrefab)
        {
            doorResult = ((Level1)loader).CreateSlidingDoor(slidingDoorPrefab, sprite, position, moveX, moveY, speed);
        }
    }

    class LockedPassageComponent : ILevelComponent
    {
        string sprite, label;
        Vector3 position;
        SlidingDoorComponent door1Ref, door2Ref;

        public LockedPassageComponent(string sprite, Vector3 position, SlidingDoorComponent door1, SlidingDoorComponent door2, string label)
        {
            this.sprite = sprite;
            this.position = position;
            this.door1Ref = door1;
            this.door2Ref = door2;
            this.label = label;
        }

        public void Build(LevelLoader loader,
                          GameObject normalPassagePrefab,
                          GameObject lockedPassagePrefab,
                          GameObject damagingEnvPrefab,
                          GameObject movingPlatformPrefab,
                          GameObject movingPlatformTilePrefab,
                          GameObject slidingDoorPrefab)
        {
            ((Level1)loader).CreateLockedPassage(lockedPassagePrefab, sprite, position, door1Ref.doorResult, door2Ref.doorResult, label);
        }
    }

    class DamagingEnvComponent : ILevelComponent
    {
        string sprite;
        Vector3 position;

        public DamagingEnvComponent(string sprite, Vector3 position)
        {
            this.sprite = sprite;
            this.position = position;
        }

        public void Build(LevelLoader loader,
                          GameObject normalPassagePrefab,
                          GameObject lockedPassagePrefab,
                          GameObject damagingEnvPrefab,
                          GameObject movingPlatformPrefab,
                          GameObject movingPlatformTilePrefab,
                          GameObject slidingDoorPrefab)
        {
            ((Level1)loader).CreateDamagingEnv(damagingEnvPrefab, sprite, position);
        }
    }

    public override void loadLevel(GameObject normalPassagePrefab,
                                   GameObject lockedPassagePrefab,
                                   GameObject damagingEnvPrefab,
                                   GameObject movingPlatformPrefab,
                                   GameObject movingPlatformTilePrefab,
                                   GameObject slidingDoorPrefab)
    {
        // Build the components list
        var components = new List<ILevelComponent>();

        components.Add(new NormalPassageComponent("House_tileset_30", new Vector3(-107.5f, -18.700003f, 0), "IL1"));
        components.Add(new NormalPassageComponent("House_Red_4", new Vector3(-1.5003f, 0.29983f, 0), "IL1.1"));
        components.Add(new NormalPassageComponent("House_red_5", new Vector3(-0.5003f, 0.29983f, 0), "IL1.1"));

        var door1 = new SlidingDoorComponent("Tree_45", new Vector3(9.3881f, -7.380007f, 0), 0, 2f, 1f);
        var door2 = new SlidingDoorComponent("Tree_45", new Vector3(9.3881f, -9.220007f, 0), 0, -2f, 1f);
        components.Add(door1);
        components.Add(door2);

        components.Add(new LockedPassageComponent("cavetiles_2107", new Vector3(11.4997f, -8.70017f, 0), door1, door2, "L2"));

        components.Add(new DamagingEnvComponent("Furniture_39", new Vector3(-110.5f, -10.703f, 0)));

        // Execute all component builds
        foreach (var c in components)
        {
            c.Build(this, normalPassagePrefab, lockedPassagePrefab, damagingEnvPrefab, movingPlatformPrefab, movingPlatformTilePrefab, slidingDoorPrefab);
        }
    }
}
