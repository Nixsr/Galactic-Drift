using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text livesText;
    public Text levelText;
    public Text avoidedAsteroidsText;  

    public GameObject pauseButton;

    public GameObject gameOverPanel;
    public GameObject winPanel;

    public GameObject levelCompletePanel;
    public Text levelCompleteText;
    public Button continueButton;

    public GameObject gameWonPanel;
    public Text gameWonText;

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        livesText.text = "Lives: " + GameManager.Instance.playerLives;
        levelText.text = "Level: " + GameManager.Instance.currentLevel;
        avoidedAsteroidsText.text = "Asteroids avoided: " + GameManager.Instance.avoidedAsteroids + "/" + GameManager.Instance.totalAsteroids;

        if (GameManager.Instance.isGameOver)
    {
        if (GameManager.Instance.currentLevel > 3)
        {
            if (winPanel != null){
                winPanel.SetActive(true);
                 HidePauseButton();
                 // for the score manager
                 AddScoreToHighScores(true);
                 // end of score manager
            }

            if (gameOverPanel != null)
                gameOverPanel.SetActive(false);
        }
        else
        {
            if (gameOverPanel != null){
                gameOverPanel.SetActive(true);
                HidePauseButton();
                // for the score manager
                AddScoreToHighScores(false);
                // end of score manager
            }
            if (winPanel != null)
                winPanel.SetActive(false);
        }
    }
    else
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
        if (winPanel != null)
            winPanel.SetActive(false);
    }

     if (GameManager.Instance.isLevelComplete)
        {
            levelCompletePanel.SetActive(true);
            levelCompleteText.text = $"Level {GameManager.Instance.currentLevel-1} completed!";
            continueButton.gameObject.SetActive(true);
            gameWonPanel.SetActive(false);
        }
        else if (GameManager.Instance.isGameCompleted)
        {
            levelCompletePanel.SetActive(false);
            continueButton.gameObject.SetActive(false);
            gameWonPanel.SetActive(true);
            gameWonText.text = "Congratulations! You have completed the game!";
            // for the score manager
            AddScoreToHighScores(true);
            // end of score manager
        }
        else
        {
            levelCompletePanel.SetActive(false);
            gameWonPanel.SetActive(false);
        }
    }

    // for the score manager
    public void AddScoreToHighScores(bool gameCompleted)
    {
        //string username = LoginManager.Instance.Username;
        string username = LoginManager.Instance.GetUsername();
        int level = GameManager.Instance.currentLevel;
        int avoidedAsteroids = GameManager.Instance.avoidedAsteroids;
        int totalAsteroids = GameManager.Instance.totalAsteroids;

        ScoreManager.Instance.AddScore(username, level, avoidedAsteroids, totalAsteroids, gameCompleted);
    }
    // end of score manager

    public void ContinueToNextLevel()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ContinueToNextLevel();
            levelCompletePanel.SetActive(false);
        }
    }

    public void RestartGame()
    {
        GameManager.Instance.StartGame();
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        
 
        Destroy(gameObject);
    }

    private void HidePauseButton()
    {
        pauseButton.SetActive(false);
    }

    private void ShowPauseButton()
    {
        pauseButton.SetActive(true);
    }

    // for the score manager
    public void ShowHighScores()
    {
        ScoreManager.Instance.ShowHighScoreScene();
    }
    // end of score manager

}