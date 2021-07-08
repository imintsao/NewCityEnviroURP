using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// change wind speed using keyboard
public class WindAdjust : MonoBehaviour
{
    public float windSpeed;
    public Material grassMat;   // this references the material to which this shadergraph applies; 
                                // note: must define this in Inspector
    public float delWindSpeed;   // incremental change to wind speed
    //public float minWindSpeed;
    public float maxWindSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start console log");
        windSpeed = (float)0.2;
        grassMat.SetFloat("Vector1_44D5ED92",windSpeed);
        delWindSpeed = (float)0.1;
        maxWindSpeed = (float)2.0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && (windSpeed>delWindSpeed))
        {
            windSpeed -= delWindSpeed;
            grassMat.SetFloat("Vector1_44D5ED92", windSpeed);
            Debug.Log("speed minus");

        }
        //if (Input.GetKeyDown(KeyCode.RightArrow) && (windSpeed < maxWindSpeed))
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            windSpeed += delWindSpeed;
            grassMat.SetFloat("Vector1_44D5ED92", windSpeed);
            Debug.Log("speed add");
        }
    }
}
