using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManage : MonoBehaviour
{
    public GameObject ballObj;
    public KeyCode resetKey;
    

    public Camera[] netcams;
    

    private Vector3 tmpPos;

    private Camera activeCam;
    private int camIndex;
   
    private void Start()
    {
        
        
        foreach (Camera c in netcams)  // disable all netcams to start
        {
            c.enabled = false;
        }
        
        activeCam = null;
        ballObj.GetComponent<Rigidbody>().useGravity = false;  // ball has no gravity initially
        
        
    }
    public void TurnOnNetCam()
    {
        if (activeCam!=null) {
            activeCam.enabled = false; // disable previous active cam, else it is still active
        }
        camIndex = Random.Range(0, netcams.Length);  // now turn on a new active cam
        //camIndex = 2; // for testing only
        activeCam = netcams[camIndex];
        
       
        activeCam.enabled = true;
    }


    void Update()
    {
        if (Input.GetKeyUp(resetKey))
        {

            if (activeCam != null)
            {
                activeCam.enabled = false; // disable previous active cam, else it is still active
            }
            //activeCam.enabled = false;
            
            ballObj.GetComponent<BallScript>().ResetPos();  // Get ball to reset to a new position
            
            //Debug.Log("GameManage ball pos :  " + tmpPos.x + "  ,  " + tmpPos.y + "  ,  " + tmpPos.z);
        }
    }

    
    /*
    void Update()  // for testing purpose, limit the choice of new position to just 3, and feed position to ball's ResetPos
    {
        if (Input.GetKeyUp(resetKey))
        {
            posIndex++;
            if (posIndex == 2) posIndex = 0;

            ballObj.GetComponent<BallScript>().ResetPos(ballPos[posIndex]);
            tmpPos = ballObj.transform.position;
            Debug.Log("GameManage ball pos :  " + tmpPos.x + "  ,  " + tmpPos.y + "  ,  " + tmpPos.z);
        }
    }
    */
}
