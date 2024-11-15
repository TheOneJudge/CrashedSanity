using UnityEditor.Animations;
using UnityEngine;

public class DiageticUiScript : MonoBehaviour
{
    [SerializeField] private GameObject canvas, dCanvas, point, infoPanel;
    [SerializeField] private GameObject cam;

    public Texture2D cursorTex;


    [SerializeField] private Animator anim, playerAnim;

    private bool active = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;


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
        canvas.transform.localRotation = cam.transform.localRotation;
    }

    private void OnDUIDisplay(bool active)
    {

        if (active)
        {
            
            canvas.SetActive(active);
            //dCanvas.SetActive(active);

            playerAnim.SetBool("onDisplay", true);
            anim.SetBool("onDisplay", true);
            
            //cam.transform.LookAt(point.transform.position);

            Cursor.lockState = CursorLockMode.None;

        }
        else
        {
            playerAnim.SetBool("onDisplay", false);

            anim.SetBool("onDisplay", false);

            dCanvas.SetActive(active);
            canvas.SetActive(active);


            Cursor.lockState = CursorLockMode.Locked;

        }

        Cursor.visible = active;


    }

    public void OnDUIPanel()
    {
        anim.SetBool("openPanel", true);
        Debug.Log("panel open");
    }

}
