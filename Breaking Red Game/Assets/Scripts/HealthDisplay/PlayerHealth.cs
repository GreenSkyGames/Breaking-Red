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

    private bool bcMode = false; //bc mode 

    /* This function is called before the first frame update.
     * It initializes the player's health. */
    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;

        //update BC mode 
        UpdateBCMode(); //Liz 
    }

    /* This function changes the player's health by a specified amount.
     * @param amount The amount to change the player's health by. */
    public void changeHealth(int amount)
    {
        if (bcMode)
        {
            Debug.Log("BC Mode on, player isn't taking damage!");
        }
        else
        {
            Debug.Log("BC Mode not on");
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            //Debug.Log($"Health: {currentHealth}");
        }

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

            if(!bcMode) //if BC mode not enabled then 
            {
                // UnityEditor.EditorApplication.isPlaying = false;
                Debug.Log("Player has died.");
                SceneManager.LoadScene("GameOver"); //calling game over screen 
            }
            else //if BC mode is enabled 
            {
                currentHealth = maxHealth; // BC is invinvible 
            }
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

    private void UpdateBCMode(){
        bcMode = PlayerPrefs.GetInt("BCMode", 0) == 1;
    }
}