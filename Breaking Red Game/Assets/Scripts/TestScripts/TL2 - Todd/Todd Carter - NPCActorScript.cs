using UnityEngine;

public class NPCStressTestActorScript : MonoBehaviour
{
	private NPCStressTestScript NPCStressWall;

	//Find the testing environmental objects and deactivate them.
	public void Awake()
	{
		NPCStressWall = FindAnyObjectByType<NPCStressTestScript>();
	}

	//When collision detected, break out of the for loop to stop spawning.
    private void OnTriggerExit2D(Collider2D other)
    {		
		//Check for the testing barrier
        /*if (NPCStressWall == null)
        {
            Debug.LogError("NPCDeathWall GameObject not found. Ensure it has the 'NPCDeathWall' tag.");
        }

        if (other.CompareTag("TheWolf")) // Make sure the boundary object has this tag
        {
            Debug.Log(gameObject.name + " has exited the boundary!");

			NPCStressWall.gameObject.GetComponent<NPCStressTestScript>().StopSpawning();
        }*/ //COMMENTED OUT FOR TESTING
    }
}