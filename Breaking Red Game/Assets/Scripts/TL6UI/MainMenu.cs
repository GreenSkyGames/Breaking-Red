// main menu script 

/* Name: Liz Beltran 
 * Role: Team Lead 6 --Version Control Manager
 * This is the script for the main menu. This file will define
 * the behavior of the main menu, including: 
 * load menu, start game, quit game, and see credits. 
*/

using UnityEngine;
using UnityEngine.SceneManagement; //to switch scenes 
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem; 
using System.Collections; 
using System.Collections.Generic;

//implementing facade pattern to this code
public class GameUIFacade
{
    public void PlayClickSound()
    {
        AudioManager.instance.Play("ClickSound"); 
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log($"Loading scene: {sceneName}"); 
        SceneManager.LoadScene(sceneName); 
    }

    public void QuitApp(){
        Debug.Log("Quitting game."); 
        Application.Quit(); 
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#endif
    }
}

public class MainMenu : MonoBehaviour
{
    private GameUIFacade facade; 

    private void Awake()
    {
        facade = new GameUIFacade(); 
    }

    // calling the main menu. 
    public void LoadMenu()
    {
        Debug.Log("Loading menu.");
        // SceneManager.LoadScene("StartMenu");
        facade.LoadScene("StartMenu"); 
    }

    // Player decides to resume a previous game??? 
    //Starts the game when the start game button is pressed
    public void StartGame()
    { 
        Debug.Log("Starting a new game");

        // Play the button click
        // AudioManager.instance.Play("ClickSound");
        facade.PlayClickSound(); 
        // SceneManager.LoadScene("CharacterSelector");
        facade.LoadScene("CharacterSelector"); 
    }

    // Player decides to Quit the game 
    //Quits the game when the button is trigerred for quitgame
    public void QuitGame()
    {
        Debug.Log("Quitting game.");
        // Application.Quit();//quitting the game 
        // UnityEditor.EditorApplication.isPlaying = false;
        // AudioManager.instance.Play("ClickSound"); // Play sound on button click
        facade.PlayClickSound(); 
        facade.QuitApp(); 
    }

    public void SeeCredits(){
        Debug.Log("Seeing Credits...."); 
        facade.PlayClickSound(); 
        facade.LoadScene("Credits"); 
        // SceneManager.LoadScene("Credits"); //credits scene 
        // AudioManager.instance.Play("ClickSound"); // Play sound on button click
    }
}
