using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    AudioSource src;

    void Start()
    {
        this.src = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    IEnumerator PlayClip(AudioClip clip)
    {
        src.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
    }

    IEnumerator WaitSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

}
