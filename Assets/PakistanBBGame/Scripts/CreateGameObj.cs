using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CreateGameObj : MonoBehaviour
{

    public KeyCode redKey;  // 'S' key : switch to red Material
    public KeyCode invisKey;  // 'H' key :  switch to invis Material

    public KeyCode tiltKey;  // 'T' key :  tilt the ring

    public float ringDiameter;  
    public int ringTotal;    // total number of cylinders forming the ring of diameter ringDiameter

    public float cylinderLength; // how long is each cylinder 
    
    public float cylinderWidth; // the width of the cylinder; cylinder cross secion should be kept to circular, so x and z scale must be kept to same

    public GameObject parentRing;


    GameObject cubeObj;

    Material darkBlue;
    Material brightRedMat;
    Material invisMat;

      
    
    // Start is called before the first frame update
    void Start()
    {
        
        // ****** Note all Materials referenced must be placed in Resources folder, else they will not be found

        invisMat = Resources.Load("Invisible", typeof(Material)) as Material;
        brightRedMat = Resources.Load("Bright red", typeof(Material)) as Material;
        darkBlue = Resources.Load("DarkBlu", typeof(Material)) as Material;
       
        CreateCubeObj();


        CreateRingSeries();

        //cylinderLength = ((float)Math.PI * ringDiameter)/ringTotal;  // each cylinder of length 2*Pi*R/ringTotal
        //cylinderLength *= 100;
        Debug.Log("cyl length:" + cylinderLength);
    }


    private void CreateRingSeries()
    {
        float delAng = 2.0f * (float)Math.PI /ringTotal;  // divide up 2*PI  into ringTotal of slices each of delAng
        float ringRadius = ringDiameter / 2.0f;

        float nowAng;
        float tmpAng;
        float deg2Rad = 180.0f / (float)Math.PI;  // deg to radian

        for (int i = 0; i < ringTotal; i++)
        {

            nowAng = i * delAng;
            float x = ringRadius * (float)Math.Sin(nowAng);
            float z = ringRadius * (float)Math.Cos(nowAng);
            float h = 2.0f;

            GameObject cyl = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

            //cyl.transform.parent = parentRing.transform;

            cyl.transform.localPosition = new Vector3(x, h, z);
            cyl.transform.localScale = new Vector3(cylinderWidth, cylinderLength, cylinderWidth);
            tmpAng = 90.0f + nowAng * deg2Rad; 
            
            /*
            if (i<=10)
            {
                Debug.Log("i =" + i + "  x:" + x + "  h:" + h + "   z:" + z);
            }
            */
            //Debug.Log(i+"  angle:" + tmpAng);

            /*
              1. Cylinder after rotation along x direction is now parallel to ground and aligned with the z-axis
              2. We first rotate cylinder by 90 deg to get it to be perpendicular to the radius, then we increment this successivly along the whole circle

            */
            cyl.transform.Rotate(90.0f, tmpAng, 0, Space.World);              // rotate 90 deg along x axis so that parallel to plane, then reorient based on current position

            cyl.GetComponent<Renderer>().material = brightRedMat;

            cyl.transform.parent = parentRing.transform;    // set all the cylinders to have a single parent so that one can just rotate the parent if desired

        }

    }

    
    private void RotateRing(float angle)
    {

        parentRing.transform.Rotate(0, 0, angle, Space.Self);
    }

    private void CreateCubeObj()
    {

        cubeObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cubeObj.transform.localScale = new Vector3(0.5f, 2.0f, 0.5f);
        cubeObj.transform.localPosition = new Vector3(0, 3, 0);
        cubeObj.name = "Script Cube";
               
        cubeObj.GetComponent<MeshRenderer>().material =darkBlue;
        cubeObj.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
    }
    
    private void FixedUpdate()
    {
        
        if (Input.GetKey(redKey))
        {
            cubeObj.GetComponent<Renderer>().material = brightRedMat;

        }

        if (Input.GetKey(invisKey))
        {
            cubeObj.GetComponent<Renderer>().material = invisMat;
        }

        if (Input.GetKey(tiltKey))
        {
            RotateRing(30.0f);
        }
    }
}
