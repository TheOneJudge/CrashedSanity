using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScripts : MonoBehaviour
{

    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    private void Start()
    {
        //Cursor.visible = false;
    }

    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        Cursor.visible = true;
    }

    public void OnMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnQuit()
    {
        Application.Quit();
    }

}
