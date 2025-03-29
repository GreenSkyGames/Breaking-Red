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
        //Toggle pause state when 'P' is pressed
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
    
    //resumes the game when the player presses the resume game button in the pause menu
    public void ResumeGame(){
        // Play the button click
        AudioManager.sinstance.Play("ClickSound");

        // Restore all audio sources
        StartCoroutine(AudioManager.sinstance.restoreAudioStates());

        PauseMenu.SetActive(false); //pause menu goes away 
        Time.timeScale=1f; // resuming the game
        IsPaused = false; //game is not paused
        //SceneManager.LoadScene("Level 1");
    }

    //Function to pause the game
    void PauseGame(){
        // Play the button click
        AudioManager.sinstance.Play("ClickSound");

        // Pause all audio sources and save their states
        StartCoroutine(AudioManager.sinstance.pauseAllAudioSources());

        PauseMenu.SetActive(true); //pause menu called 
        Time.timeScale=0f; // pausing the game 
        IsPaused = true; //game is paused
        //SceneManager.LoadScene("PauseMenu");
    }

    //save game when the player chooses to save the game
    public void SaveGame()
    {
        // Play the button click sound
        AudioManager.sinstance.Play("ClickSound");

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
    public void LoadMenu(){
        // Play the button click
        AudioManager.sinstance.Play("ClickSound");
        AudioManager.sinstance.Play("MenuBGM");

        Debug.Log("Loading menu.");
        SceneManager.LoadScene("Start Menu");
    }

    //Quits the game when the player presses to quit the game
    public void QuitGame(){
        // Play the button click
        AudioManager.sinstance.Play("ClickSound");

        Debug.Log("Quitting game."); 
        Application.Quit();//quitting the game 
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
