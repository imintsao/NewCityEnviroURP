using UnityEngine.Audio;
using UnityEngine;

public class BallCollide : MonoBehaviour
{

    //public AudioManager audiioManager;

    AudioSource netHit;

    void Start()
    {
        netHit = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        
        //int i = 0;


        //audiioManager.Play("NetHit");
        foreach (var contact in collision.contacts)
        {
            //sounds.Play();
            netHit.Play();
            //audiioManager.Play("NetHit");
            //Debug.Log("hitch contact hit ");
        }
    }
}
