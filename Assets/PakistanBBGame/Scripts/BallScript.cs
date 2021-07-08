using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

    public GameObject netSupport;
    private Vector3 pos;

    private float netPostPosX = 0.9f;  // x position of the center post of netSupport
    
    public void ResetPos()
    {
        transform.GetComponent<Rigidbody>().useGravity = false;   // resetball to enable ball to float in air
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;  // reset to zero velocity as ball might be still rolling
        transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        //Debug.Log("netSupport x = " + netSupport.transform.position.x);
        pos.x = netPostPosX+Random.Range(-0.9f,0.9f);

        //pos.y = netSupport.transform.position.y + Random.Range(-0.5f, 0.5f);  // reference netsupport height to set ball height
        //Debug.Log("ball height rel to net support = " + pos.y);
        pos.y = Random.Range(2.1f, 2.35f);
        pos.z = 3.9f;
        //pos.z = 4.3f;
        //pos.z = 4.5f;
        //pos.z = Random.Range(5.0f, 7.5f);
        //pos.z = 7.5f;
        transform.position = pos;

        Debug.Log("ball pos : " + pos.x + "  ,  " + pos.y + "  ,  " + pos.z);

    }

    public void ResetPos(Vector3 newPos)
    {
        
        transform.GetComponent<Rigidbody>().useGravity = false;   // resetball to enable ball to float in air
        transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);  // reset to zero velocity as ball might be still rolling

        transform.position = newPos;

        Debug.Log("ball pos : " + pos.x + "  ,  " + pos.y + "  ,  " + pos.z);

    }
}
