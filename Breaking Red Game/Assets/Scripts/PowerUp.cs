using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Enum to define different types of power-ups
    public enum itemName { PoisonApple, GoldenApple, BerserkerBrew }
    public itemName type;  // Type of the power-up
    public float effectAmount;

    // apply effect to the player
    public void ApplyEffect(PlayerController playerController)
    {
        switch (type)
        {
            case itemName.PoisonApple:
                playerController.health -= effectAmount;  // decreases health
                Debug.Log("Player health decreased");
                break;

            case itemName.GoldenApple:
                playerController.health += effectAmount;  // increases health
                Debug.Log("Player health increased");
                break;
            
            case itemName.BerserkerBrew:
                //playerController.speed += effectAmount;
                Debug.Log("Player speed increased");
                break;
        }
    }

    // when player collides
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // play power up sound effect tbd
            //AudioManager.instance.Play("PowerUpSound");

            // trigger PowerUpManager to handle the interaction
            PowerUpManager powerUpManager = other.GetComponent<PowerUpManager>();
            if (powerUpManager != null)
            {
                powerUpManager.HandlePowerUpInteraction(this, other.GetComponent<PlayerController>());
            }

            // destroy the power-up object after interaction (it will either be used or stored)
            Destroy(gameObject);
        }
    }
}


