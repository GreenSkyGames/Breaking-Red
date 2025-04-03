using UnityEngine;

public class EnchantedBerry : PowerUp
{
    public override void v_applyEffect(PlayerController playerController)
    {
        if (playerController != null)
        {
            //playerController.sanity += effectAmount;
            //Debug.Log($"Sanity increased to {playerController.sanity}");
        }
    }
}
