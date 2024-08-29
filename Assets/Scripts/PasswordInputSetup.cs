using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasswordInputSetup : MonoBehaviour
{
    public TMP_InputField passwordInputField;

    void Start()
    {
        // Ensure this is a password input field
         passwordInputField.contentType = TMP_InputField.ContentType.Password;
        passwordInputField.asteriskChar = '*'; // Set the masking character
    }
}
