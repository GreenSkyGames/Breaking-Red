using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;  // Add this if it's not already at the top

/* Name: Shan Peck
*	Role: Team Lead 4 -- Project Manager
*	
*	This file contains the definition for the PowerUpManager class
*   It manages what happens after the player collides with a power-up
*	It inherits from MonoBehaviour */

public class PowerUpManager : MonoBehaviour
{
    public GameObject choicePrompt;  // Use Now or Store for Later UI
    public Image[] inventorySlots;
    //public PlayerHealth playerHealth;

    void Start()
    {
        // Search all root objects in the scene (including inactive)
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject obj in rootObjects)
        {
            Transform t = FindInChildren(obj.transform, "choicePrompt");
            if (t != null)
            {
                choicePrompt = t.gameObject;
                Debug.Log("choicePrompt found: " + choicePrompt.name);
                return;
            }
        }
    }

    // Recursive function to find by name (even inactive)
    Transform FindInChildren(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child;
            }
            Transform found = FindInChildren(child, name);
            if (found != null)
            {
                return found;
            }
        }
        return null;
    }

    /* This function shows the choicePrompt for all except poison apple
     * For poison apple, applyEffect() is used
     * For others, SetActive() for the choice prompt is set to true and a coroutine starts and uses waitForPlayerInput() */
    public void handlePowerUpInteraction(PowerUp powerUp, PlayerController playerController)
    {
        if (powerUp == null) // Check if powerUp object is null before proceeding
        {
            Debug.LogWarning("PowerUp object is null!");
            return;
        }
        /* poison apple applies immediately */
        if (powerUp is PoisonApple)
        {
            AudioManager.instance.Play("PowerUpSound"); // play power up sound effect
            PoisonApple poisonApple = (PoisonApple)powerUp;
            poisonApple.v_applyEffect(playerController);
            Destroy(powerUp.gameObject);
        }
        else
        {
            /* for other power-ups, prompt the player with a choice */
            choicePrompt.SetActive(true);  // show the UI prompt
            StartCoroutine(_waitForPlayerInput(powerUp, playerController));  // wait for player's input
        }
    }

    /* This function is a coroutine to handle waiting for the player's input
     * The choice prompt is not turned off and time is paused until a player clicks either U or L
     * If U, applyEffect() is used
     * If L, addToInventory() is used
     * Choice prompt is hidden after the player makes a choice and time resume */
    private IEnumerator _waitForPlayerInput(PowerUp powerUp, PlayerController playerController)
    {
        bool inputReceived = false;

        StartCoroutine(AudioManager.instance.PauseAllAudioSources()); // Pause all audio sources

        while (!inputReceived)
        {
            Time.timeScale = 0;

            // When the player presses 'U' (Use Now)
            if (Input.GetKeyDown(KeyCode.U))
            {
                /*
                if(playerHealth.currentHealth >= playerHealth.maxHealth)
                {
                    //messageText.text = "Your health is already full!";
                    yield return new WaitForSecondsRealtime(2f);
                    Time.timeScale = 1;
                    choicePrompt.SetActive(false);
                    yield break;
                }
                */
                AudioManager.instance.Play("PowerUpSound"); // play power up sound effect
                
                // Now we check the type of power-up and apply its effect
                if (powerUp is GoldenApple)
                {
                    GoldenApple goldenApple = (GoldenApple)powerUp;
                    goldenApple.v_applyEffect(playerController);  // Calls the GoldenApple-specific method
                }
                else if (powerUp is BerserkerBrew)
                {
                    BerserkerBrew berserkerBrew = (BerserkerBrew)powerUp;
                    berserkerBrew.v_applyEffect(playerController);  // Calls the BerserkerBrew-specific method
                }
                else if (powerUp is EnchantedBerry)
                {
                    EnchantedBerry enchantedBerry = (EnchantedBerry)powerUp;
                    enchantedBerry.v_applyEffect(playerController);
                }
                else
                {
                    powerUp.v_applyEffect(playerController);  // Calls the base method for other power-ups
                }

                Destroy(powerUp.gameObject);
                inputReceived = true;
            }
            else if (Input.GetKeyDown(KeyCode.L))  // 'L' for Store for Later
            {
                AudioManager.instance.Play("ClickSound"); // play click sound effect
                InventoryManager.sInstance.addToInventory(powerUp.itemType.ToString(), powerUp.sprite, powerUp.itemDescription); // add to inventory
                Destroy(powerUp.gameObject);
                inputReceived = true;
            }

            yield return null;
        }

        /* hide the prompt after the player makes a choice */
        Debug.Log("Choice prompt hidden");
        Time.timeScale = 1;

        StartCoroutine(AudioManager.instance.RestoreAudioStates()); // Restore all audio sources
        choicePrompt.SetActive(false);
    }
}

