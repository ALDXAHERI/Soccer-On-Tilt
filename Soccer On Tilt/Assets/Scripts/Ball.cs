using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public float bounceForce = 5f;
    public GameManager gameManager;

    private Vector3 ballStartPos;
    public Transform playerOne;
    public Transform playerTwo;
    private Vector3 playerOneStartPos;
    private Vector3 playerTwoStartPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ballStartPos = transform.position;
        playerOneStartPos = playerOne.position;
        playerTwoStartPos = playerTwo.position;
        StartBouncing();
    }

    void StartBouncing()
    {
        if (Time.timeScale == 0f || gameManager == null) return;
        rb.velocity = Vector2.up * bounceForce;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            gameManager.UpdateScore(other.gameObject.name);
            Invoke(nameof(StopBall), 0.2f);
            Invoke(nameof(ResetGame), 0.5f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.PlayHitSound();
        }
    }

    void StopBall()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

    void ResetGame()
    {
        if (Time.timeScale == 0f) return;
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.position = ballStartPos;
        playerOne.position = playerOneStartPos;
        playerTwo.position = playerTwoStartPos;
        StartBouncing();
    }
}
