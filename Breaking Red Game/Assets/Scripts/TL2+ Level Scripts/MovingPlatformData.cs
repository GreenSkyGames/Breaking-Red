using UnityEngine;

public class MovingPlatformData
{
    private float moveTime;
    private float horGoal;
    private float vertGoal;
    private Vector2 startPos;
    private Vector2 goalPos;
    private Vector2 prevPos;
    private Rigidbody2D playerRigidbody;
    private PlatformTest platformTest;

    // Getters
    public float getMoveTime() => moveTime;
    public float getHorGoal() => horGoal;
    public float getVertGoal() => vertGoal;
    public Vector2 getStartPos() => startPos;
    public Vector2 getGoalPos() => goalPos;
    public Vector2 getPrevPos() => prevPos;
    public Rigidbody2D getPlayerRigidbody() => playerRigidbody;
    public PlatformTest getPlatformTest() => platformTest;

    // Setters
    public void setMoveTime(float value) => moveTime = value;
    public void setHorGoal(float value) => horGoal = value;
    public void setVertGoal(float value) => vertGoal = value;
    public void setStartPos(Vector2 value) => startPos = value;
    public void setGoalPos(Vector2 value) => goalPos = value;
    public void setPrevPos(Vector2 value) => prevPos = value;
    public void setPlayerRigidbody(Rigidbody2D value) => playerRigidbody = value;
    public void setPlatformTest(PlatformTest value) => platformTest = value;
}
