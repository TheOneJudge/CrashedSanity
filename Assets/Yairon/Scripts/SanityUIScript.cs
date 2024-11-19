using UnityEngine;
using UnityEngine.UI;

public class SanityUIScript : MonoBehaviour
{

    [SerializeField] private GameObject canvas, point;

    public SanitySystem sSystem;
    private float sanity;
    public Image sanityUI;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sanity = sSystem.GetCurrentSanity();
        sanityUI.fillAmount = sanity/ 100;
        
        canvas.transform.position = point.transform.position;
        canvas.transform.localRotation = point.transform.localRotation;
    }
}
