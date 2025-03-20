using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Enum to define different types of power-ups
    public enum itemName { PoisonApple, GoldenApple, BerserkerBrew }
    public itemName type;  // Type of the power-up
    public Sprite sprite;
    private InventoryManager inventoryManager;
    public int effectAmount;

    void Start()
    {
        inventoryManager = InventoryManager.instance;
    }
    // apply effect to the player
    public void ApplyEffect(PlayerController playerController)
    {
        PlayerHealth playerHealth = playerController.GetComponent<PlayerHealth>();
        if(playerHealth == null) return;
        switch (type)
        {
            case itemName.PoisonApple:
                playerHealth.ChangeHealth(-effectAmount);  // decreases health
                Debug.Log("Player health decreased");
                break;

            case itemName.GoldenApple:
                playerHealth.ChangeHealth(effectAmount);  // increases health
                Debug.Log("Player health increased");
                break;
            
            case itemName.BerserkerBrew:
                //playerController.speed += effectAmount;
                Debug.Log("Player attack power increased");
                break;
        }
    }

    // when player collides
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // play power up sound effect tbd
            //AudioManager.instance.Play("PowerUpSound");
            
            // trigger PowerUpManager to handle the interaction
            PowerUpManager powerUpManager = other.GetComponent<PowerUpManager>();
            if (powerUpManager != null)
            {
                powerUpManager.HandlePowerUpInteraction(this, other.GetComponent<PlayerController>());
            }
        }
    }
}


