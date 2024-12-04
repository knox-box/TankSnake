using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;  // Reference to the AudioSource component
    [SerializeField] private AudioClip colorMatchSound;  // Sound effect to play when colors match

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        
        // If the AudioSource is not assigned in the Inspector, try to find it on the current GameObject
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            // Disable physics reaction by setting Rigidbody2D to kinematic
            playerRigidbody.bodyType = RigidbodyType2D.Kinematic;

            // Get the SpriteRenderer of both the player and the obstacle
            SpriteRenderer playerRenderer = GetComponent<SpriteRenderer>();
            SpriteRenderer obstacleRenderer = other.gameObject.GetComponentInChildren<SpriteRenderer>();

            // Ensure the player and obstacle have valid SpriteRenderer components
            if (playerRenderer != null && obstacleRenderer != null)
            {
                // Compare colors of player and obstacle
                if (playerRenderer.color == obstacleRenderer.color)
                {
                    Debug.Log("Obstacle deleted: Colors match.");

                    // Increase the score in GameManager
                    GameManager.Instance.IncreaseScore();

                    // Play the color match sound effect
                    if (audioSource != null && colorMatchSound != null)
                    {
                        audioSource.PlayOneShot(colorMatchSound);
                    }

                    // Destroy the obstacle
                    Destroy(other.gameObject);

                    return; // Exit to avoid further processing
                }
            }

            // If colors don't match, handle regular collision (e.g., destroy the player)
            Destroy(gameObject); // Destroy the player
            GameManager.Instance.GameOver();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        // Re-enable physics reaction after exiting collision
        playerRigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
}
