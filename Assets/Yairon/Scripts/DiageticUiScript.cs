using UnityEditor.Animations;
using UnityEngine;

public class DiageticUiScript : MonoBehaviour
{
    [SerializeField] private GameObject canvas, point;
    [SerializeField] private Camera cam;

    [SerializeField] private Animator anim;

    private bool active = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab) && !active)
        {
            active = true;
            OnDUIDisplay(active);
        }
        else if(Input.GetKeyUp(KeyCode.Tab) && active)
        {
            active = false;
        }
        
        canvas.transform.position = point.transform.position;
        canvas.transform.localRotation = cam.transform.localRotation;
    }

    private void OnDUIDisplay(bool active)
    {

        if (active)
        {
            anim.SetBool("displayOn", true);
            canvas.SetActive(true);

            cam.transform.LookAt(point.transform.position);

            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            anim.SetBool("displayOn", false);
            Cursor.lockState = CursorLockMode.Locked;
        }

        

    }

}
