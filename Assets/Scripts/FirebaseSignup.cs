using Firebase.Auth;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

public class FirebaseSignup : MonoBehaviour
{
    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;
    public TMP_InputField confirmPasswordInputField; 
    public Button signupButton;
    public TMP_Text statusText;

    private FirebaseAuth auth;

    void Start()
    {
        // Initialize Firebase Auth
        auth = FirebaseAuth.DefaultInstance;
        signupButton.onClick.AddListener(() => SignupUser(emailInputField.text, passwordInputField.text, confirmPasswordInputField.text));
    }

    void SignupUser(string email, string password, string confirmPassword)
    {

        if (password != confirmPassword)
        {
            statusText.text = "Passwords do not match.";
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                statusText.text = "Signup canceled.";
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                statusText.text = "Signup error: " + task.Exception?.GetBaseException()?.Message;
                return;
            }

            // Access the Firebase user from the AuthResult
            AuthResult authResult = task.Result;
            FirebaseUser newUser = authResult.User;

            Debug.LogFormat("User signed up successfully: {0} ({1})", newUser.DisplayName, newUser.Email);
            statusText.text = "Signup successful!";

            SceneManager.LoadScene("MainMenuLogin");
        });
    }
}
