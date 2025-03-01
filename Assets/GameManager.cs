using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject tankExplosionPrefab; // Reference to your TankExplosion prefab
    public GameObject tank; // Reference to your tank GameObject

    public void EndGame()
    {
        // Instantiate the explosion prefab at the tank's position
        Instantiate(tankExplosionPrefab, tank.transform.position, Quaternion.identity);

        // Disable the tank (or destroy it)
        tank.SetActive(false);

        // Optional: Display game over screen or logic
        Debug.Log("Game Over!");
    }



}
