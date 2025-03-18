// Liz Beltran 
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
    public static bool isPaused = false; 
    // public GameObject pauseMenuUI; 
    public GameObject PauseMenu; // pause menu object to be connected with script 
    public GameObject ResumeButton; 
    public GameObject MenuButton;

    private List<AudioSource> allAudioSources = new List<AudioSource>(); // To store all active AudioSources
    private List<bool> audioSourceStates = new List<bool>(); // To store the state, play or pause

    void Start(){
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
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            if (isPaused)
            {
                ResumeGame(); 
            }
            else
            {
                PauseGame(); 
            }
        } 
    }

    public void ResumeGame(){
        // Play the button click
        AudioManager.instance.Play("ClickSound");

        // Restore all audio sources
        RestoreAudioStates();

        PauseMenu.SetActive(false); //pause menu goes away 
        Time.timeScale=1f; // resuming the game
        isPaused = false; //game is not paused
        //SceneManager.LoadScene("Level 1");
    }

    void PauseGame(){
        // Play the button click
        AudioManager.instance.Play("ClickSound");

        // Pause all audio sources and save their states
        PauseAllAudioSources();

        PauseMenu.SetActive(true); //pause menu called 
        Time.timeScale=0f; // pausing the game 
        isPaused = true; //game is paused
        //SceneManager.LoadScene("PauseMenu");
    }

    // Save the state of all audio sources
    private void PauseAllAudioSources()
    {
        allAudioSources.Clear();
        audioSourceStates.Clear();

        // Find all AudioSources in the scene and pause them
        foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>())
        {
            allAudioSources.Add(audioSource);
            audioSourceStates.Add(audioSource.isPlaying);

            audioSource.Pause();  // Pause the audio
        }
    }

    // Restore the state of all audio sources
    private void RestoreAudioStates()
    {
        for (int i = 0; i < allAudioSources.Count; i++)
        {
            AudioSource audioSource = allAudioSources[i];

            if (audioSourceStates[i])
            {
                audioSource.UnPause();  // Unpause the audio
            }
        }

        // Clear the stored states after restoring
        allAudioSources.Clear();
        audioSourceStates.Clear();
    }

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

    public void LoadMenu(){
        // Play the button click
        AudioManager.instance.Play("ClickSound");
        AudioManager.instance.Play("MenuBGM");

        Debug.Log("Loading menu.");
        SceneManager.LoadScene("Start Menu");
    }
    public void QuitGame(){
        // Play the button click
        AudioManager.instance.Play("ClickSound");

        Debug.Log("Quitting game."); 
        Application.Quit();//quitting the game 
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
