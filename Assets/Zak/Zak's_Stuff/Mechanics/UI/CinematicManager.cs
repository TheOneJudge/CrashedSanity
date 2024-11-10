using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CinematicManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;         // The VideoPlayer component
    public GameObject pressEnterText;       // The "Press Enter" text
    public GameObject mainMenu;             // The Main Menu GameObject
    public RawImage videoRawImage;          // The RawImage for video display
    public AudioSource mainMenuOST;         // AudioSource for the main menu OST
    public Animator cameraAnimator;         // Animator for the main camera's animation

    public double manualVideoLength = 5.0;  // Manually set video length (seconds)
    public double secondsBeforeEnd = 0.5;   // Adjust this to control when to pause
    private bool canProceed = false;

    void OnEnable()
    {
        canProceed = false;
        pressEnterText.SetActive(false);
        mainMenu.SetActive(false);
        videoRawImage.enabled = true;
        videoPlayer.gameObject.SetActive(true);
        mainMenuOST.Stop();

        // Ensure the camera animation is initially stopped
        if (cameraAnimator != null)
        {
            cameraAnimator.enabled = false;
        }

        videoPlayer.Stop();
        videoPlayer.Play();
    }

    void Update()
    {
        // Continuously check if the video is nearing the pause point
        if (!canProceed && videoPlayer.isPlaying && videoPlayer.time >= (manualVideoLength - secondsBeforeEnd))
        {
            PauseVideoAndShowText();
        }

        if (canProceed && Input.GetKeyDown(KeyCode.Return))
        {
            ProceedToMainMenu();
        }
    }

    void PauseVideoAndShowText()
    {
        videoPlayer.Pause();
        pressEnterText.SetActive(true);
        canProceed = true;
    }

    void ProceedToMainMenu()
    {
        pressEnterText.SetActive(false);
        videoRawImage.enabled = false;
        videoPlayer.gameObject.SetActive(false);
        mainMenu.SetActive(true);

        // Start the main camera's animation for the main menu
        if (cameraAnimator != null)
        {
            cameraAnimator.enabled = true;
            cameraAnimator.Play("LevelDisplayAnim", 0); // Replace with the actual animation name
        }

        mainMenuOST.Play();
    }
}
