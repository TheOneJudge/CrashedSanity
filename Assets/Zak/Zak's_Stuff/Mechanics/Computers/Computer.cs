using UnityEngine.UI;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public float interactionRange = 2f;
    public KeyCode interactKey = KeyCode.E;

    public GameObject computerUIPanel;

    private Transform player;

    private bool isPlayerInRange = false;

    public ComputerDoor door;

    public Generator specificGenerator;

    public string correctPassword = "YourPassword"; // Set your password here
    public GameObject menuPanel; // Drag your menu panel here
    public GameObject passwordPanel; // Drag the password panel here
    public InputField passwordInputField;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        menuPanel.SetActive(false);
        computerUIPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        isPlayerInRange = Vector3.Distance(player.position, transform.position) <= interactionRange;

        if (isPlayerInRange && Input.GetKeyDown(interactKey))
        {
            if (specificGenerator.IsPowered())
            {
                OpenComputerUI();
            }
            else
            {
                Debug.Log("The generator is not on. Cannot access the computer.");
            }
        }
    }

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

    void UnlockMenu()
    {
        menuPanel.SetActive(true);  // Enable your menu panel
        passwordPanel.SetActive(false);  // Disable the password panel
        passwordInputField.interactable = false;  // Disable further input
    }

    private void OpenComputerUI()
    {
        computerUIPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Call AccessComputer() to reset the password panel
        AccessComputer();
    }

    public void OpenDoor()
    {
        if (door != null)
        {
            door.RequestOpen();
        }
    }

    public void CloseComputerUI()
    {
        computerUIPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void AccessComputer()
    {
        passwordInputField.text = "";  // Clear the input field
        passwordInputField.interactable = true;  // Re-enable the input field
        passwordPanel.SetActive(true);  // Show the password panel again
        menuPanel.SetActive(false);  // Keep the menu panel hidden until password is entered again
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
