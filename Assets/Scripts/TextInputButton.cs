using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextInputButton : MonoBehaviour
{
    public Button button;
    public TMP_InputField inputField;
    public TextMeshProUGUI buttonText;
    public bool isPassword = false;

    //private bool isEditing = true;

    void Start()
    {
        //button.onClick.AddListener(ToggleInput);
        inputField.onEndEdit.AddListener(OnInputEnd);
        
        // Initially hide the input field
        inputField.gameObject.SetActive(true);

        if (isPassword)
        {
            inputField.contentType = TMP_InputField.ContentType.Password;
        }
    }

     void OnInputEnd(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            if (isPassword)
            {
                buttonText.text = new string('*', value.Length);
            }
            else
            {
                buttonText.text = value;
            }
        }
        
        //  ToggleInput();
    }
}