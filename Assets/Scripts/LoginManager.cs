using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private TextInputButton emailInputButton;
    [SerializeField] private TextInputButton passwordInputButton;
    [SerializeField] private TMP_Text messageText;

    private FirebaseAuth auth;

    private void Start()
    {
        // Initialize Firebase Auth
        auth = FirebaseAuth.DefaultInstance;
    }

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