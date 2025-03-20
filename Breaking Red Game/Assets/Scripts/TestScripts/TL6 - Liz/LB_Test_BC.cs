using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

public class LB_Test_Scripts : MonoBehaviour
{
    private bool isBCModeEnabled; 

    void Start(){
        isBCModeEnabled = PlayerPrefs.GetInt("BCMode", 0) == 1; 
    }

    //test checks if the BC Mode persists throughout all the diff scenes of the game
    public IEnumerator BCTest(){ 
        Debug.Log("Starting BC Mode test"); 
        if(isBCModeEnabled){
            Debug.Log("BC Mode Enabled! Player is invincible"); 
        }else{
            Debug.Log("BC Mode not enabled."); 
        }
        yield return null; 
    }

    public void runLBTests(){ //function to call all the tests 
        StartCoroutine(BCTest()); //run BC mode test 
    }
}
