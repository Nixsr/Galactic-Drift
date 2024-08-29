using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenuGame";
    public string gameSceneName = "MainGameScene";
    public string pauseMenuSceneName = "PauseMenu";
    public string signUpMenuScene = "MainMenuSignUp";
    public string loginMenuScene = "MainMenuLogin";

    private bool isPaused = false;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log("MenuManager Start");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed");
            if (SceneManager.GetActiveScene().name == gameSceneName)
            {
                Debug.Log("Toggling pause menu");
                TogglePauseMenu();
            }
        }
    }

    public void StartGame()
    {
        Debug.Log("StartGame method called");
        //SceneManager.LoadScene(gameSceneName);
        SceneManager.LoadScene("LoadingScene");
        Time.timeScale = 1;
        SceneManager.LoadScene("Level1");
        isPaused = false;
    }

    public void ShowMainMenu()
    {
        Debug.Log("ShowMainMenu method called");
        //SceneManager.LoadScene(gameSceneName);
        SceneManager.LoadScene(mainMenuSceneName);
        Time.timeScale = 1;
        //isPaused = true;
    }

    public void ShowSignUpMenu()
    {
        Debug.Log("ShowMainMenu method called");
        //SceneManager.LoadScene(gameSceneName);
        SceneManager.LoadScene(signUpMenuScene);
        Time.timeScale = 1;
        //isPaused = true;
    }

    public void ShowLoginMenu()
    {
        Debug.Log("ShowMainMenu method called");
        //SceneManager.LoadScene(gameSceneName);
        SceneManager.LoadScene(loginMenuScene);
        Time.timeScale = 1;
        //isPaused = true;
    }

    public void ShowLoggedMenu()
    {
        Debug.Log("ShowMainMenu method called");
        //SceneManager.LoadScene(gameSceneName);
        SceneManager.LoadScene("MainMenuGameLogged");
        Time.timeScale = 1;
        //isPaused = true;
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        Debug.Log("Pause state: " + isPaused);
        if (isPaused)
        {
            Time.timeScale = 0;
            SceneManager.LoadScene(pauseMenuSceneName, LoadSceneMode.Additive);
        }
        else
        {
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync(pauseMenuSceneName);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public bool IsGamePaused()
    {
        return isPaused;
    }
}