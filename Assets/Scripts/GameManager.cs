using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentLevel = 1;
    public int totalAsteroids;

    //public int avoidedAsteroids;
    public int avoidedAsteroids { get; private set; }

    // Per l'avviso alla fine di ogni livello
    public bool isLevelComplete { get; private set; }

    // Per l'avviso alla fine del gioco
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
            // Opzionale: potresti voler aggiungere una logica per far lampeggiare il giocatore o dargli temporanea invincibilità
            Debug.Log($"Giocatore colpito! Vite rimanenti: {playerLives}");
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
        // Per l'avviso alla fine di ogni livello
        isLevelComplete = true;
        // Fine avviso
        if (currentLevel < 3)
        {
            currentLevel++;
            // Commentato per l'avviso alla fine di ogni livello
            //LoadCurrentLevel();
        }
        else
        {
            WinGame();
        }
    }

    // Metodo aggiunto per l'avviso alla fine di ogni livello
    public void ContinueToNextLevel()
    {
            isLevelComplete = false;
            LoadCurrentLevel();
    }
    // Fine metodo aggiunto

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
        // Qui puoi aggiungere logica per mostrare una schermata di Game Over
        // Ad esempio: SceneManager.LoadScene("GameOverScene");
    }

    private void WinGame()
    {
        Debug.Log("Congratulazioni! Hai completato tutti i livelli!");
        // Qui puoi aggiungere logica per mostrare una schermata di vittoria
        // Ad esempio: SceneManager.LoadScene("WinScene");
        //isGameOver = true;

        // Per l'avviso alla fine del gioco
        isGameCompleted = true;
    }
}