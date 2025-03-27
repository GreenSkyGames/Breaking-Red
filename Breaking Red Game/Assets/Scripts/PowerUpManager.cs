using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    public GameObject choicePrompt;  // Use Now or Store for Later UI
    public Image[] inventorySlots;

    public void handlePowerUpInteraction(PowerUp powerUp, PlayerController playerController)
    {
        // poison apple applies immediately
        if (powerUp.itemType == PowerUp.itemName.PoisonApple)
        {
            powerUp.applyEffect(playerController);
            Destroy(powerUp.gameObject);
        }
        else
        {
            // for other power-ups, prompt the player with a choice
            choicePrompt.SetActive(true);  // Show the UI prompt
            StartCoroutine(waitForPlayerInput(powerUp, playerController));  // wait for player's input
        }
    }

    // coroutine to handle waiting for the player's input
    private IEnumerator waitForPlayerInput(PowerUp powerUp, PlayerController playerController)
    {
        bool inputReceived = false;

        while (!inputReceived)
        {
            Time.timeScale = 0;
            if (Input.GetKeyDown(KeyCode.U))  // 'U' for Use Now
            {
                powerUp.applyEffect(playerController);  // apply the power-up effect
                Destroy(powerUp.gameObject);
                inputReceived = true;
            }
            else if (Input.GetKeyDown(KeyCode.L))  // 'L' for Store for Later
            {
                // add to inventory for later use
                InventoryManager.sInstance.addToInventory(powerUp.itemType.ToString(), powerUp.sprite);
                Debug.Log("Item added to inventory");
                Destroy(powerUp.gameObject);
                inputReceived = true;
            }

            yield return null;
        }

        // hide the prompt after the player makes a choice
        Debug.Log("Choice prompt hidden");
        Time.timeScale = 1;
        choicePrompt.SetActive(false);
    }
}

