using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float maxSpeed = 100;
    public float rotationSpeed = 200;
    public float dashSpeed = 300; // Speed during the dash
    public float dashDuration = 0.2f; // How long the dash lasts
    private Vector2 movementVector;
    private bool isDashing = false; // Track whether a dash is active
    private float dashTimeRemaining = 0f; // Time remaining for the dash


    private void Awake()
    {
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    public void Move(Vector2 movementVector)
    {
        this.movementVector = movementVector;
    }
    public void Dash()
    {
        if (!isDashing) // Start a dash if not already dashing
        {
            isDashing = true;
            dashTimeRemaining = dashDuration;
            Debug.Log("Dash started!");
        }
    }
    private void FixedUpdate()
    {
        float currentSpeed = maxSpeed;

        // Handle dashing logic
        if (isDashing)
        {
            currentSpeed = dashSpeed;
            dashTimeRemaining -= Time.fixedDeltaTime;

            if (dashTimeRemaining <= 0)
            {
                isDashing = false;
                Debug.Log("Dash ended!");
            }
        }
        rb2d.velocity = (Vector2)transform.up * movementVector.y * currentSpeed * Time.deltaTime;

        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * rotationSpeed * Time.fixedDeltaTime));
    }
}
