using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
            StartCoroutine(waitForPlayerInput(powerUp, playerController));  // wait for player's input
        }
    }

    /* This function is a coroutine to handle waiting for the player's input
     * The choice prompt is not turned off and time is paused until a player clicks either U or L
     * If U, applyEffect() is used
     * If L, addToInventory() is used
     * Choice prompt is hidden after the player makes a choice and time resume */
    private IEnumerator waitForPlayerInput(PowerUp powerUp, PlayerController playerController)
    {
        bool inputReceived = false;

        StartCoroutine(AudioManager.instance.PauseAllAudioSources()); // Pause all audio sources

        while (!inputReceived)
        {
            Time.timeScale = 0;

            // When the player presses 'U' (Use Now)
            if (Input.GetKeyDown(KeyCode.U))
            {
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
                InventoryManager.sInstance.addToInventory(powerUp.itemType.ToString(), powerUp.sprite); // add to inventory
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

