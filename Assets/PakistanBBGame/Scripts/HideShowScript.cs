using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShowScript : MonoBehaviour
{

    public GameObject ball;

    public KeyCode displayBall;  // use Key 'D' for display

    public Vector3 initBallPos;
    // Start is called before the first frame update
    void Start()
    {
        ball.SetActive(false);  // hide the basketball initially
        ball.transform.localPosition = initBallPos;
    }

    
    void FixedUpdate()
    {
        if (Input.GetKey(displayBall))
        {
            ball.SetActive(true);
        }
    }






}
