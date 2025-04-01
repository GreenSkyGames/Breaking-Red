using UnityEngine;

public class SlidingDoor : MovingPlatform
{
    [SerializeField] protected bool _unlocked = false; // Determines if the door can move
    [SerializeField] private float _speed = 2.0f; // Speed of movement

    private bool _moved = false; // Ensures the door only moves once

    void Update()
    {
        if (_unlocked && !_moved)
        {
            OneWay();
            _moved = true;
        }
    }

    // Moves the door in one direction and then disables it
    private void OneWay()
    {
        StartCoroutine(MoveAndDisable());
    }

    private System.Collections.IEnumerator MoveAndDisable()
    {
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = new Vector2(startPosition.x + pHorGoal, startPosition.y + pVertGoal);
        float elapsedTime = 0f;

        while (elapsedTime < pMoveTime)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / pMoveTime);
            elapsedTime += Time.deltaTime * _speed;
            yield return null;
        }

        transform.position = targetPosition;
        gameObject.SetActive(false); // Disable the door after moving
    }

    public void UnlockDoor()
    {
        _unlocked = true;
        Debug.Log("unlocked!");
    }
}
