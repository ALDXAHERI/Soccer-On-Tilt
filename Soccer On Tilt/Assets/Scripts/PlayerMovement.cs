using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables for movement speed, jump force, and ground detection
    public float moveSpeed = 5f;
    public float jumpForce = 9f;
    public bool isGrounded;

    // Variables for Rigidbody and AudioSource components
    private Rigidbody2D rb;
    private AudioSource moveSoundSource;
    public bool isPlayerOne;  // Boolean to check if this is player one

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSoundSource = GetComponent<AudioSource>();

        // Set the volume of the move sound to 65% of its original volume
        moveSoundSource.volume = 0.65f;
    }

    void Update()
    {
        // Check if this GameObject is controlled by player one or player two
        if (isPlayerOne)
        {
            HandlePlayerOneInput();
        }
        else
        {
            HandlePlayerTwoInput();
        }
    }

    // Handles input and movement for player one
    void HandlePlayerOneInput()
    {
        float move = 0;

        // Check for left and right movement inputs (A and D keys)
        if (Input.GetKey(KeyCode.A)) move = -1;
        if (Input.GetKey(KeyCode.D)) move = 1;

        // Apply horizontal movement to the Rigidbody2D
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // Play the move sound if the player is moving and the sound isn't already playing
        if (move != 0 && !moveSoundSource.isPlaying)
        {
            moveSoundSource.Play();
        }
        // Stop the move sound if the player stops moving
        else if (move == 0 && moveSoundSource.isPlaying)
        {
            moveSoundSource.Stop();
        }

        // Check for jump input (W key) and ensure player is grounded
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            // Apply upward force for jumping
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // Handles input and movement for player two
    void HandlePlayerTwoInput()
    {
        float move = 0;

        // Check for left and right movement inputs (arrow keys)
        if (Input.GetKey(KeyCode.LeftArrow)) move = -1;
        if (Input.GetKey(KeyCode.RightArrow)) move = 1;

        // Apply horizontal movement to the Rigidbody2D
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // Play the move sound if the player is moving and the sound isn't already playing
        if (move != 0 && !moveSoundSource.isPlaying)
        {
            moveSoundSource.Play();
        }
        // Stop the move sound if the player stops moving
        else if (move == 0 && moveSoundSource.isPlaying)
        {
            moveSoundSource.Stop();
        }

        // Check for jump input (Up Arrow key) and ensure player is grounded
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            // Apply upward force for jumping
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // Detects collision with the floor to set grounded status
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If colliding with an object tagged as "Floor", set isGrounded to true
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    // Detects when the player leaves the floor to unset grounded status
    private void OnCollisionExit2D(Collision2D collision)
    {
        // If leaving collision with an object tagged as "Floor", set isGrounded to false
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }
}
