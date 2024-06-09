using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegarGlock : MonoBehaviour, IPegavel
{
    private AudioSource audioSrc;
    public AudioClip somPegarGlock;
    public GameObject glockPersonagem;


    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.clip = somPegarGlock;
    }

    public void Pegar()
    {
        audioSrc.Play();
        glockPersonagem.SetActive(true);

    }
}