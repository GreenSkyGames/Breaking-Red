using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

/*
	This is an attempt at creating a stress test for the project.

	It very simply activates a box beneath the testing position.

	Then it starts spawning NPCs at that location, one per frame.

	Once one of the NPCs is detected having left the boundary, the test ends.

	An on-screen button is required for usage.

	An on-screen display shows how many NPCs were spawned before one
	was pushed out of the boundary.

*/


public class NPCStressTestScript : MonoBehaviour
{	
	private GameObject NPCStressWall;
	
	public int spawnCount = 1000; //Maximum number of spawns.
	public GameObject NPCPrefab;

	public TMP_Text countText;
	private int totalSpawned;
	private bool stopSpawning = false;

	private List<GameObject> spawnedObjects = new List<GameObject>();


	//Find the testing environmental objects and deactivate them.
	public void Awake()
	{
		NPCStressWall = GameObject.FindGameObjectWithTag("NPCStressWall");

		NPCStressWall.gameObject.SetActive(false);
	}

	//A simple stress test to check that NPCs will fill a space until it breaks.
    public IEnumerator NPCStressTest(float moveDistance = 1000f)
    {

//------------------------START OF NPC STRESS TEST--------------------------------------------//


		//Find the testing NPC target (The Wolf from Level 1)
		GameObject TestTarget = GameObject.FindGameObjectWithTag("TheWolf");
        if (TestTarget == null)
        {
            Debug.LogError("TestTarget GameObject not found. Ensure it has the 'TheWolf' tag.");
            yield break;
        }

		//Activate the testing barrier
		NPCStressWall.gameObject.SetActive(true);

		//Check if NPC is deactivated (already dead), activate if they are
		if(TestTarget.gameObject.activeSelf == false)
		{
			Debug.Log("NPC was dead!");
			TestTarget.gameObject.GetComponent<NPCManager>().currentHealth = 9;
			TestTarget.gameObject.SetActive(true);
		}

		//Store the original position of the NPC and move to test location
		Vector2 NPCOriginalPosition = TestTarget.transform.position;
        TestTarget.transform.position = new Vector2(-6, -3);

		//Potentially infinite loop to spawn new NPCs.
		int i = 0;
		while(true)
		{
			if(stopSpawning == true)
			{
				Debug.Log("Spawning finished!  Total spawned: " + totalSpawned);

				//After the test is over, delete the extra NPCs.
				foreach (GameObject objDel in spawnedObjects)
				{
					if(objDel != null)
					{
						Destroy(objDel);
					}
				}

				TestTarget.transform.position = NPCOriginalPosition;

				break;
			}
			i++;

			GameObject obj = Instantiate(NPCPrefab, TestTarget.transform.position, Quaternion.identity);
			obj.name = "NPC_"+ i;

			spawnedObjects.Add(obj);

			totalSpawned++;
			countText.text = "Objects Spawned: " + totalSpawned;
			yield return null;
		}

    }

	//Method to end the spawning, as used by the test script attached to the NPCs
	public void StopSpawning()
	{
		stopSpawning = true;
	}


	//Runs the coroutine.
    public void RunNPCStressTest()
    {
        StartCoroutine(NPCStressTest());
    }
}