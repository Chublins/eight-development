using UnityEngine;

public class CollisionLogic : MonoBehaviour
{
    // Reference to the Rigidbody component of your object
    private Rigidbody2D rb;

    private void Start()
    {
        // Get the Rigidbody component attached to your object
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the tag "Platform"
        if (collision.gameObject.CompareTag("Platform"))
        {
            // Set velocity to 0 in the x direction
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }
}
