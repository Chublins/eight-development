using UnityEngine;

public class movement : MonoBehaviour
{
    public Rigidbody2D Rigid;
    public float Jstrength;
    public float moveSpeed = 5f;
    private bool isGrounded = false;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Rigid = GetComponent<Rigidbody2D>(); // Ensure Rigidbody is initialized
        Rigid.freezeRotation = true; // Freeze rotation
    }

    // Update is called once per frame
    void Update()
    {
        //Left/Right
        

        // Jump Logic
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Rigid.velocity = new Vector2(Rigid.velocity.x, Jstrength);
            print("jump");
        }
    }

    // Collision detection
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        
        
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}