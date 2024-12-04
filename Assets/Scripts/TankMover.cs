using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float maxSpeed = 100;
    public float rotationSpeed = 200;
    public float boostMultiplier = 2f; // Multiplier for speed boost
    private Vector2 movementVector;
    private bool isBoosting = false; // Track whether boost is active


    private void Awake()
    {
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    public void Move(Vector2 movementVector)
    {
        this.movementVector = movementVector;
    }
    public void SetBoost(bool boostActive)
    {
        isBoosting = boostActive;
    }

    private void FixedUpdate()
    {
        float currentSpeed = isBoosting ? maxSpeed * boostMultiplier : maxSpeed;

        rb2d.velocity = (Vector2)transform.up * movementVector.y * currentSpeed * Time.deltaTime;

        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * rotationSpeed * Time.fixedDeltaTime));
    }
}
