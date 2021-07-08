using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeZoneScript : MonoBehaviour
{

    //public Camera netcam;

    public GamePlayManage gameManager;
    void OnTriggerEnter(Collider gameObj)
    {
        if (gameObj.GetComponent<SphereCollider>() != null)  // obj must have sphere collider, ie. is a ball
        {
            //netcam.enabled = true;

            gameManager.TurnOnNetCam();
            //Debug.Log("enter strike zone");       
        }
    }
}
