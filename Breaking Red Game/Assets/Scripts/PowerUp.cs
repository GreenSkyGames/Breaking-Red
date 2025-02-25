using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Enum to define different types of power-ups
    public enum itemName { PoisonApple, GoldenApple }

    public itemName type;  // Type of the power-up
    public float effectAmount;  // Amount of effect (e.g., how much health is reduced or increased)

    // Apply the effect of the power-up to the player
    public void ApplyEffect(PlayerController playerController)
    {
        switch (type)
        {
            case itemName.PoisonApple:
                playerController.health -= effectAmount;  // Poison apple decreases health
                Debug.Log("Player health decreased");
                break;

            case itemName.GoldenApple:
                playerController.health += effectAmount;  // Health boost increases health
                break;
        }
    }

    // Detect when the player collides with the power-up
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the PowerUpManager and call ApplyPowerUpEffect
            PowerUpManager powerUpManager = other.GetComponent<PowerUpManager>();
            if (powerUpManager != null)
            {
                powerUpManager.ApplyPowerUpEffect(this, other.GetComponent<PlayerController>());
            }

            // Destroy the power-up object after being collected
            Destroy(gameObject);
        }
    }
}

