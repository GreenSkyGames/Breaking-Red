using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

public class NPCTestScript : MonoBehaviour
{	
	//A simple boundary test to ensure collision is happening for the NPCs correctly.
    public IEnumerator EnemyCollisionTest(float moveDistance = 1000f)
    {

        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player GameObject not found. Ensure it has the 'Player' tag.");
            yield break;
        }

		GameObject TheWolf = GameObject.FindGameObjectWithTag("TheWolf");
        if (TheWolf == null)
        {
            Debug.LogError("TheWolf GameObject not found. Ensure it has the 'TheWolf' tag.");
            yield break;
        }

		TheWolf.GetComponent<NPCManager>().onHostility();

		//The player position is taken at start.
		//This acts as the destination for the NPC.
		Vector2 playerPosition = player.transform.position;
		Debug.Log(playerPosition);

		//20 seconds are given for the NPC to reach the player.
		yield return new WaitForSeconds(20);

		//The NPC position is checked after duration is up.
		Vector2 NPCPosition = TheWolf.transform.position;
		Debug.Log(NPCPosition);

		//Since the NPC is blocked, it should not reach the player after 20 seconds.
		if(NPCPosition != playerPosition)
		{
			Debug.Log("Collision test completed.");
		}
			

    }


    public void RunTests()
    {
        StartCoroutine(EnemyCollisionTest());
        //StartCoroutine(AudioSourceLimitTest());
    }
}