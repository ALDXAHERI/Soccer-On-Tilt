using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // UI Elements for displaying player scores, timer, and game-over messages
    public TextMeshProUGUI playerOneScoreText;
    public TextMeshProUGUI playerTwoScoreText;
    public TextMeshProUGUI timerText;
    public GameObject endGamePanel;
    public GameObject goldenGoalText;
    public TextMeshProUGUI gameOverText;

    // Audio sources for hit and goal sounds
    public AudioSource hitSoundSource;
    public AudioSource goalSoundSource;

    // Variables for keeping track of scores and game state
    private int playerOneScore = 0;
    private int playerTwoScore = 0;
    private int winningScore = 5;  // The score required to win the game
    private bool gameEnded = false;  // Indicates if the game has ended
    private bool goldenGoal = false;  // Indicates if the game is in golden goal mode

    // Duration of the match in seconds
    public float matchTime = 60f;

    void Start()
    {
        // Initialize UI and hide end game panel and golden goal text at the start
        UpdateScoreUI();
        endGamePanel.SetActive(false);
        goldenGoalText.SetActive(false);
    }

    void Update()
    {
        // Continuously update the timer if the game has not ended
        if (!gameEnded)
        {
            HandleTimer();
        }
    }

    // Handles the countdown timer
    void HandleTimer()
    {
        if (matchTime > 0)
        {
            // Decrease the match time and update the timer display
            matchTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(matchTime / 60f);
            int seconds = Mathf.FloorToInt(matchTime % 60f);
            timerText.text = $"{minutes:0}:{seconds:00}";
        }
        else if (!goldenGoal)
        {
            // If the timer reaches zero, check if scores are tied
            if (playerOneScore == playerTwoScore)
            {
                StartGoldenGoal();  // Enter golden goal mode if scores are tied
            }
            else
            {
                // End the game with the player who has a higher score as the winner
                if (playerOneScore > playerTwoScore)
                {
                    EndGame("Player 1");
                }
                else
                {
                    EndGame("Player 2");
                }
            }
        }
    }

    // Updates the score based on which player scored a goal
    public void UpdateScore(string goalName)
    {
        if (gameEnded) return;  // Prevent updating the score if the game has ended

        if (goalName == "playerOneGoal")
        {
            // Increment Player Two's score and update the UI
            playerTwoScore++;
            playerTwoScoreText.text = $"Player Two: {playerTwoScore}";
            StartCoroutine(PlayPartialGoalSound());  // Play partial goal sound

            // End the game if Player Two reaches the winning score
            if (playerTwoScore >= winningScore) EndGame("Player 2");
        }
        else if (goalName == "playerTwoGoal")
        {
            // Increment Player One's score and update the UI
            playerOneScore++;
            playerOneScoreText.text = $"Player One: {playerOneScore}";
            StartCoroutine(PlayPartialGoalSound());  // Play partial goal sound

            // End the game if Player One reaches the winning score
            if (playerOneScore >= winningScore) EndGame("Player 1");
        }

        // If in golden goal mode, end the game immediately with the current lead
        if (goldenGoal)
        {
            EndGame(playerOneScore > playerTwoScore ? "Player 1" : "Player 2");
        }
    }

    // Starts the golden goal mode if scores are tied at the end of the match time
    private void StartGoldenGoal()
    {
        goldenGoal = true;
        timerText.text = "0:00";  // Display 0:00 in the timer
        goldenGoalText.SetActive(true);  // Show the golden goal message
    }

    // Ends the game and displays the end game panel with the winner's name
    private void EndGame(string winner)
    {
        gameEnded = true;
        endGamePanel.SetActive(true);  // Show the end game panel
        gameOverText.text = $"Game Over\n{winner} Wins!";  // Display winner's message
        Time.timeScale = 0f;  // Pause the game

        // Stop all sounds
        if (hitSoundSource.isPlaying) hitSoundSource.Stop();
        if (goalSoundSource.isPlaying) goalSoundSource.Stop();
    }

    // Resets the game state and UI when restarting the game
    public void RestartGame()
    {
        gameEnded = false;
        goldenGoal = false;
        Time.timeScale = 1f;  // Resume game time

        playerOneScore = 0;
        playerTwoScore = 0;
        matchTime = 60f;  // Reset match time

        UpdateScoreUI();
        endGamePanel.SetActive(false);  // Hide end game panel
        goldenGoalText.SetActive(false);  // Hide golden goal text
    }

    // Updates the score display in the UI for both players
    private void UpdateScoreUI()
    {
        playerOneScoreText.text = $"Player One: {playerOneScore}";
        playerTwoScoreText.text = $"Player Two: {playerTwoScore}";
    }

    // Play only the first second of the goal sound
    private IEnumerator PlayPartialGoalSound()
    {
        goalSoundSource.Play();  // Play the goal sound
        yield return new WaitForSeconds(1f);  // Wait for 1 second
        goalSoundSource.Stop();  // Stop the sound
    }

    // Plays the hit sound when a player hits the ball
    public void PlayHitSound()
    {
        if (hitSoundSource != null) 
        {
            hitSoundSource.Play();
        }
    }
}
