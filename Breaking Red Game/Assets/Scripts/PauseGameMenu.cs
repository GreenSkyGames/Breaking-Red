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

    public void LoadMenu(){
        Debug.Log("Loading menu.");
        SceneManager.LoadScene("Start Menu");
    }
    public void QuitGame(){
        Debug.Log("Quitting game."); 
        Application.Quit();//quitting the game 
        UnityEditor.EditorApplication.isPlaying = false;
    }

} //end class 
