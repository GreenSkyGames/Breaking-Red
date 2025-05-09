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
    [SerializeField] private Tilemap _tilemap;

    // Target movement time
    [SerializeField] protected float pMoveTime = 1.0f;

    private Vector2 _startPos; 
    private Vector2 _goalPos;
    private Vector2 _prevPos;

    private Rigidbody2D _playerRigidbody;
    private PlatformTest _platformTest;

    private bool _bcMode = false; //bc mode variable 
    private GameObject[] bridgeTiles; //represents the new tiles that will from the bridge, will appear/disappear with BCToggle 

    public void SetMovementGoals(float horGoal, float vertGoal, float moveTime)
    {
        pHorGoal = horGoal;
        pVertGoal = vertGoal;
        pMoveTime = moveTime;
    }


    //This sets the initial positions for all necessary variables in the scene
    void Start()
    {
        // Initialize starting position and calculate goal position
        _startPos = transform.position;
        _goalPos = new Vector2(_startPos.x + pHorGoal, _startPos.y + pVertGoal);
        _prevPos = _startPos;

        bridgeTiles = GameObject.FindGameObjectsWithTag("BCBridge"); //new tiles tagged with this
    }

    /*This updates once per frame
     * Each time it updates, it moves the platform between specified locations at a constant speed
     * If a player steps on the platform it continues moving while also moving the player with it*/
    /*void Update()
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
    /*_playerRigidbody.position += platformVelocity * Time.deltaTime;
}

_prevPos = currentPosition; // Update the previous position
}*/

    void Update()
    {
        UpdateBCMode(); 
        //checking if BCMode is enabled 
        if(_bcMode) // if bcmode is enabled 
        {
            //Platforms shouldnt move and the bcmode platform tiles will show, 
            // forming a bridge for BC to use and not fall to death 
            EnableBCPlatforms();  
            return; //return so that the regular platforms do not move. 
        }
        else
        {
            DisableBCPlatforms(); //bcmode bridge shouldnt be seen by the player 

        } 

        // Calculate interpolation factor using Mathf.PingPong and apply SmoothStep for easing
        float t = Mathf.PingPong(Time.time / pMoveTime, 1.0f);
        float easedT = Mathf.SmoothStep(0.0f, 1.0f, t);  // Apply smoothing function to create a slower speed at the ends

        // Interpolate position between start and goal using the eased time
        Vector2 currentPosition = Vector2.Lerp(_startPos, _goalPos, easedT);
        transform.position = currentPosition;

        // Calculate platform velocity and apply it to the player if they're on the platform
        Vector2 platformVelocity = (currentPosition - _prevPos) / Time.deltaTime;

        if (_playerRigidbody != null)
        {
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
            if (_tilemap != null && _tilemap.gameObject.activeSelf)
            {
                _tilemap.gameObject.SetActive(false); // Hide the tilemap layer
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
            if (_tilemap != null && !_tilemap.gameObject.activeSelf)
            {
                _tilemap.gameObject.SetActive(true); // Show the tilemap layer
            }
        }
    }

    //Disable the platforms with BCBridge, when the game is not in BCMode 
    public void DisableBCPlatforms()
    {
        foreach (GameObject tile in bridgeTiles)
        {
            tile.SetActive(false); 
        }
    }

    //Enable the BCBridge Tiles to form a bridge when BCMode activated 
    public void EnableBCPlatforms()
    {
        foreach (GameObject tile in bridgeTiles)
        {
            tile.SetActive(true); 
        }
    }

    private void UpdateBCMode()
    {
        _bcMode = PlayerPrefs.GetInt("BCMode", 0) == 1;
        PlayerPrefs.Save();  
    }
}