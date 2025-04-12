using UnityEngine;

public class GoldenApple : PowerUpTemplate
{
    public override void applyEffect(PlayerController playerController)
    {
        PlayerHealth playerHealth = playerController.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.changeHealth(effectAmount);  // Increases health
            Debug.Log($"Golden Apple: Player health increased = {playerHealth.currentHealth}");
        }
    }
}
