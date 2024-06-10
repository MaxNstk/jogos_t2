using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoSonoro : MonoBehaviour, IPegavel
{
    public void Pegar()
    {
        AudioSource src = GetComponent<AudioSource>();
        src.Play();
        PuzzleFase2 controller = FindObjectOfType<PuzzleFase2>();
        controller.clipPlayed(src.clip);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
