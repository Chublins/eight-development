using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this value to control the speed of movement
    public Transform wallCheck; // A transform at the side of the player to check for walls
    public LayerMask wallLayer; // The layer(s) representing walls

    private Rigidbody2D rb;
    private bool isTouchingWall; // Flag to track if the player is touching a wall
    private Vector3 lastValidPosition; // Store the last valid position

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastValidPosition = transform.position; // Initialize lastValidPosition
    }

    void Update()
    {
        // Check if the player is touching a wall
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, 0.1f, wallLayer);
    }

    void FixedUpdate()
    {
        // Get input for horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate movement direction based on input
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Apply movement to the rigidbody
        rb.velocity = movement;

        // If the player is touching a wall, revert to the last valid position
        if (isTouchingWall)
        {
            transform.position = lastValidPosition;
        }
        else
        {
            // Update the last valid position when the player is not touching a wall
            lastValidPosition = transform.position;
        }
    }
}
