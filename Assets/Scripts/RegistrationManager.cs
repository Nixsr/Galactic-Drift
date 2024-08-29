using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class RegistrationManager : MonoBehaviour
{
    [SerializeField] private TextInputButton emailInputButton;
    [SerializeField] private TextInputButton passwordInputButton;
    [SerializeField] private TextInputButton confirmPasswordInputButton;
    public string loginMenuScene = "MainMenuLogin";
    [SerializeField] private TMP_Text messageText;

    private FirebaseAuth auth;

    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public async void OnRegisterButtonClick()
    {

        if (emailInputButton == null || passwordInputButton == null || confirmPasswordInputButton == null)
    {
        Debug.LogError("Email or Password or Confirm Password Input Button is not assigned!");
        ShowMessage("Login setup is incomplete. Please contact support.");
        return;
    }

    if (emailInputButton.inputField == null || passwordInputButton.inputField == null || confirmPasswordInputButton.inputField == null)
    {
        Debug.LogError("Input Field in Email or Password or Confirm Password Input Button is not assigned!");
        ShowMessage("Login setup is incomplete. Please contact support.");
        return;
    }


        string email = emailInputButton.inputField.text;
        string password = passwordInputButton.inputField.text;
        string confirmPassword = confirmPasswordInputButton.inputField.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            ShowMessage("Please fill in all fields.");
            return;
        }

        if (password != confirmPassword)
        {
            ShowMessage("Passwords do not match.");
            return;
        }

        try
        {
            await RegisterUser(email, password);
        }
        catch (System.Exception e)
        {
            ShowMessage($"Registration failed: {e.Message}");
        }
    }

    private async Task RegisterUser(string email, string password)
    {
        ShowMessage("Registering...");
        
        try
        {
            await auth.CreateUserWithEmailAndPasswordAsync(email, password);
            ShowMessage("Registration successful! You can now log in.");
            // TODO: You might want to automatically log the user in here, or redirect to the login screen
            //SceneManager.LoadScene(loginMenuScene);
        }
        catch (FirebaseException e)
        {
            throw new System.Exception($"Firebase error ({e.ErrorCode}): {e.Message}");
        }
    }

    private void ShowMessage(string message)
    {
        Debug.Log("Showing message: " + message);
    if (messageText != null)
    {
        messageText.text = message;
    }
    else
    {
        Debug.LogError("messageText is null!");
    }
    }
}