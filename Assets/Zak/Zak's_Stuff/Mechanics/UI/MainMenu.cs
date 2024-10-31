using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTex;

    private void Start()
    {
        Cursor.SetCursor(cursorTex, Vector2.zero, CursorMode.Auto);
    }

    public void onPlay()
    {
        SceneManager.LoadScene("The_Game");
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
