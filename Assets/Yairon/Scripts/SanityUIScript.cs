using UnityEngine;

public class SanityUIScript : MonoBehaviour
{

    [SerializeField] private GameObject canvas, point;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        canvas.transform.position = point.transform.position;
        canvas.transform.localRotation = point.transform.localRotation;
    }
}
