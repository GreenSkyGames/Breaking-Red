using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    public GameObject choicePrompt;  // Use Now or Store for Later UI

    public void HandlePowerUpInteraction(PowerUp powerUp, PlayerController playerController)
    {
        // poison apple applies immediately
        if (powerUp.type == PowerUp.itemName.PoisonApple)
        {
            powerUp.ApplyEffect(playerController);
        }
        else
        {
            // for other power-ups, prompt the player with a choice
            choicePrompt.SetActive(true);  // Show the UI prompt
            StartCoroutine(WaitForPlayerInput(powerUp, playerController));  // wait for player's input
        }
    }

    // coroutine to handle waiting for the player's input
    private IEnumerator WaitForPlayerInput(PowerUp powerUp, PlayerController playerController)
    {
        bool inputReceived = false;

        while (!inputReceived)
        {
            if (Input.GetKeyDown(KeyCode.U))  // 'U' for Use Now
            {
                powerUp.ApplyEffect(playerController);  // apply the power-up effect
                inputReceived = true;
            }
            else if (Input.GetKeyDown(KeyCode.S))  // 'S' for Store for Later
            {
                // add to inventory for later use
                InventoryManager.instance.AddToInventory(powerUp);
                inputReceived = true;
            }

            yield return null;
        }

        // hide the prompt after the player makes a choice
        choicePrompt.SetActive(false);
    }
}

