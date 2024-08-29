using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreSceneManager : MonoBehaviour
{
    public HighScoreManager highScoreManager;

    void Start()
    {
        if (highScoreManager == null)
        {
            Debug.LogError("HighScoreManager not assigned in HighScoreSceneManager!");
            highScoreManager = FindObjectOfType<HighScoreManager>();
            if (highScoreManager == null)
            {
                Debug.LogError("HighScoreManager not found in the scene!");
                return;
            }
        }

        int lastScore = PlayerPrefs.GetInt("LastScore", 0);
        string username = PlayerPrefs.GetString("Username", "Player");
        Debug.Log($"Retrieved last score: {lastScore} for user: {username}");

        highScoreManager.AddHighScore(username, lastScore);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuGame");
    }
}