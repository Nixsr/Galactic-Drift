using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class HighScoreManager : MonoBehaviour
{
    public Text highScoreText;
    public int maxScores = 100;

    private Dictionary<string, HighScoreEntry> highScores;

    [System.Serializable]
    public class HighScoreEntry
    {
        public string username;
        public int score;
    }

    void Awake()
    {
        highScores = new Dictionary<string, HighScoreEntry>();
        LoadHighScores();
    }

    void Start()
    {
        DisplayHighScores();
    }

    void LoadHighScores()
    {
        string json = PlayerPrefs.GetString("HighScores", "");
        Debug.Log($"Loaded JSON: {json}");
        if (!string.IsNullOrEmpty(json))
        {
            HighScoreList wrapper = JsonUtility.FromJson<HighScoreList>(json);
            if (wrapper != null && wrapper.highScores != null)
            {
                foreach (var entry in wrapper.highScores)
                {
                    if (!highScores.ContainsKey(entry.username) || 
                        highScores[entry.username].score < entry.score)
                    {
                        highScores[entry.username] = entry;
                    }
                }
            }
        }
        Debug.Log($"Loaded {highScores.Count} high scores");
    }

    public void AddHighScore(string username, int score)
    {
        Debug.Log($"Adding high score: {username} - {score}");
        if (!highScores.ContainsKey(username) || highScores[username].score < score)
        {
            highScores[username] = new HighScoreEntry { username = username, score = score };
            SaveHighScores();
            DisplayHighScores();
        }
    }

    void SaveHighScores()
    {
        HighScoreList wrapper = new HighScoreList { highScores = highScores.Values.ToList() };
        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString("HighScores", json);
        PlayerPrefs.Save();
        Debug.Log($"Saved high scores: {json}");
    }

    void DisplayHighScores()
    {
        if (highScoreText == null)
        {
            Debug.LogError("High Score Text component is not assigned!");
            return;
        }

        string displayText = "High Scores:\n\n";
        var sortedScores = highScores.Values
            .OrderByDescending(entry => entry.score)
            .Take(maxScores);

        int rank = 1;
        foreach (var entry in sortedScores)
        {
            displayText += $"{rank}. {entry.username}: {entry.score}\n";
            rank++;
        }
        highScoreText.text = displayText;
        Debug.Log($"Displayed high scores: {displayText}");
    }

    [System.Serializable]
    private class HighScoreList
    {
        public List<HighScoreEntry> highScores;
    }
}