using UnityEngine;

/* Name: Shan Peck
*	Role: Team Lead 4 -- Project Manager
*	
*	This file contains the definition for the PowerUp class
*   It manages showing inventory and adding things to it
*	It inherits from MonoBehaviour */

public class PowerUp : MonoBehaviour
{
    public int effectAmount;
    public enum itemName { PoisonApple, GoldenApple, BerserkerBrew } // enum has power-up types
    public itemName itemType;  // type of the power-up
    public Sprite sprite;

    private InventoryManager _inventoryManager;

    /* This function just gets the InventoryManager instance */
    void Start()
    {
        _inventoryManager = InventoryManager.sInstance;
    }

    /* This function reacts when the Player collides with a power-up
     * It references the PowerUp, PowerUp Manager, and PlayerController scripts
     * uses the handlePowerUpInteraction function from PowerUp Manager */
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.Play("PowerUpSound"); // play power up sound effect
            
            PowerUpManager powerUpManager = other.GetComponent<PowerUpManager>(); // uses PowerUpManager script

            if (powerUpManager != null)
            {
                powerUpManager.handlePowerUpInteraction(this, other.GetComponent<PlayerController>());
            }
        }
    }

    /* This function details the effects of each power-up based on name
     * Uses changeHealth() from PlayerHealth script and given effectAmount */
    public void applyEffect(PlayerController playerController)
    {
        PlayerHealth playerHealth = playerController.GetComponent<PlayerHealth>();
        if(playerHealth == null)
        {
            return;
        }
        switch (itemType)
        {
            case itemName.PoisonApple:
                playerHealth.changeHealth(-effectAmount);  // decreases health
                Debug.Log($"Player health decreased = {playerHealth.currentHealth}");
                break;

            case itemName.GoldenApple:
                playerHealth.changeHealth(effectAmount);  // increases health
                Debug.Log($"Player health increased = {playerHealth.currentHealth}");
                break;
            
            case itemName.BerserkerBrew:
                //playerController.attackPower += effectAmount;
                Debug.Log("Player attack power increased");
                break;

            default:
                Debug.Log("No effect applied.");
                break;
        }
    }
}


