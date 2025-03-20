using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

/*
	This is an attempt at creating two adjunct boundary tests.
	These do not use Test Runner in any way.

	The first test makes the NPC (TheWolf) move in the direction of the player, and
	collision terrain is placed in the way.
	After a certain time of not reaching the player, the test will succeed.

	The second test makes the NPC (TheWolf) reset and then move in the direction of
	the player until it encounters damaging terrain.  The NPC health decreases until
	it is at or below zero, and after a set amount of time, if the NPC is inactive (dead),
	the test succeeds.

	Both tests are in the same coroutine, as they cannot happen concurrently but have to
	happen one after the next.

	Further, this script is meant to be activated by a clickable button that appears on screen,
	which is the solution that was presented by the TL3s.

	This button can be found on Level 1 of the project, as well as the relevant gameObjects
	such as the two collidable walls.

	There is no stress test.  Apologies for my failure.	

*/


public class NPCTestScript : MonoBehaviour
{	
	private GameObject NPCTestWall;
	private GameObject NPCDeathWall;

	public void Awake()
	{
		NPCTestWall = GameObject.FindGameObjectWithTag("NPCTestWall");
		NPCDeathWall = GameObject.FindGameObjectWithTag("NPCDeathWall");

		NPCTestWall.gameObject.SetActive(false);
		NPCDeathWall.gameObject.SetActive(false);
	}

	//A simple boundary test to ensure collision is happening for the NPCs correctly.
    public IEnumerator NPCBoundaryTests(float moveDistance = 1000f)
    {


//--------------------FIRST TEST - NPC COLLISION TEST--------------------------------------//
        if (NPCTestWall == null)
        {
            Debug.LogError("NPCTestWall GameObject not found. Ensure it has the 'NPCTestWall' tag.");
            yield break;
        }
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player GameObject not found. Ensure it has the 'Player' tag.");
            yield break;
        }

		GameObject TestTarget = GameObject.FindGameObjectWithTag("TheWolf");
        if (TestTarget == null)
        {
            Debug.LogError("TestTarget GameObject not found. Ensure it has the 'TheWolf' tag.");
            yield break;
        }


		//Activate the test wall
		NPCTestWall.gameObject.SetActive(true);

		//Get original player location
		//Then move player to test point
		Vector2 playerOriginalPosition = player.transform.position;
        player.transform.position = new Vector2(-1, -3);
		Vector2 playerPosition = player.transform.position;
		Debug.Log(playerPosition);


		//Get original NPC location, then move NPC to test point.
		Vector2 NPCOriginalPosition = TestTarget.transform.position;
        TestTarget.transform.position = new Vector2(-6, -3);
		
		//Hostility is activated on the NPC to make it move to the player
		TestTarget.GetComponent<NPCManager>().onHostility();

		//15 seconds are given for the NPC to reach the player.
		yield return new WaitForSeconds(15);

		//The new position of the NPC is checked
		Vector2 NPCPosition = TestTarget.transform.position;
		Debug.Log(NPCPosition);	

		//Since the NPC is blocked, it should not reach the player after 15 seconds.
		if(NPCPosition != playerPosition)
		{
			Debug.Log("Collision test succeeded.");
		}

		
		//Deactivate the test wall and return player and NPC to original positions
		NPCTestWall.gameObject.SetActive(false);
        TestTarget.transform.position = NPCOriginalPosition;
        player.transform.position = playerOriginalPosition;

//------------------------START OF SECOND TEST - NPC DEATH TEST--------------------------------------------//

        if (NPCDeathWall == null)
        {
            Debug.LogError("NPCDeathWall GameObject not found. Ensure it has the 'NPCDeathWall' tag.");
            yield break;
        }

        if (player == null)
        {
            Debug.LogError("Player GameObject not found. Ensure it has the 'Player' tag.");
            yield break;
        }

        if (TestTarget == null)
        {
            Debug.LogError("TestTarget GameObject not found. Ensure it has the 'TheWolf' tag.");
            yield break;
        }


		//Activate the test wall
		NPCDeathWall.gameObject.SetActive(true);

		//Then move player to test point
        player.transform.position = new Vector2(-1, -3);
		Debug.Log(playerPosition);

		//Check if NPC is deactivated (already dead), activate if they are
		if(TestTarget.gameObject.activeSelf == false)
		{
			Debug.Log("NPC was dead!");
			TestTarget.gameObject.GetComponent<NPCManager>().currentHealth = 9;
			TestTarget.gameObject.SetActive(true);
		}

		//Get original NPC location, then move NPC to test point.
        TestTarget.transform.position = new Vector2(-6, -3);
		TestTarget.gameObject.GetComponent<NPCManager>().currentHealth = 9;
		
		//Hostility is activated on the NPC to make it move to the player
		TestTarget.GetComponent<NPCManager>().onHostility();

		//15 seconds are given for the NPC to reach the player and die on the terrain.
		yield return new WaitForSeconds(15);

		//If the NPC has been killed by the death wall, it will be deactive.
		if(TestTarget.gameObject.activeSelf == false)
		{
			Debug.Log("Death by environment test succeeded.");
		}
					
		//Deactivate the test wall and return player and NPC to original positions
		NPCDeathWall.gameObject.SetActive(true);
		TestTarget.gameObject.GetComponent<NPCManager>().currentHealth = 10;
		TestTarget.gameObject.SetActive(true);
        TestTarget.transform.position = NPCOriginalPosition;
        player.transform.position = playerOriginalPosition;

    }


    public void RunTests()
    {
        StartCoroutine(NPCBoundaryTests());
    }
}