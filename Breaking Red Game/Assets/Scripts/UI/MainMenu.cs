// main menu script 

/* Name: Liz Beltran 
 * Role: Team Lead 6 --Version Control Manager
 *	This is the script for the --- .
 *
*/

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
        SceneManager.LoadScene("StartMenu");
    }

// Player decides to resume a previous game 
    //Starts the game when the start game button is pressed
    public void StartGame()
    { 
        Debug.Log("Starting a new game");

        // Play the button click
        AudioManager.instance.Play("ClickSound");
        SceneManager.LoadScene("CharacterSelector");
    }

// Player decides to Quit the game 
    
    //Quits the game when the button is trigerred for quitgame
    public void QuitGame()
    {
        Debug.Log("Quitting game.");
        Application.Quit();//quitting the game 
        UnityEditor.EditorApplication.isPlaying = false;
        AudioManager.instance.Play("ClickSound"); // Play sound on button click
    }

    public void SeeCredits(){
        Debug.Log("Seeing Credits...."); 
        SceneManager.LoadScene("Credits"); //credits scene 
        AudioManager.instance.Play("ClickSound"); // Play sound on button click
    }
}
