using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Rigidbody2D ballRb = other.GetComponent<Rigidbody2D>();
            ballRb.velocity = Vector2.zero;
            ballRb.angularVelocity = 0;

            Invoke(nameof(ResetScene), 3f);
        }
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
