using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 3;
    private Rigidbody2D rb2d;
    private bool hasHitTarget = false;

    private GameManager gameManager;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>(); // Find the GameManager in the scene
    }
    public void Initialize()
    {
        rb2d.velocity = transform.up * speed;
        hasHitTarget = false;
        gameObject.layer = LayerMask.NameToLayer("Default");
        Debug.Log("Bullet initialized. Has hit target: " + hasHitTarget);
    }

    public bool HasHitTarget() => hasHitTarget; // Expose hit status

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit: " + collision.name);
        if (collision.CompareTag("Obstacle"))
        {
            Bounce(collision);
        }
        else if (collision.CompareTag("Target"))
        {

            Target target = collision.GetComponent<Target>();
            if (target != null && !hasHitTarget)
            {
                // Let the target handle the hit
                target.HandleHit(this);
                hasHitTarget = true; // Now we can set hasHitTarget to true
                gameObject.layer = LayerMask.NameToLayer("UsedBullet");
            }
            Bounce(collision);
        }
        else if (collision.CompareTag("Player"))
        {
            Debug.Log("Game Over");
            //EndGame();
        }
    }
    private void Bounce(Collider2D collision)
    {
        // Get the contact point's normal vector
        Vector2 normal = collision.ClosestPoint(rb2d.position) - rb2d.position;
        normal.Normalize();

        // Reflect the bullet's velocity
        Vector2 incomingVelocity = rb2d.velocity;
        Vector2 reflectedVelocity = Vector2.Reflect(incomingVelocity, normal);

        // Set the new velocity while maintaining speed
        rb2d.velocity = reflectedVelocity.normalized * speed;

        // Optional: Adjust the bullet's rotation based on the bounce direction
        float angle = Mathf.Atan2(rb2d.velocity.y, rb2d.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle-90);
    }
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
 
