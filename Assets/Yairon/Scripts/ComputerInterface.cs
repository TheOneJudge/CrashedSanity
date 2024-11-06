using UnityEngine;
using UnityEngine.Events;

public class ComputerInterface : MonoBehaviour
{
    public UnityEvent myEvent;
    
    [SerializeField] private GameObject canvas, cam;

    private void Start()
    {
        canvas.SetActive(false);
    }


    private void Update()
    {
        canvas.transform.localRotation = Camera.main.transform.localRotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ShowE(true);
            Debug.Log("player enter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ShowE(false);
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
            canvas.SetActive(false);
        }
    }
}
