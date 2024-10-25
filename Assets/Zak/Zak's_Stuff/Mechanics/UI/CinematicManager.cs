using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CinematicManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;       // The VideoPlayer component (e.g., assigned to "Intro_Clip")
    public GameObject pressEnterText;     // The Text or TextMeshPro for "Press Enter"
    public GameObject mainMenu;           // The Main Menu GameObject
    public RawImage videoRawImage;        // The RawImage component (e.g., "Video")
    public AudioSource mainMenuOST;       // The AudioSource for the main menu OST

    public double manualVideoLength = 8.0; // Manually set video length (seconds)
    private bool canProceed = false;

    void Start()
    {
        pressEnterText.SetActive(false);  // Initially hide the "Press Enter" text
        mainMenu.SetActive(false);        // Initially hide the Main Menu
        mainMenuOST.Stop();               // Ensure the OST is stopped at the start

        // Define how many seconds before the end you'd like to pause (e.g., 1 second)
        double secondsBeforeEnd = 5.5;    // Adjust this value as needed

        // Calculate when to pause: total length minus a few seconds
        double pauseTime = manualVideoLength - secondsBeforeEnd;

        // Start a delayed call to pause the video and show "Press Enter"
        Invoke(nameof(PauseVideoAndShowText), (float)pauseTime);
    }

    // Pause the video and show the "Press Enter" text
    void PauseVideoAndShowText()
    {
        videoPlayer.Pause();                      // Pause the video
        pressEnterText.SetActive(true);           // Show "Press Enter" text
        canProceed = true;                        // Allow proceeding to the next step
    }

    void Update()
    {
        // Check if Enter is pressed after the video has paused
        if (canProceed && Input.GetKeyDown(KeyCode.Return))
        {
            ProceedToMainMenu();
        }
    }

    void ProceedToMainMenu()
    {
        pressEnterText.SetActive(false);          // Hide the "Press Enter" text
        videoRawImage.enabled = false;            // Hide the RawImage showing the video
        videoPlayer.gameObject.SetActive(false);  // Disable the VideoPlayer GameObject
        mainMenu.SetActive(true);                 // Show the Main Menu
        mainMenuOST.Play();                       // Play the OST when the main menu is active
    }
}
