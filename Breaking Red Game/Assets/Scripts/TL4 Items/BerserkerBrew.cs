using UnityEngine;

public class BerserkerBrew : PowerUp
{
    public override void v_applyEffect(PlayerController playerController)
    {
        if (playerController != null)
        {
            playerController.attackDamage += effectAmount;
            Debug.Log($"Berserker Brew: Attack damage increased to {playerController.attackDamage}");
        }
    }
}
