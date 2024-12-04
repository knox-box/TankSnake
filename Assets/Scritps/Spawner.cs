using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    public float obstacleSpawnTime = 2f;  // Time between spawns (lower is faster)
    private float timeUntilObstacleSpawn;
    public float obstacleSpeed = 16f;  // Speed at which obstacles move (higher is faster)

    // Define the range for random Y positions
    public float minY = -7.38f; // Adjust these values as needed
    public float maxY = -4f;

    GameManager gm;

    private int lastScoreAdjusted = 0;  // Keep track of the last score at which difficulty was adjusted

    private void Update()
    {
        if (GameManager.Instance.isPlaying)
        {
            AdjustDifficulty();
            SpawnLoop();
        }
    }

    private void SpawnLoop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;

        // Check if it's time to spawn an obstacle
        if (timeUntilObstacleSpawn >= obstacleSpawnTime)
        {
            Spawn();
            timeUntilObstacleSpawn = 0f;
        }
    }

    private void Spawn()
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        // Generate a random Y position within the defined range
        float randomYPosition = Random.Range(minY, maxY);

        // Set the spawn position with the random Y value
        Vector3 spawnPosition = new Vector3(transform.position.x, randomYPosition, transform.position.z);

        GameObject spawnObstacle = Instantiate(obstacleToSpawn, spawnPosition, Quaternion.identity);

        // Set the color of the obstacle to either green or red
        SpriteRenderer spriteRenderer = spawnObstacle.GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            int randomColorChoice = Random.Range(0, 2);
            spriteRenderer.color = randomColorChoice == 0 ? Color.green : Color.red;
        }

        Rigidbody2D obstacleRB = spawnObstacle.GetComponent<Rigidbody2D>();
        obstacleRB.velocity = Vector2.left * obstacleSpeed;

        // Destroy the spawned obstacle after a certain time (e.g., 5 seconds)
        Destroy(spawnObstacle, 5f);
    }

    // This function adjusts the difficulty as the score increases
    public void AdjustDifficulty()
    {
        int score = GameManager.Instance.currentScore;

        // Every 10 points, adjust the difficulty, but only if it's a new multiple of 10
        if (score >= (lastScoreAdjusted + 10)) 
        {
            lastScoreAdjusted = (score / 10) * 10;  // Update last score adjusted to the nearest multiple of 10
            Debug.Log("Difficulty increased at score: " + lastScoreAdjusted);

            // Speed up spawn rate (reduce spawn time)
            obstacleSpawnTime = Mathf.Max(0.5f, obstacleSpawnTime - 0.5f); // Minimum spawn time is 0.5 seconds
            // Increase obstacle speed
            obstacleSpeed += 2f; // Increase speed incrementally
        }
    }
}
