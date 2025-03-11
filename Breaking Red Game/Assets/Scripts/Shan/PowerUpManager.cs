using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    // This method applies the effect of a power-up to the player
    public void ApplyPowerUpEffect(PowerUp powerUp, PlayerController playerController)
    {
        // Call the ApplyEffect method on the power-up itself
        powerUp.ApplyEffect(playerController);
    }
}

