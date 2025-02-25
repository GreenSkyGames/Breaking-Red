using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingPlatform : terrainObjects
{
    // Positional offsets for goal
    [SerializeField] private float _horGoal = 0.0f;
    [SerializeField] private float _vertGoal = 0.0f;

    // Target movement time
    [SerializeField] private float _moveTime = 1.0f;

    private Vector2 _startPos;
    private Vector2 _goalPos;
    private Vector2 _prevPos;

    private Rigidbody2D _playerRigidbody;

    [SerializeField] private TilemapRenderer tilemapRenderer;

    void Start()
    {
        // Initialize starting position and calculate goal position
        _startPos = transform.position;
        _goalPos = new Vector2(_startPos.x + _horGoal, _startPos.y + _vertGoal);
        _prevPos = _startPos;
    }

    void Update()
    {
        // Calculate interpolation factor using Mathf.PingPong for smooth back-and-forth motion
        float t = Mathf.PingPong(Time.time / _moveTime, 1.0f);

        // Interpolate position between start and goal
        Vector2 currentPosition = Vector2.Lerp(_startPos, _goalPos, t);
        transform.position = currentPosition;

        // Calculate platform velocity and apply it to the player if they're on the platform
        Vector2 platformVelocity = (currentPosition - _prevPos) / Time.deltaTime;

        if (_playerRigidbody != null)
        {
            Vector2 playerVel = _playerRigidbody.linearVelocity;
            playerVel.x = platformVelocity.x;
            _playerRigidbody.linearVelocity = playerVel;
        }

        _prevPos = currentPosition; // Update the previous position
    }

    // Detect when the player steps onto the platform
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("InCollider");
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Yes");
            _playerRigidbody = collider.GetComponent<Rigidbody2D>();
        }

        if (tilemapRenderer != null)
        {
            tilemapRenderer.enabled = false; // Hide the tilemap layer
        }
    }

    // Detect when the player leaves the platform
    private void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("OffCollider");
        if (collider.CompareTag("Player"))
        {
            Debug.Log("No");
            if (collider.GetComponent<Rigidbody2D>() == _playerRigidbody)
            {
                _playerRigidbody = null;
            }
        }

        if (tilemapRenderer != null)
        {
            tilemapRenderer.enabled = false; // Show the tilemap layer
        }
    }
}
