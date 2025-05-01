using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SecretScroll : MonoBehaviour
{
    public GameObject scrollMessage;
    public TMP_Text scrollText;

    void Start()
    {
        // Ensure the message is hidden at the start
        scrollMessage.SetActive(false);
    }

    // This is the trigger function to detect player collision with the scroll
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Ensure the collision is with the player
        {
            // Set the text for the scroll message
            //scrollText.text = "You found the secret scroll!";
            // Display the message panel
            scrollMessage.SetActive(true);
        }
    }

    // Close the message when the player clicks the "X" button
    public void CloseMessage()
    {
        scrollMessage.SetActive(false);  // Hide the message
        Destroy(gameObject);
    }
}


