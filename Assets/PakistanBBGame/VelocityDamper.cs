using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityDamper : MonoBehaviour
{

    
    private Vector3 ballVel;
    private float velFactor;
    // Start is called before the first frame update
    void Start()
    {
        if (CompareTag("UpperDamper"))   // adj vel damping depending on location of back panel
        {
            velFactor = 0.3f;            // upper panel damping
        }
        else
        {

            velFactor = 0.1f;           // sweet spot strike zone damping
        }
    }


    private void OnTriggerEnter(Collider collider)
    {

        //GameObject gameObject = collider.gameObject;
        if (collider.GetComponent<SphereCollider>() != null)  // obj must have sphere collider, ie. is a ball
        {
            Rigidbody rb = collider.GetComponent<Collider>().attachedRigidbody;  // note special method to get rigidbody from collider
            ballVel = rb.velocity;
            //Debug.Log("in vel = " + rb.velocity.x + "  ,  " + rb.velocity.y + "  ,  " + rb.velocity.z);
            //rb.velocity = new Vector3(ballVel.x, ballVel.y * velFactor, (float)ballVel.z * velFactor);
            rb.velocity = rb.velocity * velFactor;
            //Debug.Log("out vel = " + rb.velocity.x + "  ,  " + rb.velocity.y + "  ,  " + rb.velocity.z);
        }
    }
    
}
