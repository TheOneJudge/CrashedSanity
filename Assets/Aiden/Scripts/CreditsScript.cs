using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    public GameObject MainText;
    public GameObject StationaryTings;
    bool stopTime = false;
    public float step = 20;
    Vector3 TextPos, blockerPos;
    

    // Update is called once per frame
    void Start()
    {
        TextPos = MainText.transform.position;
        blockerPos = StationaryTings.transform.position;
    }

    void Update()
    {
        //constantly move MainText;
        TextPos.y += step * Time.deltaTime;
        MainText.transform.position = TextPos;
        print(TextPos.y);
        
        //check if the StationaryTings object has reached a given height and stop its movement if it has
        if(!stopTime)
        {
            blockerPos.y += step * Time.deltaTime;
            StationaryTings.transform.position = blockerPos;
            //print(blockerPos.y);
        }

        if(blockerPos.y >= 1000)
        {
            stopTime = true;
        }


        //change scene if the hieght of MainText has exceeded a given height
        if(TextPos.y >= 3500)
        {
             SceneManager.LoadScene(0);
        }
    }
}
