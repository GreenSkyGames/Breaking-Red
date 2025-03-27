using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Enum to define different types of power-ups
    public int effectAmount;
    public enum itemName { PoisonApple, GoldenApple, BerserkerBrew }
    public itemName itemType;  // Type of the power-up
    public Sprite sprite;

    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = InventoryManager.sInstance;
    }

    // when player collides
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // play power up sound effect tbd
            //AudioManager.instance.Play("PowerUpSound");
            
            // trigger PowerUpManager to handle the interaction
            PowerUpManager powerUpManager = other.GetComponent<PowerUpManager>();
            if (powerUpManager != null)
            {
                powerUpManager.handlePowerUpInteraction(this, other.GetComponent<PlayerController>());
            }
        }
    }

    // apply effect to the player
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


