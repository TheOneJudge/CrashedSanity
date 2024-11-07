using UnityEngine;
using UnityEngine.Events;

public class ComputerInterface : MonoBehaviour
{
    public UnityEvent myEvent;
    
    [SerializeField] private GameObject canvas, menu;
    [SerializeField] private GameObject cam;

    private bool triggered = false;
    private bool ePress = false;


    private void Start()
    {
        canvas.SetActive(false);
        menu.SetActive(false);
    }


    private void Update()
    {
        canvas.transform.localRotation = cam.transform.localRotation;

        Debug.Log(cam.transform.localRotation);

        if (triggered && Input.GetKeyUp(KeyCode.E))
        {
            ePress = true;
            ShowMenu(ePress);
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            triggered = true;
            ShowE(triggered);
            Debug.Log("player enter");

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.tag == "Player")
        {
            triggered = false;
            ShowE(triggered);
            ShowMenu(triggered);
            Debug.Log("player exit");
        }
    }
    private void ShowE(bool triggered)
    {
        if (triggered)
        {
            canvas.SetActive(triggered);
        }
        else
        {
            canvas.SetActive(triggered);
        }

        //ePress = triggered;
    }

    private void ShowMenu(bool ePress)
    {

        if (ePress)
        {
            ShowE(false);
            menu.SetActive(ePress);

            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            menu.SetActive(false);
            Cursor.lockState= CursorLockMode.Locked;
        }

        Cursor.visible = ePress;
    }
}
