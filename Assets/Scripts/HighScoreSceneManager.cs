using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

public class HighScoreSceneManager : MonoBehaviour
{
    public Text titleText;
    public Button backButton;
    public RectTransform scoreListParent;

    private List<PlayerScore> highScores;
    private float yOffset = -50f;  // Starting Y position for the first score
    private float scoreSpacing = 50f;  // Vertical space between scores

    private void Start()
    {
        if (backButton != null)
        {
            backButton.onClick.AddListener(GoBack);
        }
        else
        {
            Debug.LogError("Back button is not assigned in the HighScoreSceneManager.");
        }

        if (ScoreManager.Instance != null)
        {
            List<PlayerScore> allScores = ScoreManager.Instance.GetHighScores();
            Debug.Log($"Total scores retrieved: {allScores.Count}");
            highScores = GetAllScores(allScores);
            Debug.Log($"Scores after processing: {highScores.Count}");
            CreateScoreList();
        }
        else
        {
            Debug.LogError("ScoreManager instance is null. Make sure it's properly initialized.");
        }
    }

    private List<PlayerScore> GetAllScores(List<PlayerScore> allScores)
    {
        // Simply sort the scores without grouping or filtering
        return allScores
        .GroupBy(score => score.username)
        .Select(group => group
            .OrderByDescending(score => score.gameCompleted)
            .ThenByDescending(score => score.level)
            .ThenByDescending(score => score.avoidedAsteroids)
            .First())  // Prendi il miglior punteggio per ogni utente
        .OrderByDescending(score => score.gameCompleted)
        .ThenByDescending(score => score.level)
        .ThenByDescending(score => score.avoidedAsteroids)
        .ToList();
    }

    private void CreateScoreList()
    {
        if (scoreListParent == null)
        {
            Debug.LogError("Score list parent is not assigned in the HighScoreSceneManager.");
            return;
        }

        if (highScores == null)
        {
            Debug.LogError("High scores list is null.");
            return;
        }

        Debug.Log($"Creating score list with {highScores.Count} entries");

        for (int i = 0; i < highScores.Count; i++)
        {
            CreateScoreEntry(highScores[i], i);
        }
    }

    private void CreateScoreEntry(PlayerScore score, int index)
    {
        if (score == null)
        {
            Debug.LogError($"Score at index {index} is null.");
            return;
        }

        if (scoreListParent == null)
        {
            Debug.LogError("Score list parent is null in CreateScoreEntry.");
            return;
        }

        GameObject scoreEntryObj = new GameObject($"ScoreEntry_{index}", typeof(RectTransform));
        RectTransform rectTransform = scoreEntryObj.GetComponent<RectTransform>();
        
        if (rectTransform == null)
        {
            Debug.LogError($"Failed to get RectTransform component for ScoreEntry_{index}.");
            return;
        }

        rectTransform.SetParent(scoreListParent, false);

        // Set the position and size of the score entry
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.anchoredPosition = new Vector2(0, yOffset - (index * scoreSpacing));
        rectTransform.sizeDelta = new Vector2(0, 70);

        // Create and set up the Text component
        Text scoreText = scoreEntryObj.AddComponent<Text>();
        if (scoreText == null)
        {
            Debug.LogError($"Failed to add Text component to ScoreEntry_{index}.");
            return;
        }

        Font defaultFont = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        if (defaultFont != null)
        {
            scoreText.font = defaultFont;
        }
        else
        {
            Debug.LogWarning("Default font not found. Text may not display correctly.");
        }
        scoreText.fontSize = 40;  // Reduced font size to fit more information
        scoreText.alignment = TextAnchor.MiddleCenter;

        // Format the score text
        string levelText = score.gameCompleted ? "All Levels" : $"Level {score.level}";
        string asteroidText = $"Asteroids: {score.avoidedAsteroids}/{score.totalAsteroids}";
        scoreText.text = $"{index + 1}. {score.username} - {levelText} - {asteroidText}";

        // Alternate text colors
        scoreText.color = index % 2 == 0 ? Color.white : new Color(0.8f, 0.8f, 0.8f);

        Debug.Log($"Created score entry for {score.username} at index {index}");
    }

    private void GoBack()
    {
        SceneManager.LoadScene("MainMenuGameLogged");
    }
}