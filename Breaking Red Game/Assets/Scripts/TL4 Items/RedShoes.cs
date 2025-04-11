using UnityEngine;

public class RedShoes : PowerUp
{
    public override void v_applyEffect(PlayerController playerController)
    {
        if (playerController != null)
        {
            playerController.speed += effectAmount;
            Debug.Log($"Speed increased to {playerController.speed}");
        }
    }
}
