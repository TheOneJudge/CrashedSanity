using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;
    public Camera[] surveillanceCameras;

    private Camera activeCamera;

    //private ComputerInterface compInt;

    void Start()
    {
        // Ensure only the main camera is active at the start
        SetActiveCamera(mainCamera);
    }

    void Update()
    {

      /*  if (compInt.AccessCams())
        {
            foreach (Camera cam in surveillanceCameras)
            {
                cam.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Camera cam in surveillanceCameras)
            {
                cam.gameObject.SetActive(false);
            }
        }*/
        
        
        /*// Check for camera switching keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchToSurveillanceCamera(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchToSurveillanceCamera(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            SwitchToSurveillanceCamera(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            SwitchToSurveillanceCamera(3);

        // Return to main camera
        if (Input.GetKeyDown(KeyCode.E))
            SetActiveCamera(mainCamera);*/
    }

    void SwitchToSurveillanceCamera(int index)
    {
        if (index >= 0 && index < surveillanceCameras.Length)
            SetActiveCamera(surveillanceCameras[index]);
    }

    void SetActiveCamera(Camera cameraToActivate)
    {
        // Deactivate all cameras
        foreach (Camera cam in surveillanceCameras)
        {
            cam.gameObject.SetActive(false);
        }
        mainCamera.gameObject.SetActive(false);

        // Activate the selected camera
        cameraToActivate.gameObject.SetActive(true);
        activeCamera = cameraToActivate;
    }

    public void ActivateSurveillanceCameras(bool active)
    {
        if (active)
        {
            foreach (Camera cam in surveillanceCameras)
            {
                cam.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Camera cam in surveillanceCameras)
            {
                cam.gameObject.SetActive(false);
            }
        }
    }
}