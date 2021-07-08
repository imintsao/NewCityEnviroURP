using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBoardCollide : MonoBehaviour
{

    AudioSource boardHit;
    // Start is called before the first frame update
    void Start()
    {
        boardHit = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {

       foreach (var contact in collision.contacts)
        {
            
            boardHit.Play();
            
        }
    }
}
