using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class MainMenu : MonoBehaviour
{

    public void onPlay()
    {
        SceneManager.LoadScene("The_Game");
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
