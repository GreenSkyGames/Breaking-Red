// Liz Beltran 
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

    public void LoadMenu()
    {
        Debug.Log("Loading menu.");
        SceneManager.LoadScene("Start Menu");
    }
    //Starts the game when the start game button is pressed
    public void StartGame()
    { 
        Debug.Log("Starting a new game");

        // Play the button click
        AudioManager.instance.Play("ClickSound");
        SceneManager.LoadScene("CharacterSelector");
    }
    
    //Quits the game when the button is trigerred for quitgame
    public void QuitGame()
    {
        Debug.Log("Quitting game.");
        Application.Quit();//quitting the game 
        UnityEditor.EditorApplication.isPlaying = false;
        AudioManager.instance.Play("ClickSound"); // Play sound on button click
    }
}
