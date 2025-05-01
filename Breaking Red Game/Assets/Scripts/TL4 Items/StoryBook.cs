using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryBook : MonoBehaviour
{
    public GameObject bookMessage;
    public TMP_Text bookText;

    void Start()
    {
        // Ensure the message is hidden at the start
        bookMessage.SetActive(false);
    }

    // This is the trigger function to detect player collision with the scroll
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Ensure the collision is with the player
        {
            // Set the text for the scroll message
            //scrollText.text = "You found the secret scroll!";
            // Display the message panel
            bookMessage.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    // Close the message when the player clicks the "X" button
    public void CloseMessage()
    {
        bookMessage.SetActive(false);  // Hide the message
        Destroy(gameObject);
        Time.timeScale = 1f;
    }
}
