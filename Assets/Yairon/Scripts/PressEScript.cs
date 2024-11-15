using TMPro;
using UnityEngine;

public class PressEScript : MonoBehaviour
{

    [SerializeField] private GameObject pressEPanel, cam;
    [SerializeField] private Generator gen;
    [SerializeField] private TMPro.TextMeshProUGUI text;

    [SerializeField] private bool active = false;


    private void Start()
    {
        pressEPanel.SetActive(active);
    }

    private void OnTriggerEnter(Collider other)
    {
        active = true;
        pressEPanel.SetActive(active);
    }

    private void OnTriggerExit(Collider other)
    {
        active = false;
        pressEPanel.SetActive(active);
    }

    private void Update()
    {
        pressEPanel.transform.rotation = pressEPanel.transform.rotation;

        if (gen.IsPowered())
        {
            text.text = "online";
        }
        else
        {
            {
                text.text = "press e";
            }
        }

    }

}
