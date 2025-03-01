using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public GameObject tankExplosionPrefab; // Reference to your tank explosion prefab
    public float sceneChangeDelay = 0.1f; // Delay in seconds before changing scenes

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider is a bullet
        if (other.CompareTag("Bullet"))
        {
            // Start the coroutine to handle the death process
            StartCoroutine(HandleDeath());
        }
    }

    private IEnumerator HandleDeath()
    {
        // Instantiate the explosion effect at the player's position
        Instantiate(tankExplosionPrefab, transform.position, Quaternion.identity);

        // Disable the tank object

        // Wait for the specified delay
        yield return new WaitForSeconds(sceneChangeDelay);

        // Call the method to change the scene
        LoadNewScene();
    }

    private void LoadNewScene()
    {
        // Load the DeathScene
        SceneManager.LoadScene("DeathScene");
    }
}