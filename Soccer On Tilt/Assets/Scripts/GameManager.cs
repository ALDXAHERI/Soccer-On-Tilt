using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI playerOneScoreText;
    public TextMeshProUGUI playerTwoScoreText;
    public GameObject endGamePanel;
    public TextMeshProUGUI gameOverText;

    private int playerOneScore = 0;
    private int playerTwoScore = 0;
    private int winningScore = 5;
    private bool gameEnded = false;

    public bool GameEnded => gameEnded;

    void Start()
    {
        UpdateScoreUI();
        endGamePanel.SetActive(false);
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
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void UpdateScoreUI()
    {
        playerOneScoreText.text = $"Player One: {playerOneScore}";
        playerTwoScoreText.text = $"Player Two: {playerTwoScore}";
    }
}
