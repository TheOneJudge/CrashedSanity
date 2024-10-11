using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class MainMenu : MonoBehaviour
{

    public void onPlay()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
