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

    void Start(){
        if (SceneManager.GetActiveScene().name == "PauseMenu")
        {
            PauseMenu.SetActive(true);  // Show menu in PauseMenu scene
        }
        else
        {
            PauseMenu.SetActive(false); // Hide menu in other scenes
        }
        LoadGame();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            if(isPaused)
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
        PauseMenu.SetActive(false); //pause menu goes away 
        Time.timeScale=1f; // resuming the game
        isPaused = false; //game is not paused
        //SceneManager.LoadScene("Level 1");
    }

    void PauseGame(){
        PauseMenu.SetActive(true); //pause menu called 
        Time.timeScale=0f; // pausing the game 
        isPaused = true; //game is paused
        //SceneManager.LoadScene("PauseMenu");
    }
    public void SaveGame()
    {
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
        Debug.Log("Loading menu.");
        SceneManager.LoadScene("Start Menu");
    }
    public void QuitGame(){
        Debug.Log("Quitting game."); 
        Application.Quit();//quitting the game 
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
