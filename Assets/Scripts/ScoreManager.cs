using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerScore
{
    public string username;
    public int level;
    public int avoidedAsteroids;
    public int totalAsteroids;
    public bool gameCompleted;

    public PlayerScore(string username, int level, int avoidedAsteroids, int totalAsteroids, bool gameCompleted)
    {
        this.username = username;
        this.level = level;
        this.avoidedAsteroids = avoidedAsteroids;
        this.totalAsteroids = totalAsteroids;
        this.gameCompleted = gameCompleted;
    }
}

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                
                instance = FindObjectOfType<ScoreManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(ScoreManager).Name;
                    instance = obj.AddComponent<ScoreManager>();
                    Debug.LogError("ScoreManager instance is null. Make sure it's in the scene.");
                }
            }
            return instance;
        }
    }

    private List<PlayerScore> highScores = new List<PlayerScore>();
    private const int MaxHighScores = 999999;
    private const string HighScoresKey = "HighScores";

    private void Awake()
    {
        Debug.Log("ScoreManager Awake called");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadHighScores();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(string username, int level, int avoidedAsteroids, int totalAsteroids, bool gameCompleted)
    {
        Debug.Log($"AddScore called: Username={username}, Level={level}, Asteroids={avoidedAsteroids}/{totalAsteroids}, Completed={gameCompleted}");
        PlayerScore newScore = new PlayerScore(username, level, avoidedAsteroids, totalAsteroids, gameCompleted);
        highScores.Add(newScore);
        highScores = highScores.OrderByDescending(s => s.level)
                               .ThenByDescending(s => (float)s.avoidedAsteroids / s.totalAsteroids)
                               .Take(MaxHighScores)
                               .ToList();
        SaveHighScores();
    }

    public List<PlayerScore> GetHighScores()
    {
        Debug.Log($"GetHighScores called. Number of scores: {highScores.Count}");
        return highScores;
    }

    private void SaveHighScores()
    {
        string json = JsonUtility.ToJson(new SerializableList<PlayerScore> { list = highScores });
        PlayerPrefs.SetString(HighScoresKey, json);
        PlayerPrefs.Save();
        Debug.Log("High scores saved to PlayerPrefs");
    }

    private void LoadHighScores()
    {
        if (PlayerPrefs.HasKey(HighScoresKey))
        {
            string json = PlayerPrefs.GetString(HighScoresKey);
            SerializableList<PlayerScore> loadedScores = JsonUtility.FromJson<SerializableList<PlayerScore>>(json);
            highScores = loadedScores.list;
            Debug.Log($"High scores loaded from PlayerPrefs. Number of scores: {highScores.Count}");
        }
        else
        {
            Debug.Log("No high scores found in PlayerPrefs");
        }
    }

    public void ShowHighScoreScene()
    {
        Debug.Log("ShowHighScoreScene called");
        SceneManager.LoadScene("HighScoreScene");
    }
}

[System.Serializable]
public class SerializableList<T>
{
  public List<T> list;
}