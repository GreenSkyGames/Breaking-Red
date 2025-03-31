// Game over screen 

using UnityEngine;
using System.Collections; 
using System.Collections.Generic;
using UnityEngine.SceneManagement; //to switch scenes 
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem; 

public class UIScreenManager : MonoBehaviour
{
    public GameObject gameScreenUI; //represents the game over screen or the victory screen 
    public GameObject restart; //represents the restart button in the game over screen 
    public GameObject mainMenu; //represents the main menu button (in both game over and victory screens
    public GameObject quitGame; //represents the quit button in the screens 

    // calling the main menu. 
    public void LoadMenu()
    {
        Debug.Log("Loading menu.");
        SceneManager.LoadScene(0); //start menu at build profile 0 
    } 

    //quitting the game 
    public void QuitGame()
    {
        Debug.Log("Quitting game.");
        Application.Quit();//quitting the game 
        UnityEditor.EditorApplication.isPlaying = false;
        AudioManager.instance.Play("ClickSound"); // Play sound on button click
    } 

//restarting a level when die (important for game over screen)
    public void Restart()
    {
        //Figure out logistics later 
    }

}
