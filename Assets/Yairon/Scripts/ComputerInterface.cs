using UnityEngine;
using UnityEngine.Events;

public class ComputerInterface : MonoBehaviour
{
    public UnityEvent myEvent;

    [SerializeField] private GameObject pressEPanel, menu, offlinePanel;
    [SerializeField] private GameObject cam;

    [SerializeField] private Generator gen;

    private bool triggered = false;
    private bool ePress = false;


    private void Start()
    {
        pressEPanel.SetActive(false);
        menu.SetActive(false);
    }


    private void Update()
    {
        pressEPanel.transform.localRotation = cam.transform.localRotation;

        //Debug.Log(cam.transform.localRotation);

        if (triggered && gen.IsPowered())
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                ePress = true;
                ShowMenu(ePress);
            }       
        }
        else if(triggered && !gen.IsPowered())
        {

        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            triggered = true;
            ShowE(triggered, gen.IsPowered());
            Debug.Log("gen: " + gen.IsPowered());

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.tag == "Player")
        {
            triggered = false;
            ShowE(triggered, gen.IsPowered());
            ShowMenu(triggered);
            Debug.Log("player exit");
        }
    }
    private void ShowE(bool triggered, bool online)
    {
        if (triggered && online)
        {
            pressEPanel.SetActive(triggered);
        }
        else if(triggered && !online)
        {
            offlinePanel.SetActive(triggered);
        }

        if (!triggered)
        {
            offlinePanel.SetActive(triggered);
            pressEPanel.SetActive(triggered);

        }
        //ePress = triggered;
    }

    private void ShowOffline(bool online)
    {

    }

    private void ShowMenu(bool ePress)
    {

        if (ePress)
        {
            ShowE(false, false);
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
