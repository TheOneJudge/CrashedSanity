using UnityEditor.Animations;
using UnityEngine;

public class DiageticUiScript : MonoBehaviour
{
    [SerializeField] private GameObject canvas, dCanvas, point;
    [SerializeField] private Camera cam;

    public Texture2D cursorTex;


    [SerializeField] private Animator anim;

    private bool active = false;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
        Cursor.SetCursor(cursorTex, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Locked;
        canvas.SetActive(false);
        dCanvas.SetActive(false);

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
            OnDUIDisplay(active);

        }

        

        //canvas.transform.position = point.transform.position;
        //canvas.transform.localRotation = cam.transform.localRotation;
    }

    private void OnDUIDisplay(bool active)
    {

        if (active)
        {
            
            canvas.SetActive(active);
            //dCanvas.SetActive(active);

            anim.SetBool("onDisplay", true);
            
            //cam.transform.LookAt(point.transform.position);

            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            anim.SetBool("onDisplay", false);

            dCanvas.SetActive(active);
            canvas.SetActive(active);


            Cursor.lockState = CursorLockMode.Locked;
        }

        Cursor.visible = active;


    }

}