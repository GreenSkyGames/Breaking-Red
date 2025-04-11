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
    public GameObject PauseMenu; // pause menu object to be connected with script 
    private List<AudioSource> allAudioSources = new List<AudioSource>(); // To store all active AudioSources
    private List<bool> audioSourceStates = new List<bool>(); // To store the state, play or pause
    private bool _menuActivated; //inventory manager being activated? 
    public GameObject inventoryMenu; //inventory manager object 
    public static PauseGameMenu instance; 
    private GameUIFacade facade; 
    public Button inventoryButton; 

    // Pause menu called => in view, Pause menu uncalled=> not in view 
    void Start()
    {
        // facade = new GameUIFacade(); 
        //initializing pause menu state 
        PauseMenu.SetActive(false); 
        IsPaused = false; 
        Time.timeScale = 1f; //ensure time is running on scene start
        //LoadGame(); COMMENTED OUT FOR GAME TESTING PURPOSES

        if (inventoryButton != null)
        {
            inventoryButton.onClick.AddListener(ViewInventory);
        } 
    }

    // Singleton Pattern 
    // https://refactoring.guru/design-patterns/singleton 
    void Awake()
    {
        if (instance != null && instance !=this)
        {
            Destroy(gameObject); // prevent duplicates 
            return; 
        }

        instance = this; 
        DontDestroyOnLoad(gameObject); 
    }

    // How pause menu called, p pressed to either call pause menu or remove it from the screen 
    void Update()
    {
        //Toggle pause state when 'P' is pressed
        if(Input.GetKeyDown(KeyCode.P)){
            Debug.Log("P was pressed"); 
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

        PauseMenu.SetActive(false); //pause menu goes away 
        Time.timeScale=1f; // resuming the game
        IsPaused = false; //game is not paused
        Debug.Log("Game has resumed."); 

        // Play the button click
        AudioManager.instance.Play("ClickSound");
        // facade.PlayClickSound(); 
        // Restore all audio sources
        StartCoroutine(AudioManager.instance.RestoreAudioStates());
        // SceneManager.LoadScene("Level 1");
    }

    //Function to pause the game
    public void PauseGame()
    {
        PauseMenu.SetActive(true); //pause menu called 
        Time.timeScale = 0f; // pausing the game freezing time
        IsPaused = true; //game is paused
        Debug.Log("Game is paused."); 

        // Play the button click
        AudioManager.instance.Play("ClickSound");
        // facade.PlayClickSound(); 

        // Pause all audio sources and save their states
        StartCoroutine(AudioManager.instance.PauseAllAudioSources());
    }

    //save game when the player chooses to save the game
    public void SaveGame()
    {
        // Play the button click sound
        AudioManager.instance.Play("ClickSound");
        // facade.PlayClickSound(); 

        // Assume that you're saving player's position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
            PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
            PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);
            PlayerPrefs.Save();
            Debug.Log("Game Saved: Player's position saved.");
        }
        else
        {
            Debug.Log("Save failed: No player object found.");
        }
    }

    //Load the Game if the player presses the loadgame button
    public void LoadGame()
    {
        // Check if saved data exists
        if (PlayerPrefs.HasKey("PlayerPosX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX");
            float y = PlayerPrefs.GetFloat("PlayerPosY");
            float z = PlayerPrefs.GetFloat("PlayerPosZ");
            Vector3 savedPosition = new Vector3(x, y, z);

            // Load player's position
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = savedPosition;
                Debug.Log("Game Loaded: Player's position restored.");
            }
            else
            {
                Debug.Log("Load failed: No player object found.");
            }
        }
        else
        {
            Debug.Log("No save data to load.");
        }
    }

    //Loads the menu screen when the load menu button is clicked
    public void LoadMenu()
    {
        //reset pause state before loading menu 
        Time.timeScale = 1f; 
        IsPaused = false; 

        // PauseMenu.SetActive(false); //removing the pause menu from the background
        // Play the button click
        AudioManager.instance.Play("ClickSound");
        // facade.PlayClickSound(); 
        AudioManager.instance.Play("MenuBGM");

        Debug.Log("Loading menu.");
        SceneManager.LoadScene("StartMenu");
        // facade.LoadScene("StartMenu"); 
    }

    //Quits the game when the player presses to quit the game
    public void QuitGame()
    {
        // Play the button click
        AudioManager.instance.Play("ClickSound");
        // facade.PlayClickSound(); 

        Debug.Log("Quitting game."); 
        Application.Quit();//quitting the game 
        UnityEditor.EditorApplication.isPlaying = false;
        // facade.QuitApp(); 
    }

// player can view inventory from pause menu 
    public void ViewInventory()
    {
        // Play the button click
        AudioManager.instance.Play("ClickSound");
        Debug.Log("Viewing inventory..."); 

        _menuActivated = !_menuActivated;
        Time.timeScale = _menuActivated ? 0 : 1;

        inventoryMenu.SetActive(_menuActivated);
        PauseMenu.SetActive(!_menuActivated); //taking down pause menu when inventory pulled up. 
    }

}
