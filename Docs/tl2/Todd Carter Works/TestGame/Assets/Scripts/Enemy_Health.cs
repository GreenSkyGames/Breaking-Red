using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
	public int currentHealth;
	public int maxHealth;

	//Start enemies at max health, just because.
	private void Start()
	{
		currentHealth = maxHealth;
	}

	//Amount should arrive as negative if damage is being dealt.
	public void ChangeHealth(int amount)
	{
		currentHealth += amount;

		//Check if overhealed
		if(currentHealth > maxHealth)
		{
			currentHealth = maxHealth;	
		}
		else if(currentHealth <= 0)
		{
			Destroy(gameObject);
		}
	}
}
