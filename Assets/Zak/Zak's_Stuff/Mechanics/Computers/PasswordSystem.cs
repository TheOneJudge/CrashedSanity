using UnityEngine;
using UnityEngine.UI;

public class PasswordSystem : MonoBehaviour
{
    public string correctPassword = "YourPassword"; // Set your password here
    public GameObject menuPanel; // Drag your menu panel here
    public GameObject passwordPanel; // Drag the password panel here
    public InputField passwordInputField;

    void Start()
    {
        // Keep menu locked initially
        menuPanel.SetActive(false);
    }

    // This function will be called when the user presses the "Submit" button
    public void CheckPassword()
    {
        if (passwordInputField.text == correctPassword)
        {
            UnlockMenu();
        }
        else
        {
            Debug.Log("Incorrect Password");
            // Optionally display an error message
        }
    }

    // Unlock the menu and hide the password panel
    void UnlockMenu()
    {
        menuPanel.SetActive(true);  // Enable your menu panel
        passwordPanel.SetActive(false);  // Disable the password panel
        passwordInputField.interactable = false;  // Optionally disable further input
    }
}
