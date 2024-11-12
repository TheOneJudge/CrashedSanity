using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ComputerInterface : MonoBehaviour
{
    public UnityEvent myEvent;

    [SerializeField] private GameObject pressEPanel, offlinePanel, menuPanel;
    [SerializeField] private GameObject sanityBarCanvas;

    [SerializeField] private Generator gen;

    private bool triggered = false;
    private bool ePress = false;


    [Header("Password")]

    [SerializeField] private GameObject passwordPanel, error;

    [SerializeField] private string password;

    [SerializeField] private InputField passwordInput;


    private void Start()
    {
        pressEPanel.SetActive(false);
        menuPanel.SetActive(false);
        passwordPanel.SetActive(false);
        offlinePanel.SetActive(false);
    }


    private void Update()
    {
        //pressEPanel.transform.localRotation = cam.transform.localRotation;

        //Debug.Log(cam.transform.localRotation);

        if (triggered && gen.IsPowered())
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                ePress = true;
                sanityBarCanvas.SetActive(false );
                EnterPassword();
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
            //ShowMenu();
            Debug.Log("player exit");

            sanityBarCanvas.SetActive(true);

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

    private void EnterPassword()
    {
        passwordInput.text = "";
        passwordInput.interactable = true;
        passwordPanel.SetActive(true);
        pressEPanel.SetActive(false);
        menuPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ShowMenu()
    {

        ShowE(false, false);
        menuPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

        passwordInput.interactable = false;
        passwordPanel.SetActive(false);


        /*if (ePress)
        {
            ShowE(false, false);
            menuPanel.SetActive(ePress);

            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            menuPanel.SetActive(false);
            Cursor.lockState= CursorLockMode.Locked;
        }*/

        //Cursor.visible = ePress;
    }

    [SerializeField] public void PassCheck()
    {
        if (passwordInput.text == password)
        {
            ShowMenu();
        }
        else
        {
            error.SetActive(true);
            
            Debug.Log("Incorrect Password");

            float timer = 0;
            timer += Time.deltaTime;
            if(timer >= 2f)
            {
                timer = 0;
                error.SetActive(false);
            }
        }
    }

}
