using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamagingEnv : MonoBehaviour
{
    public int damage = 2;
    [SerializeField] private float _scaleSpeed;
    [SerializeField] private float _seedHeight;
    [SerializeField] private float _finalHeight;

	public float damagePerSecond = 10f;
	public float damageInterval = 0.75f;
	private HashSet<NPCManager> affectedObjects = new HashSet<NPCManager>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
        }        

		if (collision.gameObject.tag != "Player")
        {
            //collision.gameObject.GetComponent<NPCManager>().ChangeHealth(-damage);
			NPCManager target = collision.gameObject.GetComponent<NPCManager>();
			if (target != null)
			{
				StartCoroutine(applyDamageOverTime(target));
			}
        }
    }


    public void death()
    {
        Debug.Log("Player died moving over an edge!");
        Application.Quit();//quitting the game 
        UnityEditor.EditorApplication.isPlaying = false;
    }

	//This is for damage over time.
	//Currently only affects NPCs (and anything not on the player layer).
	private void OnTriggerEnter2D(Collider2D collision)
    {
		NPCManager target = collision.gameObject.GetComponent<NPCManager>();
        if (target != null && !affectedObjects.Contains(target))
        {
            affectedObjects.Add(target);
            StartCoroutine(applyDamageOverTime(target));
        } 
        else if(collision.CompareTag("Player") && !affectedObjs.Contains(collision.gameObject)) 
        {
            affectedObjs.Add(collision.gameObject);
            StartCoroutine(damageOverTime(collision.gameObject));
        }
    }

	//This is for damage over time.
	//Currently only affects NPCs (and anything not on the player layer).
    private void OnTriggerExit2D(Collider2D collision)
    {
        NPCManager target = collision.GetComponent<NPCManager>();
        if (target != null && affectedObjects.Contains(target))
        {
            affectedObjects.Remove(target);
        }
        else if (collision.CompareTag("Player") && affectedObjs.Contains(collision.gameObject))
        {
            affectedObjs.Remove(collision.gameObject);
        }
    }

	//This is for damage over time.
	//Currently only affects NPCs (and anything not on the player layer).
    private IEnumerator applyDamageOverTime(NPCManager target)
    {
        while (affectedObjects.Contains(target)) // Apply damage as long as target is in the trigger
        {
            target.changeHealth(-damage);
            yield return new WaitForSeconds(damageInterval); // Wait for the next frame
        }
    }

    //Damage over time for player
    //public int playerDamage = 4; // Damage per tick
    public int dmg = 9;
    public float damageInt = 1f; // Time between damage ticks
    private HashSet<GameObject> affectedObjs = new HashSet<GameObject>();

    private IEnumerator damageOverTime(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        if (playerHealth == null)
        {
            Debug.LogError("PlayerManager component missing on player object!");
            yield break;
        }

        while (affectedObjs.Contains(player))
        {
            playerHealth.ChangeHealth(-dmg);

            yield return new WaitForSeconds(damageInt);
        }
    }
}
