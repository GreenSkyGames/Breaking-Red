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
        PauseMenu.SetActive(false);
    }


    void Update(){
        _select = Input.GetAxis("Vertical");
        if(Input.GetKeyDown(KeyCode.P) || (Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame)){
            EventSystem.current.SetSelectedGameObject(ResumeButton);
            ButtonSelect = 1;
            //ResumeButton.color = HighlightedColor;
            if(_select == -1)
            {
                if(ButtonSelect == 1)
                {
                    EventSystem.current.SetSelectedGameObject(MenuButton);
                    ButtonSelect = 2;

                }
                else if(ButtonSelect == 2)
                {
                    EventSystem.current.SetSelectedGameObject(Quitbutton);
                    ButtonSelect = 3;
                }
                else if(ButtonSelect == 1)
                {
                    EventSystem.current.SetSelectedGameObject(ResumeButton);
                    ButtonSelect = 1;
                }
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

    public void pauseGame(){
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

    }

    public void resumeGame(){
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void mainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("mainMenu");
    }

    public void quitGame(){
        Application.Quit();
    }
}