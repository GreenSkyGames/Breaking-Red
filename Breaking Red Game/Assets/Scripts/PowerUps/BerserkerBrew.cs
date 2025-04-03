using UnityEngine;

public class BerserkerBrew : PowerUp
{
    public override void v_applyEffect(PlayerController playerController)
    {
        if (playerController != null)
        {
            //playerController.attackPower += effectAmount;
            //Debug.Log($"Berserker Brew: Attack Power increased to {playerController.attackPower}");
        }
    }
}
