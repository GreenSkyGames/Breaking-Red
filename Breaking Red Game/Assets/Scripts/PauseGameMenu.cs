// Puase menu script 

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem; 
//using System.Collections; 
using System.Collections.Generic; // 

public class PauseGameMenu : MonoBehaviour
{  
    public static bool IsPaused = false; 
    // public GameObject pauseMenuUI; 
    public GameObject PauseMenu; // pause menu object to be connected with script 
    public GameObject ResumeButton; 
    public GameObject MenuButton;

    private List<AudioSource> allAudioSources = new List<AudioSource>(); // To store all active AudioSources
    private List<bool> audioSourceStates = new List<bool>(); // To store the state, play or pause

// Pause menu called => in view, Pause menu uncalled=> not in view 
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "PauseMenu")
        {
            PauseMenu.SetActive(true);  // Show menu in PauseMenu scene
        }
        else
        {
            PauseMenu.SetActive(false); // Hide menu in other scenes
        }
        //LoadGame(); COMMENTED OUT FOR GAME TESTING PURPOSES
    }

// How pause menu called, p pressed to either call pause menu or remove it from the screen 
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
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

// When game is resumed ... 
    public void ResumeGame()
    {
        // Play the button click
        AudioManager.instance.Play("ClickSound");

        // Restore all audio sources
        StartCoroutine(AudioManager.instance.RestoreAudioStates());

        PauseMenu.SetActive(false); //pause menu goes away 
        Time.timeScale=1f; // resuming the game
        IsPaused = false; //game is not paused
        //SceneManager.LoadScene("Level 1");
    }

//when game is paused ... 
    void PauseGame()
    {
        // Play the button click
        AudioManager.instance.Play("ClickSound");

        // Pause all audio sources and save their states
        StartCoroutine(AudioManager.instance.PauseAllAudioSources());

        PauseMenu.SetActive(true); //pause menu called 
        Time.timeScale=0f; // pausing the game 
        IsPaused = true; //game is paused
        //SceneManager.LoadScene("PauseMenu");
    }

// save button on pause menu, save player position 
    public void SaveGame()
    {
        // Play the button click
        AudioManager.instance.Play("ClickSound");

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

// load game, previous game state recalled. 
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
// main menu button clicked, function calls the Main menu 
    public void LoadMenu()
    {
        // Play the button click
        AudioManager.instance.Play("ClickSound");
        AudioManager.instance.Play("MenuBGM");

        Debug.Log("Loading menu.");
        SceneManager.LoadScene("Start Menu");
    }
// quit game option clicked. Game quit ... 
    public void QuitGame()
    {
        // Play the button click
        AudioManager.instance.Play("ClickSound");

        Debug.Log("Quitting game."); 
        Application.Quit();//quitting the game 
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
