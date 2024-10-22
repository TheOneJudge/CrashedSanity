using UnityEngine;

public class Navigation_Menu : MonoBehaviour
{
    public GameObject navigationPanel;  // Your Tab menu panel
    public UIScripts pauseMenuScript;   // Reference to the Pause Menu (UIScripts)
    public GameObject optionsBar;

    public bool isTabMenuOpen = false;

    void Update()
    {
        // Detect Tab key press
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isTabMenuOpen)
            {
                CloseTabMenu();
            }
            else if (!UIScripts.gameIsPaused)  // Prevent opening Tab menu if game is paused
            {
                OpenTabMenu();
            }
        }

        // Allow closing Tab menu with Escape
        if (Input.GetKeyDown(KeyCode.Escape) && isTabMenuOpen)
        {
            CloseTabMenu();
        }
    }

    public void Resume()
    {
        navigationPanel.SetActive(false);
        Time.timeScale = 1f;
        isTabMenuOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    // Opens the Tab menu and pauses the game, enables the mouse cursor
    void OpenTabMenu()
    {
        navigationPanel.SetActive(true);
        optionsBar.SetActive(true);  // Activate the options bar
        Time.timeScale = 0f;  // Pause the game
        isTabMenuOpen = true;

        // Enable and unlock the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Closes the Tab menu and resumes the game, hides the mouse cursor
    void CloseTabMenu()
    {
        navigationPanel.SetActive(false);
        optionsBar.SetActive(false);  // Deactivate the options bar
        Time.timeScale = 1f;  // Resume the game
        isTabMenuOpen = false;

        // Lock and hide the cursor again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
