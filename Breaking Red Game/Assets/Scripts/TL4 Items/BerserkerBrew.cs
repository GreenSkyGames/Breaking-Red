using UnityEngine;

public class BerserkerBrew : PowerUpTemplate
{
    public override void applyEffect(PlayerController playerController)
    {
        if (playerController != null)
        {
            playerController.attackDamage += effectAmount;
            Debug.Log($"Berserker Brew: Attack damage increased to {playerController.attackDamage}");
        }
    }
}
