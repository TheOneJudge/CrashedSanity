using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public Camera[] cameras; // Array to store your cameras
    private int currentCameraIndex;

    void Start()
    {
        // Start by disabling all cameras except the main one
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == 0); // Only enable the first camera
        }
        currentCameraIndex = 0; // Default camera index
    }

    public void SwitchToCamera(int cameraIndex)
    {
        if (cameraIndex >= 0 && cameraIndex < cameras.Length)
        {
            Time.timeScale = 1;
            cameras[currentCameraIndex].gameObject.SetActive(false); // Disable current camera
            cameras[cameraIndex].gameObject.SetActive(true); // Enable the new camera
            currentCameraIndex = cameraIndex; // Update the current camera index
        }
    }
}
