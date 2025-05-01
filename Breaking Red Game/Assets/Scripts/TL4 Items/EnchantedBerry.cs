using UnityEngine;

public class EnchantedBerry : PowerUpTemplate
{
    public override void applyEffect(PlayerController playerController)
    {
        if (playerController != null)
        {
            playerController.removeKill();
        }
    }
}
