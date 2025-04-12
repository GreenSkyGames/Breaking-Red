using UnityEngine;

public class RedShoes : PowerUpTemplate
{
    public override void applyEffect(PlayerController playerController)
    {
        if (playerController != null)
        {
            playerController.speed += effectAmount;
            Debug.Log($"Speed increased to {playerController.speed}");
        }
    }
}
