using UnityEngine.UI;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public float interactionRange = 2f;
    public KeyCode interactKey = KeyCode.E;

    public GameObject computerUIPanel;

    private Transform player;

    private bool isPlayerInRange = false;

    public ComputerDoor door;

    public Generator specificGenerator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        computerUIPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        isPlayerInRange = Vector3.Distance(player.position, transform.position) <= interactionRange;

        if (isPlayerInRange && Input.GetKeyDown(interactKey))
        {
            if (specificGenerator.IsPowered())
            {
                OpenComputerUI();
            }
            else
            {
                Debug.Log("The generator is not on. Cannot access the computer.");
            }
        }
    }

    private void OpenComputerUI()
    {
        computerUIPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

    public void OpenDoor()
    {
        if (door != null)
        {
            door.RequestOpen();
        }
    }

    public void CloseComputerUI()
    {
        computerUIPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
