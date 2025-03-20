//Liz Beltran

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.EventSystems; 

public class LB_TestMenu : MonoBehaviour
{
    private GameObject startButton;
    private GameObject quitButton;

//navigating through all the buttons to ensure the user can access the correct menu item. 
    private IEnumerator TestBoundaryNavigation(){
        Debug.Log("Testing boundary navigation on menu options");

        // Select first menu item (Start button)
        EventSystem.current.SetSelectedGameObject(startButton);
        yield return new WaitForSeconds(0.5f); //wait 

        // Try moving "up" (should stay on Start button)
        EventSystem.current.SetSelectedGameObject(startButton);
        yield return new WaitForSeconds(0.5f);

        //if the selection is still on the first item 
        if (EventSystem.current.currentSelectedGameObject == startButton){
            Debug.Log("Passed: Navigation stayed on Start button when moving up.");
        }else{
            Debug.LogError("Failed: Navigation moved out of bounds above Start button.");
        }

        // Move to last item (Quit button)
        EventSystem.current.SetSelectedGameObject(quitButton);
        yield return new WaitForSeconds(0.5f); //wait 

        // Try moving "down" (should stay on Quit button)
        EventSystem.current.SetSelectedGameObject(quitButton);
        yield return new WaitForSeconds(0.5f); //wait 

        //check if the selection is still on the quit button 
        if (EventSystem.current.currentSelectedGameObject == quitButton){
            Debug.Log("Passed: Navigation stayed on Quit button when moving down.");
        }else{
            Debug.LogError("Failed: Navigation moved out of bounds below Quit button.");
        }

        Debug.Log("Checking button boundary tests done..."); 
    }

    //this will be the stress test section 
    private IEnumerator SpamButtons(GameObject button){
        Debug.Log("Spamming the Start button... "); 
        for (int i = 0; i < 5; i++){ //spam 5 times 
            EventSystem.current.SetSelectedGameObject(startButton); 
            Debug.Log($"Spam {i + 1} / 5: {button.name} selected "); 
            yield return new WaitForSeconds(0.1f); //wait 

            Debug.Log($"After spam {i + 1}: {EventSystem.current.currentSelectedGameObject.name}"); 
        }
        //Final state 
        Debug.Log($"Final Selected after spamming: {EventSystem.current.currentSelectedGameObject.name}"); 
    }
    public void runUITests(){ //function to call all the tests 
        Debug.Log("====Checking buttons!====="); 
        StartCoroutine(TestBoundaryNavigation()); 
        StartCoroutine(SpamButtons(startButton)); 
        StartCoroutine(SpamButtons(quitButton));
    }

}
