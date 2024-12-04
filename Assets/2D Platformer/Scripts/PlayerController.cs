using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        public float movingSpeed;
        public float jumpForce;
        private float moveInput;

        private bool facingRight = false;
        [HideInInspector]
        public bool deathState = false;

        private bool isGrounded;
        public Transform groundCheck;

        private new Rigidbody2D rigidbody;
        private Animator animator;
        private GameManager gameManager;

        private AudioSource audioSource; // Reference to AudioSource component
        public AudioClip jumpSound; // Reference to the jump sound

        private SpriteRenderer spriteRenderer; // Reference to SpriteRenderer for changing color

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
            spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component

            if (jumpSound != null && audioSource != null)
            {
                audioSource.clip = jumpSound;
            }
        }

        private void FixedUpdate()
        {
            CheckGround();
        }

        void Update()
        {
            // Handle movement
            if (Input.GetButton("Horizontal"))
            {
                moveInput = Input.GetAxis("Horizontal");
                rigidbody.velocity = new Vector2(moveInput * movingSpeed, rigidbody.velocity.y); // Set constant horizontal speed
                animator.SetInteger("playerState", 1); // Turn on run animation
            }
            else
            {
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y); // Stop horizontal movement when no input
                if (isGrounded) animator.SetInteger("playerState", 0); // Turn on idle animation
            }

            // Handle jump
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
            {
                rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                if (audioSource != null && jumpSound != null)
                {
                    audioSource.Play(); // Play the jump sound
                }
            }

            // Handle jump animation
            if (!isGrounded) animator.SetInteger("playerState", 2); // Turn on jump animation

            // Handle flipping
            if (facingRight == false && moveInput > 0)
            {
                Flip();
            }
            else if (facingRight == true && moveInput < 0)
            {
                Flip();
            }

            // Handle color change
            if (Input.GetKeyDown(KeyCode.Z))
            {
                ChangeColor(Color.green); // Change to green
            }

            else if (Input.GetKeyDown(KeyCode.C))
            {
                ChangeColor(Color.red); // Change to red
            }
        }


        private void Flip()
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }

        private void CheckGround()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.2f);
            isGrounded = colliders.Length > 1;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Obstacle")
            {
                deathState = true; // Say to GameManager that player is dead
            }
            else
            {
                deathState = false;
            }
        }

        private void ChangeColor(Color color)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = color; // Change the sprite color
            }
        }
    }
}
