using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;  // Add this if it's not already at the top
using TMPro;

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
    public CanvasGroup healthMessageGroup;
    public CanvasGroup inventoryFullGroup;

    private PlayerHealth _playerHealth;

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
    public void handlePowerUpInteraction(PowerUpTemplate powerUp, PlayerController playerController)
    {
        if (powerUp == null) // Check if powerUp object is null before proceeding
        {
            Debug.LogWarning("PowerUp object is null!");
            return;
        }
        if (_playerHealth == null)
        {
            _playerHealth = playerController.GetComponent<PlayerHealth>();
        }
        /* poison apple applies immediately */
        if (powerUp is PoisonApple)
        {
            AudioManager.instance.Play("PowerUpSound"); // play power up sound effect
            PoisonApple poisonApple = (PoisonApple)powerUp;
            poisonApple.applyEffect(playerController);
            Destroy(powerUp.gameObject);
        }
        else if (powerUp is OwlsWing || powerUp is CanOfTuna)
        {
            InventoryManager inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
            bool isAdded = inventoryManager.addToInventory(powerUp.itemType.ToString(), powerUp.sprite, powerUp.itemDescription);
            if (isAdded)
            {
                AudioManager.instance.Play("PowerUpSound");
                Destroy(powerUp.gameObject);
            }
            else
            {
                StartCoroutine(showInventoryFullMessage());
            }
            return;
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
    private IEnumerator _waitForPlayerInput(PowerUpTemplate powerUp, PlayerController playerController)
    {
        bool inputReceived = false;

        StartCoroutine(AudioManager.instance.PauseAllAudioSources()); // Pause all audio sources

        while (!inputReceived)
        {
            Time.timeScale = 0;

            // When the player presses 'U' (Use Now)
            if (Input.GetKeyDown(KeyCode.U))
            {   
                // Now we check the type of power-up and apply its effect
                if (powerUp is GoldenApple)
                {
                    if (_playerHealth != null && _playerHealth.currentHealth >= _playerHealth.maxHealth)
                    {
                        StartCoroutine(showHealthMessage());
                    }
                    else
                    {
                        AudioManager.instance.Play("PowerUpSound");
                        GoldenApple goldenApple = (GoldenApple)powerUp;
                        goldenApple.applyEffect(playerController);
                        Destroy(powerUp.gameObject);
                    }
                }
                else if (powerUp is BerserkerBrew)
                {
                    AudioManager.instance.Play("PowerUpSound"); // play power up sound effect
                    BerserkerBrew berserkerBrew = (BerserkerBrew)powerUp;
                    berserkerBrew.applyEffect(playerController);  // Calls the BerserkerBrew-specific method
                    Destroy(powerUp.gameObject);
                }
                else if (powerUp is EnchantedBerry)
                {
                    AudioManager.instance.Play("PowerUpSound"); // play power up sound effect
                    EnchantedBerry enchantedBerry = (EnchantedBerry)powerUp;
                    enchantedBerry.applyEffect(playerController);
                    Destroy(powerUp.gameObject);
                }
                else if (powerUp is RedShoes)
                {
                    AudioManager.instance.Play("PowerUpSound");
                    RedShoes redShoes = (RedShoes)powerUp;
                    redShoes.applyEffect(playerController);
                    Destroy(powerUp.gameObject);
                }
                else
                {
                    AudioManager.instance.Play("PowerUpSound"); // play power up sound effect
                    powerUp.applyEffect(playerController);  // Calls the base method for other power-ups
                }

                inputReceived = true;
            }
            else if (Input.GetKeyDown(KeyCode.L))  // 'L' for Store for Later
            {
                InventoryManager inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
                bool isAdded = inventoryManager.addToInventory(powerUp.itemType.ToString(), powerUp.sprite, powerUp.itemDescription);
                if (isAdded)
                {
                    AudioManager.instance.Play("ClickSound"); // play click sound effect
                    Destroy(powerUp.gameObject);
                    inputReceived = true;
                }
                else
                {
                    inputReceived = true;
                    StartCoroutine(showInventoryFullMessage());
                }
            }

            yield return null;
        }

        /* hide the prompt after the player makes a choice */
        Debug.Log("Choice prompt hidden");
        Time.timeScale = 1;

        StartCoroutine(AudioManager.instance.RestoreAudioStates()); // Restore all audio sources
        choicePrompt.SetActive(false);
    }

    private IEnumerator showHealthMessage()
    {
        Debug.Log("Turning on message");
        healthMessageGroup.alpha = 1f;
        healthMessageGroup.blocksRaycasts = true;
        yield return new WaitForSecondsRealtime(1.5f);
        healthMessageGroup.alpha = 0f;
        Time.timeScale = 1;
        choicePrompt.SetActive(false);
        StartCoroutine(AudioManager.instance.RestoreAudioStates());
    }

    private IEnumerator showInventoryFullMessage()
    {
        if (inventoryFullGroup != null)
        {
            inventoryFullGroup.alpha = 1f;
            inventoryFullGroup.blocksRaycasts = true;
            yield return new WaitForSeconds(2f);  // How long to show message
            inventoryFullGroup.alpha = 0f;
            inventoryFullGroup.blocksRaycasts = false;
        }
    }
}

