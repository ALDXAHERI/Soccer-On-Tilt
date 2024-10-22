using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float bounceForce = 10f;
    private Rigidbody2D rb;

    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        StartBouncing();
    }

    void StartBouncing()
    {
        rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "outOfBoundPreventLeft" || 
            other.gameObject.name == "outOfBoundPreventRight")
        {
            ResetBallPosition();
        }
    }

    void ResetBallPosition()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        transform.position = startPosition;

        Invoke(nameof(StartBouncing), 0.5f);
    }
}
