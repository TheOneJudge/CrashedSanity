using UnityEngine;

public class Navigation_Menu : MonoBehaviour
{
    public GameObject navigationPanel;  // Your Tab menu panel
    public UIScripts pauseMenuScript;   // Reference to the Pause Menu (UIScripts)

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

    // Opens the Tab menu and pauses the game
    void OpenTabMenu()
    {
        navigationPanel.SetActive(true);
        Time.timeScale = 0f;  // Pause the game
        isTabMenuOpen = true;
    }

    // Closes the Tab menu and resumes the game
    void CloseTabMenu()
    {
        navigationPanel.SetActive(false);
        Time.timeScale = 1f;  // Resume the game
        isTabMenuOpen = false;
    }
}
