// Liz Beltran 
// main menu script 

using UnityEngine.SceneManagement; //to switch scenes 
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem; 
using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject QuitButton;
    public GameObject StartButton;

    public void LoadMenu()
    {
        Debug.Log("Loading menu.");
        SceneManager.LoadScene("Start Menu");
    }

    public void StartGame()
    { //when player resumes a previous game 
        Debug.Log("Starting a new game");

        // Play the button click
        AudioManager.instance.Play("ClickSound");
        // Fading out the MenuBGM and fading in the CabinBGM
        StartCoroutine(TransitionToGameScene());
    }

    private IEnumerator TransitionToGameScene()
    {
        // Fade out the MenuBGM
        StartCoroutine(AudioManager.instance.FadeOut("MenuBGM", 1.5f));
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Level 1");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game.");
        Application.Quit();//quitting the game 
        UnityEditor.EditorApplication.isPlaying = false;
        AudioManager.instance.Play("ClickSound"); // Play sound on button click
    }
}
