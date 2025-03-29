// main menu script 

using UnityEngine;
using UnityEngine.SceneManagement; //to switch scenes 
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem; 
using System.Collections; 
using System.Collections.Generic;


public class MainMenu : MonoBehaviour
{
    public GameObject QuitButton;
    public GameObject StartButton;

// calling the main menu. 
    public void LoadMenu()
    {
        Debug.Log("Loading menu.");
        SceneManager.LoadScene("Start Menu");
    }

// Player decides to resume a previous game 
    //Starts the game when the start game button is pressed
    public void StartGame()
    { 
        Debug.Log("Starting a new game");

        // Play the button click
        AudioManager.sinstance.Play("ClickSound");
        SceneManager.LoadScene("CharacterSelector");
    }

// Player decides to Quit the game 
    
    //Quits the game when the button is trigerred for quitgame
    public void QuitGame()
    {
        Debug.Log("Quitting game.");
        Application.Quit();//quitting the game 
        UnityEditor.EditorApplication.isPlaying = false;
        AudioManager.sinstance.Play("ClickSound"); // Play sound on button click
    }
}
