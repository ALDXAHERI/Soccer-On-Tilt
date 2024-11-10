using UnityEngine;

public class Ball : MonoBehaviour
{
    // Rigidbody2D component for controlling the ball's physics
    private Rigidbody2D rb;

    // Force applied to the ball to make it bounce
    public float bounceForce = 5f;

    // GameManager for updating score and playing sounds
    public GameManager gameManager;

    // Starting position of the ball
    private Vector3 ballStartPos;

    // Player transforms for resetting their positions
    public Transform playerOne;
    public Transform playerTwo;
    private Vector3 playerOneStartPos;
    private Vector3 playerTwoStartPos;

    void Start()
    {
        // Rigidbody2D and starting positions
        rb = GetComponent<Rigidbody2D>();
        ballStartPos = transform.position;
        playerOneStartPos = playerOne.position;
        playerTwoStartPos = playerTwo.position;

        // Start the ball's bouncing motion
        StartBouncing();
    }

    // Sets the initial bounce velocity of the ball
    void StartBouncing()
    {
        // Only start bouncing if the game is not paused and GameManager is assigned
        if (Time.timeScale == 0f || gameManager == null) return;

        // Apply an upward velocity to the ball
        rb.velocity = Vector2.up * bounceForce;
    }

    // Detects when the ball enters a goal trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the ball collided with a GameObject tagged as "Goal"
        if (other.CompareTag("Goal"))
        {
            // Update the score in GameManager based on the goal it entered
            gameManager.UpdateScore(other.gameObject.name);

            // Stop the ball briefly before resetting its position
            Invoke(nameof(StopBall), 0.2f);

            // Reset the game after a short delay
            Invoke(nameof(ResetGame), 0.5f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Play hit sound if the ball collides with a player or the ground
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Floor"))
        {
            gameManager.PlayHitSound();
        }
    }

    // Stops the ball's movement by setting its Rigidbody2D to Static
    void StopBall()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

    // Resets the ball and players' positions after a goal
    void ResetGame()
    {
        // Prevents resetting the game if it is currently paused
        if (Time.timeScale == 0f) return;

        // Re-enable ball movement by setting Rigidbody2D back to Dynamic
        rb.bodyType = RigidbodyType2D.Dynamic;

        // Reset ball and players to their initial positions
        transform.position = ballStartPos;
        playerOne.position = playerOneStartPos;
        playerTwo.position = playerTwoStartPos;

        // Restart the ball's bounce
        StartBouncing();
    }
}
