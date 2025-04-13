using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : ILevelBlueprintBuilder
{
    public struct TerrainInstruction
    {
        public string type;
        public string sprite;
        public Vector3 position;
        public string levelID;
        public GameObject door1, door2;
        public List<Vector2> tileOffsets;
        public float moveX;
        public float moveY;
        public float moveTime;
    }

    private List<TerrainInstruction> instructions = new List<TerrainInstruction>();

    public void AddNormalPassage(string sprite, Vector3 pos, string LevelID)
    {
        instructions.Add(new TerrainInstruction
        {
            type = "NormalPassage",
            sprite = sprite,
            position = pos,
            levelID = LevelID,
        });
    }
    public void AddLockedPassage(string sprite, Vector3 pos, GameObject door1, GameObject door2, string LevelID)
    {
        instructions.Add(new TerrainInstruction
        {
            type = "LockedPassage",
            sprite = sprite,
            position = pos,
            door1 = door1,
            door2 = door2,
            levelID = LevelID
        });
    }
    public void AddDamagingEnv(string sprite, Vector3 pos)
    {
        instructions.Add(new TerrainInstruction
        {
            type = "DamagingEnv",
            sprite = sprite,
            position = pos
        });
    }
    public void AddMovingPlatform(Vector3 pos, List<Vector2> tileOffsets, float moveX, float moveY, float moveTime)
    {
        instructions.Add(new TerrainInstruction
        {
            type = "MovingPlatform",
            position = pos,
            tileOffsets = tileOffsets,
            moveX = moveX,
            moveY = moveY,
            moveTime = moveTime
        });
    }
    public void AddSlidingDoor(string sprite, Vector3 pos, float horGoal, float vertGoal, float speed)
    {
        instructions.Add(new TerrainInstruction
        {
            type = "SlidingDoor",
            sprite = sprite,
            position = pos,
            moveX = horGoal,
            moveY = vertGoal,
            moveTime = speed
        });
    }
    public List<TerrainInstruction> GetInstructions()
    {
        return instructions;
    }
}
