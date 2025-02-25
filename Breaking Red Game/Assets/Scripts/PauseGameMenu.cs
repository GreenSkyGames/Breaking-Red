// Liz Beltran 
// Puase menu script 

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseGameMenu : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject ResumeButton;
    public GameObject MenuButton;
    public GameObject Quitbutton;

    private int ButtonSelect;
    private float _select;
    public static bool isPaused; //make global variable so no other inputs during pause
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
            if(_select == 1)
            {
                if(ButtonSelect == 1)
                {
                    EventSystem.current.SetSelectedGameObject(Quitbutton);
                    ButtonSelect = 3;
                }
                else if(ButtonSelect == 2)
                {
                    EventSystem.current.SetSelectedGameObject(ResumeButton);
                    ButtonSelect = 1;
                }
                else if(ButtonSelect == 3)
                {
                    EventSystem.current.SetSelectedGameObject(MenuButton);
                    ButtonSelect = 2;
                }
            }
            
            if(isPaused){
                resumeGame();
            }else{
                pauseGame();
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

    public void quitGame(){
        Application.Quit();
    }
}