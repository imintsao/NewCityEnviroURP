using System;
using UnityEngine;
using UnityEngine.Tilemaps;


// This script should be a component of the LineDrawer GameObject
public class ForceVector : MonoBehaviour
{
    private LineRenderer lineTracer;    // must hv a gameobject that contains a LineRenderer

    private Vector3 mousePos;  // current mouse position in world space
    private Vector3 nowPos;    // current mouse position as mouse is being moved
    private float lineLength; // how long is the line
    private float lineAngle = 0;  // angle drawn line makes with respect to object clicked
    private float lineAngleDeg;
    private Vector3 linePos_1;
    private Vector3 linePos_0;

    private float radian2Degree = 180.0f / (float)Math.PI;

    private float zOffset;  // camera is pointing along x axis from +x towards -x direction

    private RaycastHit hitData;
    private Ray ray;

    public GameObject basketBall;  // need reference to ball in order to launch, can also search by tag name
    private Vector3 bBallPos;    // location of basketball, needed to get reference to netSupport

    public GameObject netSupport;  // the whole supporting structure for basketball hoop and net
    private Vector3 supportPos;     // location of net support so that basketball will always be thrown at it

    public float forceFactor; //  force is proportional to the length of line drawn, and this is the proportionality factor 

    string tagObjName;
    private Vector3 ballForce;

    private float offSetAngle; // angle from ball to hoop panel; this will allow the ball to always be thrown in direction of hoop


    // Start is called before the first frame update
    void nnStart()
    {

        lineTracer = GetComponent<LineRenderer>();    // Get the LineRenderer component that is part of the LineDrawer gameObject
        lineTracer.positionCount = 2;                  // enable only 2 points to define line; thus straight line
        lineTracer.startWidth = 0.01f;
        lineTracer.endWidth = 0.0001f;

        //lineTracer.textureMode 
        zOffset = Camera.main.nearClipPlane;            // must shift mouse z position by this offset else nothing will be seen

        tagObjName = "TestBall";


        lineTracer.enabled = false;

        supportPos = netSupport.transform.position;  // net position in world coord
        

        //zOffset = Camera.main.farClipPlane;              // does not work with far clipPlane
    }

    // Update is called once per frame
    void nnUpdate()
    {
        if (Input.GetMouseButtonDown(0))  // when left mouse button is pressed down
        {
            mousePos = Input.mousePosition;
            ray = Camera.main.ScreenPointToRay(mousePos);  // check if specific gameObj is hit before allowing line drawing
            if ((Physics.Raycast(ray, out hitData, 300)) && (hitData.transform.tag == tagObjName))  // ** Raycast distance initially at 1000, now try 300 to make faster
            {

                lineTracer.enabled = true;

                mousePos.z = zOffset;

                nowPos = Camera.main.ScreenToWorldPoint(mousePos);

               Debug.Log("start mouse position: " + "x:  " + nowPos.x + "  y:   " + nowPos.y + "  z:   " + nowPos.z);

                lineTracer.SetPosition(0, nowPos);


            }



        }

        if ((Input.GetMouseButton(0)) && lineTracer.enabled)    // when left mouse remains pressed
        {
            mousePos = Input.mousePosition;
            mousePos.z = zOffset;

            nowPos = Camera.main.ScreenToWorldPoint(mousePos);

            lineTracer.SetPosition(1, nowPos);



        }

        if ((Input.GetMouseButtonUp(0)) && lineTracer.enabled)
        {


            linePos_1 = lineTracer.GetPosition(1);
            linePos_0 = lineTracer.GetPosition(0);
            lineLength = (linePos_1 - linePos_0).magnitude;  // linelength represents the magniture of the thrown force
            if (linePos_1.z != linePos_0.z)  // make sure we do not divide by zero
            {
                lineAngle = Mathf.Atan2((linePos_1.y - linePos_0.y), (linePos_1.z - linePos_0.z));  // line is drawn on y-z plane

            }
            else  //angle is along +y or -y direction
            {
                if ((linePos_1.y - linePos_0.y) >= 0)
                {
                    lineAngle = (float)Math.PI / 4.0f;
                }
                else
                {
                    lineAngle = -3.0f * (float)Math.PI / 4.0f;
                }

            }

            lineAngleDeg = lineAngle * radian2Degree;


            lineTracer.enabled = false;   // hide drawn line

            Debug.Log("line length: " + lineLength + " ;   line Angle degree:" + lineAngle);

            // make sure ball is always thrown in direction of the net. Therefore need offset angle between ball and net on the x-z plane
            // make sure we do not divide by zero

            bBallPos = basketBall.transform.position;

            //Debug.Log("lineDrawer ball pos : " + bBallPos.x + "  ,  " + bBallPos.y + "  ,  " + bBallPos.z);
            if (bBallPos.y != supportPos.y)
            {
                offSetAngle = (float)Math.Atan2((bBallPos.y - supportPos.y), (bBallPos.y - supportPos.y));
            }
            else offSetAngle = 0;

            //now launch ball based on line length and direction
            Rigidbody rb = basketBall.GetComponent<Rigidbody>();

            //ballForce.y = -lineLength * (float)Math.Sin(lineAngle) * forceFactor;
            ballForce.y = -(lineLength / (float)Math.Cos(offSetAngle))*forceFactor;    // this approx force in y direction by taking screen-drawn forceVector as the cosine of the total force vector
            
            //following represents Approach A (see LineDrawer documention; this seems to work fine
            ballForce.x = -lineLength * (float)Math.Cos(lineAngle) * forceFactor;
            ballForce.z = -lineLength * (float)Math.Sin(lineAngle) * forceFactor;

            //Debug.Log("ball force:"+" x:  "+ ballForce.x +"  y: "+ ballForce.y + "  z : "+ ballForce.z);

            

            rb.useGravity = true;
            rb.AddForce(ballForce, ForceMode.Impulse);


            //lineTracer.GetPosition(1)

        }
    }

}
