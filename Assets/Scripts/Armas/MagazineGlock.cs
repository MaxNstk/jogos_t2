using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineGlock : MonoBehaviour, IPegavel
{
    private AudioSource audioSrc;
    public AudioClip somMagazine;


    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.clip = somMagazine;
    }

    public void Pegar()
    {
        audioSrc.Play();
        Glock g = GameObject.FindWithTag("Arma").GetComponent<Glock>();
        g.AddCarregador();
        
    }
}
