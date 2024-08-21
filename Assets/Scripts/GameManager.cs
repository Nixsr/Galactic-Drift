using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentLevel = 1;
    public int totalAsteroids;

    public int avoidedAsteroids { get; private set; }

    public bool isLevelComplete { get; private set; }

    public bool isGameCompleted { get; private set; }

    public int playerLives = 3;
    public bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager instance created");
        }
        else
        {
            Debug.Log("Destroying duplicate GameManager");
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        Debug.Log("StartGame called. Setting currentLevel to 1");
        currentLevel = 1;
        playerLives = 3;
        isGameOver = false;
        LoadCurrentLevel();
    }

    public void SetTotalAsteroids(int total)
    {
        totalAsteroids = total;
    }



    public void PlayerHit()
    {
        playerLives--;
        if (playerLives <= 0)
        {
            GameOver();
        }
        else
        {
            
            Debug.Log($"Player hit! Lives remaining.: {playerLives}");
        }
    }

    public void AsteroidAvoided()
    {
        avoidedAsteroids++;
        CheckLevelCompletion();
    }

    private void CheckLevelCompletion()
    {
        if (avoidedAsteroids >= totalAsteroids)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        Debug.Log("CompleteLevel called. Current level: " + currentLevel);
        
        isLevelComplete = true;
        
        if (currentLevel < 3)
        {
            currentLevel++;
        }
        else
        {
            WinGame();
        }
    }

    public void ContinueToNextLevel()
    {
            isLevelComplete = false;
            LoadCurrentLevel();
    }

    private void LoadCurrentLevel()
    {
        Debug.Log("LoadCurrentLevel called. Current level: " + currentLevel);
        avoidedAsteroids = 0;
        Debug.Log("Loading level: " + currentLevel);
        SceneManager.LoadScene("Level" + currentLevel);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over!");
    }

    private void WinGame()
    {
        Debug.Log("Congratulazioni! Hai completato tutti i livelli!");
        isGameCompleted = true;
    }
}