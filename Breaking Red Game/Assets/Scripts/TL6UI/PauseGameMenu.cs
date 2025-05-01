// Puase menu script 

/* Name: Liz Beltran 
 * Role: Team Lead 6 --Version Control Manager
 *	This is the script for the Pause Menu, which defines the following behaviors:
 *  Resume, Save game, main menu, and quit game
 * in this script I will be utilizing the facade pattern and singleton pattern. 
*/ 

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem; 
using System.Collections; 
using System.Collections.Generic; 

public class PauseGameMenu : MonoBehaviour
{  
    public static bool IsPaused = false; //indicate whether game is paused or not  
    private List<AudioSource> allAudioSources = new List<AudioSource>(); // To store all active AudioSources
    private List<bool> audioSourceStates = new List<bool>(); // To store the state, play or pause
    private bool _menuActivated; //inventory manager being activated? 
    public static PauseGameMenu instance; //singleton 
    private GameUIFacade facade; //pattern 
    public GameObject PauseMenuUI; // just the pause menu panel
    public GameObject PauseMenu; //whole canvas 
    public GameObject inventoryMenu; //inventory manager object 
    public Button inventoryButton;
    private bool _shouldRestorePosition = false; // Flag to indicate if we need to restore position after scene load
    private Vector3 _savedPosition; // Store the saved position for restoration
    private string _savedScene; // Store the saved scene name
    private bool _isLoadingFromMenu = false; // Flag to track if we're loading from menu

    // Singleton Pattern 
    // https://refactoring.guru/design-patterns/singleton 
    private void Awake()
    {    
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ensures pause menu persists across scenes
            Debug.Log("pause menu instance initialized.");
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate pause menu if one already exists
            return;
        }
    } 

    // Pause menu called => in view, Pause menu uncalled=> not in view 
    void Start()
    { 
        //initializing pause menu state 
        PauseMenuUI.SetActive(false); 
        IsPaused = false; 
        Time.timeScale = 1f; //ensure time is running on scene start
        //LoadGame(); COMMENTED OUT FOR GAME TESTING PURPOSES

        if (inventoryButton != null)
        {
            inventoryButton.onClick.AddListener(ViewInventory);
            // Play the button click
            AudioManager.instance.Play("ClickSound");

            Debug.Log("Inventory butoon clicked!"); 
        } 
    }

    // How pause menu called, p pressed to either call pause menu or remove it from the screen 
    void Update()
    {
        // Prevent 'P' toggle when clicking on UI elements
        if (EventSystem.current.currentSelectedGameObject != null)
            return;

        //Toggle pause state when 'P' is pressed
        if(Input.GetKeyDown(KeyCode.P) || (Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame))
        {
            Debug.Log("P / button was pressed"); 

            if (PauseMenuUI == null)
            {
                Debug.LogWarning("PauseMenu is null. Did you forget to assign it?");
                return;
            }

            if (IsPaused)
            {
                ResumeGame(); 
            }
            else
            {
                PauseGame(); 
            }
        }
    }
    
    //resumes the game when the player presses the resume game button in the pause menu
    public void ResumeGame()
    {

        // Ensure the whole Canvas is active first
        if (PauseMenu != null && !PauseMenu.activeInHierarchy)
        {
            PauseMenu.SetActive(true);  // 🔧 This line ensures the Canvas itself is active
        }

        if( PauseMenuUI == null)
        {
            Debug.LogWarning("Pause menu is null"); 
            return; 
        }

        PauseMenuUI.SetActive(false); //pause menu goes away 
        Time.timeScale=1f; // resuming the game
        IsPaused = false; //game is not paused
        Debug.Log("Game has resumed."); 

        // Play the button click
        AudioManager.instance.Play("ClickSound");

        // Restore all audio sources
        StartCoroutine(AudioManager.instance.RestoreAudioStates());
        // SceneManager.LoadScene("Level 1");
    }

    //Function to pause the game
    public void PauseGame()
    {
        if (!PauseMenu.activeInHierarchy) //making sure canvas is active 
            PauseMenu.SetActive(true);

        if (PauseMenuUI == null)
        {
            Debug.LogWarning("PauseMenu is null. Did you forget to assign it?");
            return;
        } 

        PauseMenuUI.SetActive(true); //pause menu panel called 
        Time.timeScale = 0f; // pausing the game freezing time
        IsPaused = true; //game is paused
        Debug.Log("Game is paused."); 

        // Play the button click
        AudioManager.instance.Play("ClickSound");

        // Pause all audio sources and save their states
        StartCoroutine(AudioManager.instance.PauseAllAudioSources());
    }

    //save game when the player chooses to save the game
    public void SaveGame()
    {
        // Play the button click sound
        AudioManager.instance.Play("ClickSound");

        // Save current scene
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("CurrentScene", currentScene);

        // Save player position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
            PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
            PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);

            // Save player health
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                PlayerPrefs.SetInt("PlayerHealth", playerHealth.currentHealth);
                PlayerPrefs.SetInt("PlayerMaxHealth", playerHealth.maxHealth);
            }
        }

        // Save inventory items
        InventoryManager inventory = InventoryManager.sInstance;
        if (inventory != null)
        {
            // Save the number of items
            int itemCount = inventory.getItemCount();
            PlayerPrefs.SetInt("InventoryItemCount", itemCount);

            // Save each item
            for (int i = 0; i < itemCount; i++)
            {
                ItemSlot slot = inventory.itemSlot[i];
                if (slot != null && slot.itemName != "")
                {
                    PlayerPrefs.SetString($"InventoryItem_{i}", slot.itemName);
                    PlayerPrefs.SetString($"InventoryItemDesc_{i}", slot.itemDescription);
                    
                    // Find the power-up prefab to get its sprite
                    PowerUpTemplate[] powerUps = Resources.FindObjectsOfTypeAll<PowerUpTemplate>();
                    foreach (var powerUp in powerUps)
                    {
                        if (powerUp.itemType.ToString() == slot.itemName)
                        {
                            // Save the sprite name for later restoration
                            PlayerPrefs.SetString($"InventoryItemSprite_{i}", powerUp.sprite.name);
                            break;
                        }
                    }
                }
            }
        }

        // Save all dead NPCs
        int deadCount = 0;
        foreach (var deadNPC in NPCManager.deadNPCs)
        {
            if (deadNPC.Value)  // If the NPC is dead
            {
                PlayerPrefs.SetString($"DeadNPC_{deadCount}", deadNPC.Key);
                deadCount++;
            }
        }
        PlayerPrefs.SetInt("DeadNPCCount", deadCount);

        // Save passageway states
        LockedPassage[] passages = FindObjectsOfType<LockedPassage>();
        foreach (LockedPassage passage in passages)
        {
            if (passage != null)
            {
                string passageTag = passage.gameObject.tag;
                bool isUnlocked = passage.IsPassageUnlocked();
                PlayerPrefs.SetInt($"Passage_{passageTag}_Unlocked", isUnlocked ? 1 : 0);
            }
        }

        // Save game time
        PlayerPrefs.SetFloat("GameTime", Time.timeSinceLevelLoad);

        // Save all changes
        PlayerPrefs.Save();
        Debug.Log("Game Saved: Scene " + currentScene + ", Player position, health, inventory, and NPC states saved.");
    }

    //Load the Game if the player presses the loadgame button
    public void LoadGame()
    {
        // Check if saved data exists
        if (PlayerPrefs.HasKey("CurrentScene"))
        {
            _savedScene = PlayerPrefs.GetString("CurrentScene");
            float x = PlayerPrefs.GetFloat("PlayerPosX");
            float y = PlayerPrefs.GetFloat("PlayerPosY");
            float z = PlayerPrefs.GetFloat("PlayerPosZ");
            _savedPosition = new Vector3(x, y, z);

            // Load the saved scene if it's different from current scene
            if (SceneManager.GetActiveScene().name != _savedScene)
            {
                _shouldRestorePosition = true;
                SceneManager.LoadScene(_savedScene);
            }
            else
            {
                // If we're already in the correct scene, start the restore process
                StartCoroutine(RestorePlayerPositionWithDelay());
            }
        }
        else
        {
            Debug.Log("No save data to load.");
        }
    }

    public void LoadMenu()
    {
        // Play the button click
        AudioManager.instance.Play("ClickSound");

        // Save the current scene and position before going to menu
        string currentScene = SceneManager.GetActiveScene().name;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _savedScene = currentScene;
            _savedPosition = player.transform.position;
            _isLoadingFromMenu = true;
        }

        // Load the start menu scene
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1f; // Ensure time is running when returning to menu
        IsPaused = false; // Reset pause state
    }

    private IEnumerator RestorePlayerPositionWithDelay()
    {
        Debug.Log("Starting position restoration process...");
        Debug.Log($"Saved position: {_savedPosition}");

        // Reduced initial delay
        yield return new WaitForSeconds(0.2f);

        // Try to find the player object
        GameObject player = null;
        int attempts = 0;
        const int maxAttempts = 5;

        while (player == null && attempts < maxAttempts)
        {
            Debug.Log($"Attempt {attempts + 1} to find player...");
            
            // Try all methods in one frame
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                player = GameObject.Find("Player");
            }
            if (player == null)
            {
                var playerComponent = FindObjectOfType<PlayerController>();
                if (playerComponent != null)
                {
                    player = playerComponent.gameObject;
                }
            }

            if (player == null)
            {
                attempts++;
                yield return new WaitForSeconds(0.1f);
            }
        }

        if (player != null)
        {
            Debug.Log($"Player found! Setting position to {_savedPosition}");
            player.transform.position = _savedPosition;
            Debug.Log($"New player position: {player.transform.position}");

            // Restore player health
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null && PlayerPrefs.HasKey("PlayerHealth"))
            {
                playerHealth.currentHealth = PlayerPrefs.GetInt("PlayerHealth");
                playerHealth.maxHealth = PlayerPrefs.GetInt("PlayerMaxHealth");
                Debug.Log($"Player health restored: {playerHealth.currentHealth}/{playerHealth.maxHealth}");
            }

            // Restore dead NPCs
            if (PlayerPrefs.HasKey("DeadNPCCount"))
            {
                int deadCount = PlayerPrefs.GetInt("DeadNPCCount");
                for (int i = 0; i < deadCount; i++)
                {
                    string npcTag = PlayerPrefs.GetString($"DeadNPC_{i}");
                    NPCManager.deadNPCs[npcTag] = true;
                    
                    // Find and destroy any remaining NPCs with this tag
                    GameObject[] npcs = GameObject.FindGameObjectsWithTag(npcTag);
                    foreach (GameObject npc in npcs)
                    {
                        Destroy(npc);
                    }
                }
            }

            // Restore inventory
            InventoryManager inventory = InventoryManager.sInstance;
            if (inventory != null && PlayerPrefs.HasKey("InventoryItemCount"))
            {
                int itemCount = PlayerPrefs.GetInt("InventoryItemCount");
                for (int i = 0; i < itemCount; i++)
                {
                    string itemName = PlayerPrefs.GetString($"InventoryItem_{i}");
                    string itemDesc = PlayerPrefs.GetString($"InventoryItemDesc_{i}");
                    string spriteName = PlayerPrefs.GetString($"InventoryItemSprite_{i}");
                    
                    if (itemName != "")
                    {
                        // Find the power-up prefab to get its sprite
                        PowerUpTemplate[] powerUps = Resources.FindObjectsOfTypeAll<PowerUpTemplate>();
                        Sprite itemSprite = null;
                        
                        foreach (var powerUp in powerUps)
                        {
                            if (powerUp.itemType.ToString() == itemName && powerUp.sprite.name == spriteName)
                            {
                                itemSprite = powerUp.sprite;
                                break;
                            }
                        }
                        
                        inventory.addToInventory(itemName, itemSprite, itemDesc);
                    }
                }
                Debug.Log($"Restored {itemCount} inventory items with sprites");
            }

            // Restore passageway states
            LockedPassage[] passages = FindObjectsOfType<LockedPassage>();
            foreach (LockedPassage passage in passages)
            {
                if (passage != null)
                {
                    string passageTag = passage.gameObject.tag;
                    if (PlayerPrefs.HasKey($"Passage_{passageTag}_Unlocked"))
                    {
                        bool isUnlocked = PlayerPrefs.GetInt($"Passage_{passageTag}_Unlocked") == 1;
                        if (isUnlocked)
                        {
                            Debug.Log("PauseGame messing with stuff");
                            passage.revealPassageway();
                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Failed to find player object after multiple attempts.");
        }
    }

    //Quits the game when the player presses to quit the game
    public void QuitGame()
    {
        // Play the button click
        AudioManager.instance.Play("ClickSound");

        // Quit the application
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

// player can view inventory from pause menu 
    public void ViewInventory()
    {
        // Play the button click
        AudioManager.instance.Play("ClickSound");

        if (inventoryMenu != null)
        {
            inventoryMenu.SetActive(true);
            Debug.Log("Inventory menu activated.");
        }
        else
        {
            Debug.LogWarning("Inventory menu is not assigned.");
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene loaded: {scene.name}");
        
        if (PauseMenuUI != null){
            PauseMenuUI.SetActive(false); // Hide it when loading a new scene
            Debug.Log("PauseMenu Canvas found and disabled.");
        }
        else
        {
            Debug.LogWarning("PauseMenu Canvas is still null after scene load."); 
        }

        // Start position restoration immediately if needed
        if ((_isLoadingFromMenu || _shouldRestorePosition) && scene.name == _savedScene)
        {
            Debug.Log("Starting position restoration...");
            StartCoroutine(RestorePlayerPositionWithDelay());
            _isLoadingFromMenu = false;
            _shouldRestorePosition = false;
        }
    }
}
 