using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public Text livesText;
    public Text levelText;
    public Text avoidedAsteroidsText;  // Nuovo Text per asteroidi evitati

    // Per il bottone di pausa
    public GameObject pauseButton;


    public GameObject gameOverPanel;
    public GameObject winPanel;


    // Per l'avviso alla fine del livello
    public GameObject levelCompletePanel;
    public Text levelCompleteText;
    public Button continueButton;

    // Fine variabili per l'avviso alla fine del livello

    // Aggiunto per l'avviso alla fine del gioco
    public GameObject gameWonPanel;
    public Text gameWonText;
    // Fine variabili per l'avviso alla fine del gioco

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
            // Vittoria
            if (winPanel != null){
                winPanel.SetActive(true);
                 HidePauseButton();
            }

            if (gameOverPanel != null)
                gameOverPanel.SetActive(false);
        }
        else
        {
            // Game Over
            if (gameOverPanel != null){
                gameOverPanel.SetActive(true);
                HidePauseButton();
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
        }
        else
        {
            levelCompletePanel.SetActive(false);
            gameWonPanel.SetActive(false);
        }
    // Fine avviso alla fine del gioco
    }

    // Metodo aggiunto per l'avviso alla fine di ogni livello
    public void ContinueToNextLevel()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ContinueToNextLevel();
            levelCompletePanel.SetActive(false);
        }
    }
    // Fine metodo aggiunto per l'avviso alla fine di ogni livello

 //Original RestartGame Method
    public void RestartGame()
    {
        GameManager.Instance.StartGame();
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        
        // Aggiunto per resettare il Text degli asteroidi evitati
        Destroy(gameObject);
    }

    // Per il bottone di pausa
    private void HidePauseButton()
    {
        pauseButton.SetActive(false);
    }

    // Per il bottone di pausa
    private void ShowPauseButton()
    {
        pauseButton.SetActive(true);
    }


}