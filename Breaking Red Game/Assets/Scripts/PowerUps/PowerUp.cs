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
    public enum itemName { PoisonApple, GoldenApple, BerserkerBrew, EnchantedBerry, RedShoes, StoryBook, SecretScroll, OwlsWing, CanOfTuna, } // enum has power-up types
    public itemName itemType;  // type of the power-up
    public Sprite sprite;
    
    [TextArea] public string itemDescription;

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
            // play power up sound effect tbd
            //AudioManager.instance.Play("PowerUpSound");
            
            PowerUpManager powerUpManager = other.GetComponent<PowerUpManager>(); // uses PowerUpManager script

            if (powerUpManager != null)
            {
                if (itemType == itemName.StoryBook || itemType == itemName.SecretScroll)
                {

                }
                else
                {
                    powerUpManager.handlePowerUpInteraction(this, other.GetComponent<PlayerController>());
                }
            }
        }
    }

    /* This function details the effects of each power-up based on name
     * Uses changeHealth() from PlayerHealth script and given effectAmount */
    public virtual void v_applyEffect(PlayerController playerController)
    {
       Debug.Log("Base power-up effect");
    }
}
