using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int chargesGranted = 1; // Number of charges this power-up grants

 private void OnTriggerEnter2D(Collider2D other)
{
    // Check if the collided object is the Player
    if (other.CompareTag("Player"))
    {
        Debug.Log("Power-up collided with the Player!");

        // Find the TankMover component in the Player's children
        TankMover tankMover = other.GetComponentInChildren<TankMover>();

        if (tankMover != null)
        {
            Debug.Log("TankMover component found. Granting charges...");

            // Recharge the player's dash charges
            for (int i = 0; i < chargesGranted; i++)
            {
                tankMover.RechargeDash();
            }

            Debug.Log($"Granted {chargesGranted} charges to the player.");

            // Destroy the power-up after granting charges
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("TankMover component not found in the Player's children.");
        }
    }
    else
    {
        Debug.Log("Collision detected with a non-player object: " + other.name);
    }
}
}