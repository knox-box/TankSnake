using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the camera
    public float parallaxFactor = 1f; // Parallax effect factor for the floor (1 = move fully with camera)

    private float initialX; // Initial X position of the floor
    private float initialY; // Initial Y position of the floor

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Automatically assign the main camera if not set
        }

        initialX = transform.position.x; // Store the initial X position of the floor
        initialY = transform.position.y; // Store the initial Y position of the floor
    }

    void Update()
    {
        // Calculate the floor's new position based on the camera's movement
        float offsetX = (cameraTransform.position.x - initialX) * parallaxFactor;

        // Update the floor's position while maintaining its initial offset
        transform.position = new Vector3(initialX + offsetX, initialY, transform.position.z);
    }
}
