/*Name: Alex Senst
 * Role: Team Lead 2+ -- Software Architect
 * 
 * This file contains the definition for the MovingPlatform class
 * This class is used to generate the movement of platforms within the game
 * It inherets from the TerrainObjects class
 */
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingPlatform : TerrainObjects
{
    // Positional offsets for goal
    [SerializeField] protected float pHorGoal = 0.0f;
    [SerializeField] protected float pVertGoal = 0.0f;

    // Target movement time
    [SerializeField] protected float pMoveTime = 1.0f;

    private Vector2 _startPos;
    private Vector2 _goalPos;
    private Vector2 _prevPos;

    private Rigidbody2D _playerRigidbody;
    private PlatformTest _platformTest;

    [SerializeField] private Tilemap tilemap;

    //This sets the initial positions for all necessary variables in the scene
    void Start()
    {
        // Initialize starting position and calculate goal position
        _startPos = transform.position;
        _goalPos = new Vector2(_startPos.x + pHorGoal, _startPos.y + pVertGoal);
        _prevPos = _startPos;
    }

    /*This updates once per frame
     * Each time it updates, it moves the platform between specified locations at a constant speed
     * If a player steps on the platform it continues moving while also moving the player with it*/
    void Update()
    {
        // Calculate interpolation factor using Mathf.PingPong for smooth back-and-forth motion
        float t = Mathf.PingPong(Time.time / pMoveTime, 1.0f);

        // Interpolate position between start and goal
        Vector2 currentPosition = Vector2.Lerp(_startPos, _goalPos, t);
        transform.position = currentPosition;

        // Calculate platform velocity and apply it to the player if they're on the platform
        Vector2 platformVelocity = (currentPosition - _prevPos) / Time.deltaTime;

        if (_playerRigidbody != null)
        {
            /*Vector2 playerVel = _playerRigidbody.linearVelocity;
            playerVel.x = platformVelocity.x;
            _playerRigidbody.linearVelocity = playerVel;*/
            _playerRigidbody.position += platformVelocity * Time.deltaTime;
        }

        _prevPos = currentPosition; // Update the previous position
    }

    /* This function detects if a player is in contact with or grounded on top of the platform
     * If the player is on top of the platform, it turns off the boundary layer that triggers death while they're in contact
     * It also notifies the testing script PlatformTest that the player got on the platform by calling OnPlayerEnterPlatform()*/
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _playerRigidbody = collider.GetComponent<Rigidbody2D>();
            if (tilemap != null && tilemap.gameObject.activeSelf)
            {
                tilemap.gameObject.SetActive(false); // Hide the tilemap layer
            }

            // For Testing: Notify the test script that the player entered the platform
            PlatformTest _platformTest = FindFirstObjectByType<PlatformTest>();
            if (_platformTest != null)
            {
                _platformTest.OnPlayerEnterPlatform();
            }
        }


        
    }

    /* This code detects when the player loses contact with the platform or leaves the platform
     * If the player is no longer on the platform, it turns on the boundary layer that triggers death once again
     It also notifies the testing script PlatformTest that the player got off of the platform by calling OnPlayerExitPlatform()*/
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (collider.GetComponent<Rigidbody2D>() == _playerRigidbody)
            {
                _playerRigidbody = null;
            }
            if (tilemap != null && !tilemap.gameObject.activeSelf)
            {
                tilemap.gameObject.SetActive(true); // Show the tilemap layer
            }

            //For testing:
            // Notify the test script that the player exited the platform
            PlatformTest platformTest = FindFirstObjectByType<PlatformTest>();
            if (platformTest != null)
            {
                platformTest.OnPlayerExitPlatform();
            }
        }

        
    }
}
