using UnityEngine;

public class PoisonApple : PowerUpTemplate
{
    public override void applyEffect(PlayerController playerController)
    {
        PlayerHealth playerHealth = playerController.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.changeHealth(-effectAmount);  // Decreases health
            Debug.Log($"Poison Apple: Player health decreased = {playerHealth.currentHealth}");
        }
    }
}
