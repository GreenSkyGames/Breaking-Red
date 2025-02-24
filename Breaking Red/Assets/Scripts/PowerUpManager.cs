using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public abstract class PowerUpBase : MonoBehaviour
    {
        public abstract void ApplyEffect(GameObject player);
    }

    public class DecreaseHealth : PowerUpBase
    {
        public int healthDecrease = 10;
        public override void ApplyEffect(GameObject player)
        {
            if (gameObject.CompareTag("PoisonApple"))
            {
                PlayerController playerController = player.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    playerController.health -= healthDecrease;
                }
            }
            Destroy(gameObject);
        }
    }
    
    public class PowerUpCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            PowerUpBase powerup = GetComponent<PowerUpBase>();
            if (powerup != null)
            {
                powerup.ApplyEffect(other.gameObject);
            }
        }
    }
}
