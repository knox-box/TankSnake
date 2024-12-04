using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject explosionPrefab;
    private TargetManager targetManager; // Reference to the TargetManager
    // Method to set the TargetManager reference
    public void SetTargetManager(TargetManager manager)
    {
        targetManager = manager;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with: " + collision.name); // Log the collision
        if (collision.CompareTag("Bullet"))
        {
            Debug.Log("Bullet detected!");
            Bullet bullet = collision.GetComponent<Bullet>(); // Get the Bullet component
            if (bullet != null) // Check if bullet is not null
            {
                if (!bullet.HasHitTarget()) // Ensure bullet hasn't hit a target
                {
                    Debug.Log("Bullet hit the target!"); // Confirm bullet hit
                    HandleHit(bullet);
                }
                else
                {
                    Debug.Log("Bullet has already hit a target."); // Log if bullet has hit
                }
            }
        }
    }
    public void HandleHit(Bullet bullet)
    {
        Debug.Log("Handling hit for target: " + gameObject.name); // Confirm handling hit

        // Prevent double handling
        if (!gameObject.activeInHierarchy) return;

        // Notify the manager that the target was hit
        targetManager.HandleTargetHit(gameObject); // Notify the manager
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        gameObject.SetActive(false); // Deactivate the target

        Debug.Log("Target deactivated: " + gameObject.name + ", Active State: " + gameObject.activeInHierarchy);
    }
}
 