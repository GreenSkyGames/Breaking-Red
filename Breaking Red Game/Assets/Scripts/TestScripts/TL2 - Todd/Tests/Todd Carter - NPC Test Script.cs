using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NPCSpawnerTests
{
    GameObject npcPrefab;
    Vector2 spawnPosition = Vector2.zero;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        //Create a basic NPC prefab placeholder for testing
        npcPrefab = new GameObject("TheWolf");
        //npcPrefab.AddComponent<NPCManager>(); // optional: your NPC script
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        // Destroy all remaining test objects
        foreach (var npc in GameObject.FindGameObjectsWithTag("TheWolf"))
        {
            Object.DestroyImmediate(npc);
        }

        Object.DestroyImmediate(npcPrefab);
        yield return null;
    }

	//Quick test to spawn multiple enemies in the same location.
    [UnityTest]
    public IEnumerator SpawnMultipleNPCsInSameSpot()
    {
        int numberToSpawn = 5;

        for (int i = 0; i < numberToSpawn; i++)
        {
            GameObject npc = Object.Instantiate(npcPrefab, spawnPosition, Quaternion.identity);
            npc.tag = "TheWolf"; // so we can clean up later
        }

        yield return null; // wait a frame for objects to be fully instantiated

        GameObject[] npcs = GameObject.FindGameObjectsWithTag("TheWolf");

        Assert.AreEqual(numberToSpawn, npcs.Length, "Did not spawn the expected number of NPCs.");

        foreach (var npc in npcs)
        {
            Assert.AreEqual(spawnPosition, npc.transform.position, "NPC was not spawned at the correct position.");
        }
    }
	
	//Boundary test that spawns an enemy, 
	//then has them move to the player 
	//until collision is detected.
	[UnityTest]
	public IEnumerator NPCMovesTowardPlayer_UntilCollisionOrCloseEnough()
	{
		GameObject npc = GameObject.Instantiate(npcPrefab, new Vector2(-5, 0), Quaternion.identity);
		GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule); //Temporary player placeholder
		player.transform.position = Vector2.zero;

		float speed = 2f;
		float closeEnoughDistance = 0.5f;
		float timeout = 5f;
		float elapsed = 0f;

		bool reachedPlayer = false;

		while (elapsed < timeout)
		{
			if (npc == null || player == null) break;

			Vector3 direction = (player.transform.position - npc.transform.position).normalized;
			npc.transform.position += direction * speed * Time.deltaTime;

			float distance = Vector2.Distance(npc.transform.position, player.transform.position);
			if (distance <= closeEnoughDistance)
			{
				reachedPlayer = true;
				break;
			}

			elapsed += Time.deltaTime;
			yield return null;
		}

		Assert.IsTrue(reachedPlayer, "NPC did not reach the player within time.");

		//Clean up
		GameObject.DestroyImmediate(npc);
		GameObject.DestroyImmediate(player);

		yield return null;
	}
	
	
	//A unit test that spawns an obstacle between player and enemy to block them.
	//This should always fail, by design.
	[UnityTest]
	public IEnumerator NPCBlockedByObstacle_DoesNotReachPlayer()
	{
		// Spawn the player and NPC
		GameObject npc = GameObject.Instantiate(npcPrefab, new Vector2(-5, 0), Quaternion.identity);
		GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		player.transform.position = new Vector2(5, 0);

		// Add colliders and rigidbodies for collision detection
		npc.AddComponent<Rigidbody>().isKinematic = true;
		npc.AddComponent<CapsuleCollider>();
		player.AddComponent<CapsuleCollider>();
		player.AddComponent<Rigidbody>().isKinematic = true;

		// Add obstacle directly between them
		GameObject obstacle = GameObject.CreatePrimitive(PrimitiveType.Cube);
		obstacle.transform.position = new Vector2(0, 0);
		obstacle.transform.localScale = new Vector2(1, 3);
		obstacle.AddComponent<BoxCollider>();
		obstacle.AddComponent<Rigidbody>().isKinematic = true;

		float speed = 2f;
		float closeEnoughDistance = 0.5f;
		float timeout = 5f;
		float elapsed = 0f;

		bool reachedPlayer = false;

		while (elapsed < timeout)
		{
			if (npc == null || player == null) break;

			Vector3 direction = (player.transform.position - npc.transform.position).normalized;
			Vector3 nextPosition = npc.transform.position + direction * speed * Time.deltaTime;

			// Do a raycast to simulate blocked movement (since we're not using full physics)
			if (!Physics.Linecast(npc.transform.position, nextPosition))
			{
				npc.transform.position = nextPosition;
			}

			float distance = Vector2.Distance(npc.transform.position, player.transform.position);
			if (distance <= closeEnoughDistance)
			{
				reachedPlayer = true;
				break;
			}

			elapsed += Time.deltaTime;
			yield return null;
		}

		Assert.IsFalse(reachedPlayer, "NPC unexpectedly reached the player despite obstacle in the way.");

		// Clean up
		GameObject.Destroy(npc);
		GameObject.Destroy(player);
		GameObject.Destroy(obstacle);

		yield return null;
	}


}
