using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    [SerializeField] public TextInputButton emailInputButton;
    [SerializeField] private TextInputButton passwordInputButton;
    [SerializeField] private TMP_Text messageText;

    private static LoginManager instance;

    private FirebaseAuth auth;

    //For the score manager

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // End of score manager

    private void Start()
    {
        // Initialize Firebase Auth
        auth = FirebaseAuth.DefaultInstance;
    }


    // For score manager
    public static LoginManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LoginManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(LoginManager).Name;
                    instance = obj.AddComponent<LoginManager>();
                }
            }
            return instance;
        }
    }

    private string username;

    public string Username { get { return username; } }

    public void ExtractUsername(string email)
    {
        username = email.Split('@')[0];
    }

    public string GetUsername()
    {
        if (string.IsNullOrEmpty(username))
        {
            if (auth.CurrentUser != null && !string.IsNullOrEmpty(auth.CurrentUser.Email))
            {
                ExtractUsername(auth.CurrentUser.Email);
            }
            else
            {
                Debug.LogWarning("User is not logged in or email is not available.");
                return null;
            }
        }
        return username;
    }

    // End of score manager

    public async void OnLoginButtonClick()
    {
    Debug.Log("Login button clicked");
    
    if (emailInputButton == null || passwordInputButton == null)
    {
        Debug.LogError("Email or Password Input Button is not assigned!");
        ShowMessage("Login setup is incomplete. Please contact support.");
        return;
    }

    if (emailInputButton.inputField == null || passwordInputButton.inputField == null)
    {
        Debug.LogError("Input Field in Email or Password Input Button is not assigned!");
        ShowMessage("Login setup is incomplete. Please contact support.");
        return;
    }

        string email = emailInputButton.inputField.text;
        string password = passwordInputButton.inputField.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ShowMessage("Please enter both email and password.");
            return;
        }

        try
        {
            await LoginUser(email, password);
        }
        catch (System.Exception e)
        {
            ShowMessage($"Login failed: {e.Message}");
        }
    }

    private async Task LoginUser(string email, string password)
    {
        ShowMessage("Logging in...");
        
        try
        {
            await auth.SignInWithEmailAndPasswordAsync(email, password);
            ShowMessage("Login successful!");
            
            SceneManager.LoadScene("MainMenuGameLogged");
        }
        catch (FirebaseException e)
        {
            throw new System.Exception($"{e.Message}");
        }
    }

    private void ShowMessage(string message)
    {
        messageText.text = message;
    }

}