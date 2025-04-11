using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NPCSpawnerTests : MonoBehaviour
{
    GameObject npcPrefab;
    GameObject playerPrefab;
    Vector2 spawnPosition = Vector2.zero;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        //Create a basic NPC prefab placeholder for testing
        //GameObject npc = new GameObject("NPC");
		
		npcPrefab = Resources.Load<GameObject>("NPCPrefabs/TheWolf");
		playerPrefab = Resources.Load<GameObject>("NPCPrefabs/Player");
		
        //playerPrefab = GameObject.FindWithTag("Player");
        //npcPrefab.AddComponent<NPCManager>(); //NPCManager already part of prefab
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
            Assert.AreNotEqual(spawnPosition, npc.transform.position, "NPC was not spawned at the correct position.");
        }
    }
	
	//Boundary test that spawns an enemy, 
	//then has them move to the player 
	//until collision is detected.
	[UnityTest]
	public IEnumerator NPCMovesTowardPlayer_UntilCollisionOrCloseEnough()
	{
		//Create NPC object
		GameObject npc = GameObject.Instantiate(npcPrefab, new Vector2(-5, 0), Quaternion.identity);
		
        //Create Player GameObject
		GameObject player = GameObject.Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity);
        player.transform.position = new Vector2(5f, 0f);

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
    [UnityTest]
    public IEnumerator NPCBlockedByObstacle2D_DoesNotReachPlayer()
    {
        //Create NPC GameObject
		GameObject npc = GameObject.Instantiate(npcPrefab, new Vector2(-5, 0), Quaternion.identity);
		
        //Create Player GameObject
		GameObject player = GameObject.Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity);		
        player.transform.position = new Vector2(5f, 0f);

        //Create an Obstacle between them
        GameObject obstacle = new GameObject("Obstacle");
        obstacle.transform.position = new Vector2(0f, 0f);
        BoxCollider2D obstacleCol = obstacle.AddComponent<BoxCollider2D>();
        obstacleCol.size = new Vector2(2f, 2f);
        Rigidbody2D obstacleRb = obstacle.AddComponent<Rigidbody2D>();
        obstacleRb.bodyType = RigidbodyType2D.Static;

        float speed = 3f;
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

            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate(); //Sync with 2D physics
        }

        Assert.IsFalse(reachedPlayer, "NPC unexpectedly reached the player despite a blocking obstacle.");

        //Clean up
        GameObject.DestroyImmediate(npc);
        GameObject.DestroyImmediate(player);
        GameObject.DestroyImmediate(obstacle);
    }
}
