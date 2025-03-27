/*Name: Alex Senst
 * Role: Team Lead 2+ -- Software Architect
 * 
 * This file contains the definition for the DamagingEnv class
 * This class causes damage to being in the game upon contact or close proximity
 * It inherets from TerrainObjects
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamagingEnv : TerrainObjects
{
    [SerializeField] private float _scaleSpeed;
    [SerializeField] private float _seedHeight;
    [SerializeField] private float _finalHeight;

    public int damage = 2;
    public float damagePerSecond = 10f;
	public float damageInterval = 0.75f;
	private HashSet<NPCManager> affectedObjects = new HashSet<NPCManager>();

    /* This code detects if either a player or another being in the game comes into contact with a damaging environment variable
     * Once a creature comes in contact with it, it detects whether or not that being was a player or not, reactively changing their health in different ways respectively
     * If it is a player, the health of that player is immediately changed and subtracted by the damage amount
     * If it is not a player, a coroutine is called to damage the NPC over the course of tiem they are in contact with the damaging environment object*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().changeHealth(-damage);
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

    //This is a simple death function meant for any time a player loses enough health that the game is over, exiting the application via Application.Quit()
    public void death()
    {
        Application.Quit();//quitting the game 
        UnityEditor.EditorApplication.isPlaying = false;
    }

    /* This code detects if either a player or another being in the game comes into contact with a damaging environment variable via a trigger as opposed to a collision
     * Once a creature comes in contact with it, it detects whether or not that being was a player or not, reactively changing their health in different ways respectively
     * If it is the player, then the coroutine damageOverTime is called for the player object
     * If it is not the player, then the coroutine applyDamageOverTime is called for that different object*/
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

    /* This code detects if either a player or another being in the game stops being in contact with a damaging environment variable
     * Once a creature loses contact with it, it checks whether or not that being was a player or not, removing the respective objects based on if its tag*/
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

    /* This code applies damage to an NPC via the changeHealth function
     * The function takes in an NPC and decreases their health at set intervals indefinitely whiel they are still considered in contact with the damaging object*/
    private IEnumerator applyDamageOverTime(NPCManager target)
    {
        while (affectedObjects.Contains(target)) // Apply damage as long as target is in the trigger
        {
            target.changeHealth(-damage);
            yield return new WaitForSeconds(damageInterval); // Wait for the next frame
        }
    }

    public int dmg = 9; //Damage over time for player
    public float damageInt = 1f; // Time between damage ticks
    private HashSet<GameObject> affectedObjs = new HashSet<GameObject>();

    /* This code applies damage to a player via the changeHealth function
      * The function takes in a player object and decreases their health at set intervals indefinitely whiel they are still considered in contact with the damaging object*/
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
            playerHealth.changeHealth(-dmg);

            yield return new WaitForSeconds(damageInt);
        }
    }
}
