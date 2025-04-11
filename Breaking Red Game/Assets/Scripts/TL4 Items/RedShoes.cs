using UnityEngine;

public class RedShoes : PowerUp
{
    public override void v_applyEffect(PlayerController playerController)
    {
        if (playerController != null)
        {
            playerController._speed += effectAmount;
            Debug.Log($"Speed increased to {playerController._speed}");
        }
    }
}
