using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth = 3;
    public int maxHealth = 3;

    public SpriteRenderer playerSr;
    public PlayerController playerMovement; // Assuming this controls movement

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        Debug.Log("Health " + currentHealth);

        if (currentHealth <= 0)
        {
            // Disable rendering and movement, but keep the object active
            if (playerSr != null)
            {
                playerSr.enabled = false;
            }

            if (playerMovement != null)
            {
                playerMovement.enabled = false; //disable the player controller script
            }
            UnityEditor.EditorApplication.isPlaying = false;

            // Optionally, you might want to add other game over logic here,
            // such as displaying a game over screen, triggering events, etc.
        }
        else
        {
            //if you want to re-enable them when gaining health back, add this, or similar logic.
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

    // Function to re-enable the player (if needed later)
    public void ReEnablePlayer()
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
