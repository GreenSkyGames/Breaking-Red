using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
	public int currentHealth;
	public int maxHealth;

	public TMP_Text healthText;
	public Animator healthTextAnim;

	//Start sets the initial text on the UI.
	private void Start()
	{
		healthText.text = "HP: " + currentHealth + " / " + maxHealth;
	}

	//UI display for player health.
	public void ChangeHealth(int amount)
	{
		currentHealth += amount;
		healthTextAnim.Play("TextUpdate");
		healthText.text = "HP: " + currentHealth + " / " + maxHealth;
		if(currentHealth <= 0)
		{
			gameObject.SetActive(false);
		}
	}
}
