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

    public void LoadMenu(){
        Debug.Log("Loading menu."); 
        SceneManager.LoadScene("Start Menu"); 
    }

    public void StartGame(){ //when player resumes a previous game 
        Debug.Log("Starting a new game"); 

    }

}
