using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int totalScore;

    public Text scoreText;

    private AudioSource cheers;
    public AudioClip[] clips;   // collection of clips for applause

    private int audioIndex;

    static int hitCount;  // must hit two score areas inside the net before it is a real hit; this prevents ball just hitting one score area from outside net
                          // this var is static so that all(both) score areas share the same var
    private void Start()
    {
        hitCount = 0;
        totalScore = 0;
        UpdateScoreText();
        //cheers = GetComponent<AudioSource>();
        cheers = gameObject.AddComponent<AudioSource>();

        
        //clips = GetComponent<AudioClip>();
        //audioIndex = 0;
    }

    void OnTriggerEnter(Collider gameObj)
    {
        if (gameObj.GetComponent<SphereCollider>()!=null)  // obj must have sphere collider, ie. is a ball
        {
            if (hitCount >0)  // one score area has already been hit; this would be second one, therefore real hit
            {
                audioIndex = Random.Range(0, clips.Length);
                cheers.clip = clips[audioIndex];
                //cheers.clip = clips;
                cheers.Play();
                totalScore++;
                UpdateScoreText();
                
                Debug.Log("hitcount= " + hitCount);
                hitCount = 0;
                //Debug.Log("score: "+ totalScore);
            }
            else hitCount++;
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "SCORE: " + totalScore;

    }
}
