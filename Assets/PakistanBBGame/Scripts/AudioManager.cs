using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach (Sound s in sounds)
        {
            
            s.source = gameObject.GetComponent<AudioSource>();
            if (s.source==null)
            {
                Debug.LogWarning("no source");
                return;
            }
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            Debug.Log("adding sound  "+i++);
            Play("NetHit");
        }
    }

    private void nnStart()
    {
        Play("NetHit");
    }

    public void Play(string name)
    {
        Sound s= Array.Find(sounds, sound => sound.name == name);
        if (s==null)
        {
            Debug.LogWarning("Sound name not found");
            return;
        }
        
        s.source.Play();


    }

}
