// Pause menu script 

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem; 
using System.Collections; 
using System.Collections.Generic; 

public class PauseManager : MonoBehaviour
{  
    public static bool IsPaused = false; //indicates whether paused or not 
    public GameObject PauseMenu; // pause menu object to be connected with script 
    public static PauseManager Instance; 
    public GameObject ResumeButton; //represents the resume button 
    public GameObject MenuButton; //represents the menu button 
    public string pauseMenuSceneName = "PauseMenu"; 

    private List<AudioSource> allAudioSources = new List<AudioSource>(); // To store all active AudioSources
    private List<bool> audioSourceStates = new List<bool>(); // To store the state, play or pause

    //making sure game objects dont get destroyed when switching scenes 
    private void Awake() 
    {
        // one instance of the pause manager. 
        if(Instance == null)
        {
            Instance = this; 
            DontDestroyOnLoad(gameObject); 
            Debug.Log("PauseManager Initialized"); 
        }
        else
        {
            Debug.LogWarning("Duplicate PauseManager found. Destroying this instance... "); 
            Destroy(gameObject); 
        }
    }

// Pause menu called => in view, Pause menu uncalled=> not in view 
    void Start()
    {
        Debug.Log("Pause Manager script is running ..."); 
        //LoadGame(); COMMENTED OUT FOR GAME TESTING PURPOSES
    }

// How pause menu called, p pressed to either call pause menu or remove it from the screen 
    void Update()
    {
        //Toggle pause state when 'P' is pressed
        if(Input.GetKeyDown(KeyCode.P)){
            Debug.Log("P was pressed!");
            if (IsPaused)  
            {
                IsPaused = false; 
                ResumeGame(); // if already paused then resume game. 
            }
            else
            {
                IsPaused = true; 
                PauseGame(); //if not paused already then pause game 
            }
        } 
    }
    
    //resumes the game when the player presses the resume game button in the pause menu
    public void ResumeGame(){
        Debug.Log("Resume called ... "); 
        // Play the button click
        AudioManager.instance.Play("ClickSound");

        // Restore all audio sources
        StartCoroutine(AudioManager.instance.RestoreAudioStates());

        Time.timeScale=1f; // resuming the game
        // IsPaused = false; //game is not paused 
        SceneManager.UnloadSceneAsync(pauseMenuSceneName); // remove the pause menu scene
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(0)); 
        Debug.Log("Game Resumed");  
        // SceneManager.LoadScene("Level 1"); // after unpaused, moved to level 1
    }

    //Function to pause the game
    void PauseGame(){
        // Play the button click
        AudioManager.instance.Play("ClickSound");

        // Pause all audio sources and save their states
        StartCoroutine(AudioManager.instance.PauseAllAudioSources());
 
        Time.timeScale=0f; // pausing the game 
        // IsPaused = true; //game is paused 
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        Debug.Log("Calling the pause menu scene"); 
    }

    //save game when the player chooses to save the game
    public void SaveGame()
    {
        // Play the button click sound
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
        AudioManager.instance.Play("ClickSound");
        AudioManager.instance.Play("MenuBGM");

        Debug.Log("Loading menu.");
        SceneManager.LoadScene("Start Menu");
    }

    //Quits the game when the player presses to quit the game
    public void QuitGame(){
        // Play the button click
        AudioManager.instance.Play("ClickSound");

        Debug.Log("Quitting game."); 
        Application.Quit();//quitting the game 
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
