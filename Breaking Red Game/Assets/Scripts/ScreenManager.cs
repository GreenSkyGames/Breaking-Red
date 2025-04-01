// Game over screen and victory screens! 

using UnityEngine;
using System.Collections; 
using System.Collections.Generic;
using UnityEngine.SceneManagement; //to switch scenes 
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem; 

public class UIScreenManager : MonoBehaviour
{
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
        Debug.Log("Will restart at beginning of level you died at. TBD"); 
    }

}
