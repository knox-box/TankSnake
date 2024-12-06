using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TargetManager : MonoBehaviour
{
    public GameObject targetPrefab; // Reference to the target prefab
    public GameObject dashPowerUpPrefab; // Reference to the dash power-up prefab
    public float spawnDelayMin = 1; // Minimum spawn delay
    public float spawnDelayMax = 3; // Maximum spawn delay
    public float powerUpSpawnInterval = 10f; // Time interval for power-up spawns

    public Vector2 minSpawnPosition; // Minimum spawn position
    public Vector2 maxSpawnPosition; // Maximum spawn position

    public static int destroyedTargetCount = 0; // Counter for destroyed targets
    public float powerUpSpawnChance = 0.5f; // Chance to spawn a power-up (20%)

    public static void ResetDestroyedTargetCount()
    {
        destroyedTargetCount = 0;
    }
    private void Start()
    {
        SpawnTarget(); // Initial spawn
        StartCoroutine(SpawnPowerUpAtIntervals()); // Start power-up spawning coroutine

    }

    public void SpawnTarget()
    {
        // Generate a random position within the defined area
        Vector2 randomPosition = new Vector2(
            Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
            Random.Range(minSpawnPosition.y, maxSpawnPosition.y)
        );

        // Instantiate the target at the random position
        GameObject newTarget = Instantiate(targetPrefab, randomPosition, Quaternion.identity);

        // Set the TargetManager reference in the Target
        newTarget.GetComponent<Target>().SetTargetManager(this);
    }

    public void HandleTargetHit(GameObject target)
    {
        Destroy(target);
        destroyedTargetCount++; // Increment the counter
        Debug.Log("Targets destroyed: " + destroyedTargetCount); // Display the count
    // Check if a power-up should spawn
    if (Random.value <= powerUpSpawnChance)
    {
        Vector2 randomPosition = new Vector2(
            Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
            Random.Range(minSpawnPosition.y, maxSpawnPosition.y)
        );

        Instantiate(dashPowerUpPrefab, randomPosition, Quaternion.identity);
        Debug.Log("Dash power-up spawned at: " + randomPosition);
    }
        // Start the respawn coroutine
        StartCoroutine(RespawnTarget(target));
    }

    private IEnumerator RespawnTarget(GameObject target)
    {

        // Wait for a random time before respawning
        float waitTime = Random.Range(spawnDelayMin, spawnDelayMax);
        yield return new WaitForSeconds(waitTime);

        SpawnTarget(); // Spawn a new target elsewhere
    }

    public int GetDestroyedTargetCount()
    {
        return destroyedTargetCount;
    }
    private IEnumerator SpawnPowerUpAtIntervals()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerUpSpawnInterval); // Wait for the defined interval
            Vector2 randomPosition = new Vector2(
            Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
            Random.Range(minSpawnPosition.y, maxSpawnPosition.y));

            Instantiate(dashPowerUpPrefab, randomPosition, Quaternion.identity);
            Debug.Log("Dash power-up spawned at: " + randomPosition);
        }
    }
}