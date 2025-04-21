using System.Collections.Generic;
using UnityEngine;

public class Level1 : LevelLoader
{
    public override void loadLevel(GameObject normalPassagePrefab, GameObject lockedPassagePrefab, GameObject damagingEnvPrefab, GameObject movingPlatformPrefab, GameObject movingPlatformTilePrefab, GameObject slidingDoorPrefab)
    {
        LevelBuilder builder = new LevelBuilder();
        BuildLevel(builder, slidingDoorPrefab); // Step 1: blueprint creation
        LoadBlueprint(builder.GetInstructions(), normalPassagePrefab, lockedPassagePrefab, damagingEnvPrefab, movingPlatformPrefab, movingPlatformTilePrefab, slidingDoorPrefab); // Step 2: actual instantiation handled by base
    }

    private void BuildLevel(ILevelBlueprintBuilder builder, GameObject slidingDoorPrefab)
    {
        // Add normal passages
        builder.AddNormalPassage("House_tileset_30", new Vector3(-107.5f, -18.700003f, 0), "IL1");
        builder.AddNormalPassage("House_Red_4", new Vector3(-1.5003f, 0.29983f, 0), "IL1.1");
        builder.AddNormalPassage("House_red_5", new Vector3(-0.5003f, 0.29983f, 0), "IL1.1");
        // builder.AddNormalPassage("House_tileset_30", new Vector2(-0.83f, -3.14f), "L2"); // Optional: commented in original

        // Create doors and locked passage
        GameObject door1 = CreateSlidingDoor(slidingDoorPrefab, "Tree_45", new Vector3(9.3881f, -7.380007f, 0), 0, 2f, 1f);
        GameObject door2 = CreateSlidingDoor(slidingDoorPrefab, "Tree_45", new Vector3(9.3881f, -9.220007f, 0), 0, -2f, 1f);

        builder.AddLockedPassage("cavetiles_2107", new Vector3(11.4997f, -8.70017f, 0), door1, door2, "L2");

        // Add damaging environment
        builder.AddDamagingEnv("Furniture_39", new Vector3(-110.5f, -10.703f, 0));
    }

    private void LoadBlueprint(List<LevelBuilder.TerrainInstruction> instructions, GameObject normalPassagePrefab, GameObject lockedPassagePrefab, GameObject damagingEnvPrefab, GameObject movingPlatformPrefab, GameObject movingPlatformTilePrefab, GameObject slidingDoorPrefab)
    {
        foreach (var instr in instructions)
        {
            switch (instr.type)
            {
                case "NormalPassage":
                    CreateNormalPassage(normalPassagePrefab, instr.sprite, instr.position, instr.levelID);
                    break;

                case "LockedPassage":
                    CreateLockedPassage(lockedPassagePrefab, instr.sprite, instr.position, instr.door1, instr.door2, instr.levelID);
                    break;

                case "DamagingEnv":
                    CreateDamagingEnv(damagingEnvPrefab, instr.sprite, instr.position);
                    break;

                case "SlidingDoor":
                    CreateSlidingDoor(slidingDoorPrefab, instr.sprite, instr.position, instr.moveX, instr.moveY, instr.moveTime);
                    break;

                case "MovingPlatform":
                    CreatePlatform(movingPlatformPrefab, movingPlatformTilePrefab, instr.position, instr.tileOffsets, instr.moveX, instr.moveY, instr.moveTime);
                    break;
            }
        }
    }
}

