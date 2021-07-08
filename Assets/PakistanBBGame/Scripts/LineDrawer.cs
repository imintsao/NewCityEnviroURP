using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;


// This script should be a component of the LineDrawer GameObject
public class LineDrawer : MonoBehaviour
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

    //private float deg2Radian = (float)Math.PI / 180.0f;

    private float zOffset;
    private float zOffsetAdj;  // further increase zOffset to make actionLine more visible 

    private RaycastHit hitData;
    private Ray ray;

    public GameObject basketBall;  // need reference to ball in order to launch, can also search by tag name
    private Vector3 bBallPos;    // location of basketball, needed to get reference to netSupport

    public GameObject netSupport;  // the whole supporting structure for basketball hoop and net
    private Vector3 supportPos;     // location of net support so that basketball will always be thrown at it

    //private float netSupportHeightAdj; // make the net appear taller to make a larger tilt angle

    public float forceFactor; //  force is proportional to the length of line drawn, and this is the proportionality factor 
    private Vector3 forceFactorVector;

    string tagObjName;
    private Vector3 ballForce;
    

    private float tiltAngle; // angle from ball to hoop panel; this will allow the ball to always be thrown in direction of hoop

    public float slantAngleDeg;  // for testing only; normal use lineAngle instead
   

    // Start is called before the first frame update
    void Start()
    {

        lineTracer = GetComponent<LineRenderer>();    // Get the LineRenderer component that is part of the LineDrawer gameObject
        lineTracer.positionCount = 2;                  // enable only 2 points to define line; thus straight line
        lineTracer.startWidth = 0.01f;
        lineTracer.endWidth = 0.0001f;

        zOffsetAdj = 0.3f;                            // adding to camera near clipplane focal length to make lineRenderer draw better
        //lineTracer.textureMode 
        zOffset= Camera.main.nearClipPlane;            // must shift mouse z position by this offset else nothing will be seen
        zOffset += zOffsetAdj;
                                                      // Note: z position of the clipping plane determines whether action line can be seen or not, so this must be tuned
        
        
        //netSupportHeightAdj = 0.9f;    // adj by trial and error!

        tagObjName = "TestBall";

        
        lineTracer.enabled = false;

        supportPos = netSupport.transform.position;

        bBallPos = basketBall.transform.localPosition;

        forceFactorVector = new Vector3(1.0f, 2.7f, 0.7f);
        //Debug.Log("ball Pos(x,y,z)= " + bBallPos.x + "  ,  " + bBallPos.y + "  ,  " + bBallPos.z);

        //slantAngle = slantAngleDeg * deg2Radian;         // angle between ball and hoop

        
        //supportPos = netSupport.transform.localPosition;
        //zOffset = Camera.main.farClipPlane;              // does not work with far clipPlane
    }

    // Update is called once per frame
    void Update()
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

                Debug.Log("linedrawer startpos: " + "x:  " + nowPos.x + "  y:   " + nowPos.y + "  z:   " + nowPos.z);

                lineTracer.SetPosition(0, nowPos);


            }
            

            
        }

        if ((Input.GetMouseButton(0))&& lineTracer.enabled)    // when left mouse remains pressed
        {
            mousePos = Input.mousePosition;
            mousePos.z = zOffset;

            nowPos = Camera.main.ScreenToWorldPoint(mousePos);
            Debug.Log("linedrawer endpos: " + "x:  " + nowPos.x + "  y:   " + nowPos.y + "  z:   " + nowPos.z);
            lineTracer.SetPosition(1, nowPos);

            

        }

        if ((Input.GetMouseButtonUp(0)) && lineTracer.enabled)
        {
           

            linePos_1 = lineTracer.GetPosition(1);
            linePos_0 = lineTracer.GetPosition(0);
            //Debug.Log("linedrawer endpos: " + "x:  " + linePos_1.x + "  y:   " + linePos_1.y + "  z:   " + linePos_1.z);


            lineLength = (linePos_1 - linePos_0).magnitude;  // linelength represents the magniture of the thrown force

            Debug.Log("LineDrawer linelength  = " + lineLength);

            
            // use bBallPos, specifically center of ball, to calculate angle
            if (linePos_1.x != linePos_0.x)  // make sure we do not divide by zero
            {
                //lineAngle = Mathf.Atan2((linePos_1.y - linePos_0.y), (linePos_1.x - linePos_0.x));
                //Debug.Log("linePos_1: x,y = " + linePos_1.x + " , " + linePos_1.y);
                //Debug.Log("linePos_0: x,y = " + linePos_0.x + " , " + linePos_0.y + " ** ballPos: x,y= " + bBallPos.x + " , " + bBallPos.y);
                //Debug.Log("LINE: arctan of y/x= " + (linePos_0.y - linePos_1.y) + " , " + (linePos_0.x - linePos_1.x));
                //Debug.Log("BALL; arctan of y/x= " + (bBallPos.y - linePos_1.y) + " , " + (bBallPos.x - linePos_1.x));


                lineAngle = Mathf.Atan2((linePos_0.y - linePos_1.y), (linePos_0.x - linePos_1.x));  // use linePos1 as the reference for angle
                //Debug.Log("lineAngle relative to line (in deg) = " + lineAngle * radian2Degree);
                
                
                //lineAngle = Mathf.Atan2((Math.Abs(bBallPos.y - linePos_1.y)), (Math.Abs(bBallPos.x - linePos_1.x)));   // reference center of ball rather than linePos_1 to get more accurate angle
                //Debug.Log("lineAngle relative to BALL (in deg) = " + lineAngle * radian2Degree);
                //lineAngle *= 1.1f;
            }
            else  //angle is along +y or -y direction
            {
                lineAngle = 0;
            }
            
            Debug.Log("LineDrawer lineAngle (in deg) = " + lineAngle * radian2Degree);

            if (bBallPos.y != supportPos.y)
            {
                //supportPos.y += netSupportHeightAdj;   // raise the support y arbitrarily to get a bigger tilt angle
                //tiltAngle = (float)Math.Atan2((bBallPos.x - supportPos.x), (bBallPos.z - supportPos.z));
                Debug.Log("ball, support y pos = " + bBallPos.y + "   ,  " + supportPos.y);
                //tiltAngle = (float)Math.Atan2((bBallPos.y - supportPos.y), (bBallPos.z - supportPos.z));
                tiltAngle = (float)Math.Atan2((supportPos.y-bBallPos.y), (supportPos.z- bBallPos.z));
                Debug.Log(" tilt angle y/z = " + (supportPos.y - bBallPos.y) + "   ,   " + (supportPos.z - bBallPos.z));

            }
            else tiltAngle = 0;

            Debug.Log("LineDrawer tiltAngle (in deg) = " + tiltAngle*radian2Degree);
            
            /*
            {
                if ((linePos_1.y - linePos_0.y)>=0)
                {
                    lineAngle = (float)Math.PI / 4.0f;
                }else
                {
                    lineAngle = -3.0f*(float)Math.PI / 4.0f;
                }
                    
            }
            */

            lineAngleDeg = lineAngle*radian2Degree;
            
            
            lineTracer.enabled = false;   // hide drawn line

            //Debug.Log("line length not used : " + lineLength + " ;   line Angle degree:" + lineAngle);

            // make sure ball is always thrown in direction of the net. Therefore need offset angle between ball and net on the x-z plane
            // make sure we do not divide by zero

            bBallPos = basketBall.transform.position;

            //Debug.Log("lineDrawer ball pos : " + bBallPos.x + "  ,  " + bBallPos.y + "  ,  " + bBallPos.z);
            
            //now launch ball based on line length and direction
            Rigidbody rb = basketBall.GetComponent<Rigidbody>();

            
            ballForce.y = lineLength * (float)Math.Sin(tiltAngle) * forceFactor* forceFactorVector.y;
            /*
            ballForce.x = lineLength * (float)Math.Cos(slantAngle) * forceFactor;
            ballForce.z = lineLength * (float)Math.Sin(slantAngle) * forceFactor;
            */
            ballForce.x = lineLength * (float)Math.Cos(lineAngle) * forceFactor*forceFactorVector.x;
            ballForce.z = lineLength * (float)Math.Sin(lineAngle) * forceFactor * forceFactorVector.z;

            rb.useGravity = true;
            rb.AddForce(ballForce, ForceMode.Impulse);

            //ballForce.x = lineLength * (float)Math.Sin(slantAngle) * forceFactor;
            //ballForce.z = lineLength * (float)Math.Cos(slantAngle) * forceFactor;

            /*
            ballForce.x = lineLength * (float)Math.Sin(lineAngle) * forceFactor;
            ballForce.z = lineLength * (float)Math.Cos(lineAngle) * forceFactor;
            */
            //ballForce.y = -lineLength * (float)Math.Sin(lineAngle) * forceFactor;
            /*
            ballForce.y = -lineLength * (float)Math.Sin(tiltAngle) * forceFactor;
            ballForce.x = -lineLength * (float)Math.Sin(lineAngle)*  forceFactor;
            ballForce.z = -lineLength * (float)Math.Cos(lineAngle)* forceFactor;
            */


        }
    }

}
