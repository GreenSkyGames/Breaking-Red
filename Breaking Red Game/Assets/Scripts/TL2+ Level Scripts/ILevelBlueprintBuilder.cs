using System.Collections.Generic;
using UnityEngine;

public interface ILevelBlueprintBuilder
    {
        void AddNormalPassage(string sprite, Vector3 position, string LevelID);
        void AddLockedPassage(string sprite, Vector3 position, GameObject door1, GameObject door2, string levelID);
        void AddDamagingEnv(string sprite, Vector3 position);
        void AddMovingPlatform(Vector3 position, List<Vector2> tileOffsets, float moveX, float moveY, float moveTime);
        void AddSlidingDoor(string sprite, Vector3 position, float horGoal, float vertGoal, float speed);
}