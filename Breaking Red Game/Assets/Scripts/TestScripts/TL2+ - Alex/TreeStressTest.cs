using System.Collections.Generic;
using UnityEngine;

public class TreeStressTest : MonoBehaviour
{
    public GameObject treePrefab;  // The tree prefab to instantiate
    public float spawnInterval = 0f;  // Interval between each tree spawn
    public float spawnRadius = 5f;  // Radius in which trees will be spawned
    public GameObject player;  // Reference to the player
    public float fpsThreshold = 10f;  // FPS threshold to forcefully stop spawning (e.g., 5 FPS)

    private int currentTreeCount = 0;  // Track the current number of trees spawned
    private bool isSpawning = true;  // Flag to control spawning process
    private float lastTimeChecked = 0f;  // Time of last FPS check
    private int frameCountSinceLastCheck = 0;  // Frame count since last FPS check
    private float currentFPS = 100f; // Track the current frames per second
    private Vector3 initialPosition;
    private List<GameObject> spawnedTrees = new List<GameObject>();  // List to store references to spawned trees

    void Start()
    {
        if (treePrefab == null)
        {
            Debug.LogError("Tree Prefab is not assigned!");
            return;
        }

        initialPosition = player.transform.position;
    }

    public void RunStart()
    {
        // Start spawning trees
        StartCoroutine(SpawnTrees());
    }

    // Coroutine to spawn trees at intervals
    private System.Collections.IEnumerator SpawnTrees()
    {
        while (isSpawning)
        {
            // Check the FPS every 1 second
            if (Time.time - lastTimeChecked >= 1f)
            {
                currentFPS = frameCountSinceLastCheck / (Time.time - lastTimeChecked);

                // Log the FPS
                Debug.Log("Current FPS: " + currentFPS);

                // Severe check for extremely low FPS
                if (currentFPS < fpsThreshold)
                {
                    // Severe response: Immediate exit
                    Debug.LogError("FPS is critically low! Immediate stop.");
                    isSpawning = false;
                    player.transform.position = initialPosition;

                    foreach (GameObject tree in spawnedTrees)
                    {
                        Destroy(tree);
                    }

                    yield break;  // Forcefully exit the coroutine
                }

                // Reset the counters for the next check
                lastTimeChecked = Time.time;
                frameCountSinceLastCheck = 0;
            }

            // Overload with expensive operations
            for (int i = 0; i < 500; i++)  // Spawning 100 trees every frame (adjustable number)
            {
                // Simulate complex tasks per tree instantiation
                for (int j = 0; j < 1000; j++)  // Add an additional computational load
                {
                    float heavyComputation = Mathf.Sin(Time.time * j) * Mathf.Cos(Time.time * j);  // Expensive math
                }

                // Use the player's position as the spawn center
                Vector3 spawnCenter = new Vector3(0, -55, 0);
                player.transform.position = spawnCenter;

                // Spawn a tree at a random position within the defined radius around the player
                Vector3 spawnPosition = new Vector3(
                    spawnCenter.x + Random.Range(-spawnRadius, spawnRadius),
                    spawnCenter.y + Random.Range(-spawnRadius, spawnRadius),
                    0f // Z-axis is set to 0, assuming 2D
                );

                // Instantiate a tree at the calculated position
                GameObject spawnedTree = Instantiate(treePrefab, spawnPosition, Quaternion.identity);
                spawnedTrees.Add(spawnedTree);  // Add the spawned tree to the list
            }

            currentTreeCount += 100;  // Update tree count accordingly
            Debug.Log("Current Tree Count: " + currentTreeCount);  // Log tree count

            // Wait for the next spawn interval
            yield return new WaitForSeconds(spawnInterval);
            frameCountSinceLastCheck++;  // Increment frame count
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Increment frame count every frame
        frameCountSinceLastCheck++;
    }
}
