using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    AudioSource src;

    public AudioClip successClip;
    public AudioClip failClip;
    public AudioClip unlockDoorClip;


    void Start()
    {
        src = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public bool IsPlaying()
    {
        return src.isPlaying;
    }

    public IEnumerator PlayClipWaiting(AudioClip clip)
    {
        src.clip = clip;
        src.Play();
        yield return new WaitForSeconds(clip.length);
    }

    public void PlayClip(AudioClip clip)
    {
        src.PlayOneShot(clip);
    }

    internal void Stop()
    {
       src.Stop();
    }
}
