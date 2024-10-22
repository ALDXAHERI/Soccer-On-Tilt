using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public float bounceForce = 5f;

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
        rb.velocity = Vector2.up * bounceForce;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            ResetGame();
        }
    }

    void ResetGame()
    {
        transform.position = ballStartPos;
        playerOne.position = playerOneStartPos;
        playerTwo.position = playerTwoStartPos;
        StartBouncing();
    }
}
