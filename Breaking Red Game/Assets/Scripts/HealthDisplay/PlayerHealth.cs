/* File Heading Example */
/*
 * Name:  Mark Eldridge
 * Role:   Main Character Customization
 * This file contains the definition for the PlayerHealth class.
 * This class manages the player's health.
 * It inherits from MonoBehaviour.
 */

using UnityEngine;
using UnityEngine.SceneManagement; //to change scenes when game over 

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth = 100;
    public int maxHealth = 100;

    public SpriteRenderer playerSr;
    public PlayerController playerMovement; // Controls player movement

    /* This function is called before the first frame update.
     * It initializes the player's health. */
    void start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    /* This function changes the player's health by a specified amount.
     * @param amount The amount to change the player's health by. */
    public void changeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        //Debug.Log($"Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            // Disable rendering and movement, but keep the object active
            if (playerSr != null)
            {
                playerSr.enabled = false;
            }

            if (playerMovement != null)
            {
                playerMovement.enabled = false; // Disable player movement
            }
            // UnityEditor.EditorApplication.isPlaying = false;
            Debug.Log("Player has died.");
            SceneManager.LoadScene("GameOver"); //calling game over screen 
        }
        else
        {
            // Re-enable if gaining health
            if (playerSr != null && !playerSr.enabled)
            {
                playerSr.enabled = true;
            }

            if (playerMovement != null && !playerMovement.enabled)
            {
                playerMovement.enabled = true;
            }
        }
    }

    /* This function re-enables the player. */
    public void reEnablePlayer()
    {
        if (playerSr != null)
        {
            playerSr.enabled = true;
        }

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
        currentHealth = maxHealth;
    }
}