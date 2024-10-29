using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI playerOneScoreText;
    public TextMeshProUGUI playerTwoScoreText;
    public TextMeshProUGUI timerText;
    public GameObject endGamePanel;
    public GameObject goldenGoalText;
    public TextMeshProUGUI gameOverText;

    private int playerOneScore = 0;
    private int playerTwoScore = 0;
    private int winningScore = 5;
    private bool gameEnded = false;
    private bool goldenGoal = false;

    public float matchTime = 60f;

    void Start()
    {
        UpdateScoreUI();
        endGamePanel.SetActive(false);
        goldenGoalText.SetActive(false);
    }

    void Update()
    {
        if (!gameEnded)
        {
            HandleTimer();
        }
    }

    void HandleTimer()
    {
        if (matchTime > 0)
        {
            matchTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(matchTime / 60f);
            int seconds = Mathf.FloorToInt(matchTime % 60f);
            timerText.text = $"{minutes:0}:{seconds:00}";
        }
        else if (!goldenGoal)
        {
            if (playerOneScore == playerTwoScore)
            {
                StartGoldenGoal();
            }
            else
            {
                EndGame(playerOneScore > playerTwoScore ? "Player 1" : "Player 2");
            }
        }
    }

    public void UpdateScore(string goalName)
    {
        if (gameEnded) return;

        if (goalName == "playerOneGoal")
        {
            playerTwoScore++;
            playerTwoScoreText.text = $"Player Two: {playerTwoScore}";
            if (playerTwoScore >= winningScore) EndGame("Player 2");
        }
        else if (goalName == "playerTwoGoal")
        {
            playerOneScore++;
            playerOneScoreText.text = $"Player One: {playerOneScore}";
            if (playerOneScore >= winningScore) EndGame("Player 1");
        }

        if (goldenGoal)
        {
            EndGame(playerOneScore > playerTwoScore ? "Player 1" : "Player 2");
        }
    }

    private void StartGoldenGoal()
    {
        goldenGoal = true;
        timerText.text = "0:00";
        goldenGoalText.SetActive(true);
    }

    private void EndGame(string winner)
    {
        gameEnded = true;
        endGamePanel.SetActive(true);
        gameOverText.text = $"Game Over\n{winner} Wins!";
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        gameEnded = false;
        goldenGoal = false;
        Time.timeScale = 1f;

        playerOneScore = 0;
        playerTwoScore = 0;
        matchTime = 60f;

        UpdateScoreUI();
        endGamePanel.SetActive(false);
        goldenGoalText.SetActive(false);
    }

    private void UpdateScoreUI()
    {
        playerOneScoreText.text = $"Player One: {playerOneScore}";
        playerTwoScoreText.text = $"Player Two: {playerTwoScore}";
    }
}
