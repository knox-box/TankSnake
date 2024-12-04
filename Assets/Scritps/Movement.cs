using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private SpriteRenderer spriteRenderer; // To change the sprite color

    [Header("Move info")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private float moveSmoothness = 0.1f; // Smoothness of horizontal movement

    private void Start()
    {
        // Get the SpriteRenderer from the parent object (Player GameObject itself)
        spriteRenderer = GetComponent<SpriteRenderer>(); // Assuming the SpriteRenderer is on the parent
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // Handle jumping
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        // Reduce jump height when button is released
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        HandleColorChange(); // Handle the color change logic
        Flip();
    }

    private void FixedUpdate()
    {
        // Smooth horizontal movement using interpolation
        float targetSpeed = horizontal * speed;
        float smoothSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, moveSmoothness);
        rb.velocity = new Vector2(smoothSpeed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (horizontal < 0f && isFacingRight || horizontal > 0f && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void HandleColorChange()
    {
        if (Input.GetKeyDown(KeyCode.B)) // Change to green when B is pressed
        {
            spriteRenderer.color = Color.green;
        }
        else if (Input.GetKeyDown(KeyCode.N)) // Change to red when N is pressed
        {
            spriteRenderer.color = Color.red;
        }
    }

}
