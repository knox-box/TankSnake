using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the camera
    public float parallaxFactor = 1f; // Parallax effect factor for the floor (1 = move fully with camera)

    private float initialY; // Initial Y position of the floor
    private float initialX; // Initial Y position of the floor


    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Automatically assign the main camera if not set
        }

        initialY = transform.position.y; // Store the initial Y position of the floor
        initialX = transform.position.x; // Store the initial Y position of the floor

    }

    void Update()
    {
        // Move the floor horizontally with the camera
        transform.position = new Vector3(cameraTransform.position.x + initialX * parallaxFactor, initialY, transform.position.z);
    }
}
