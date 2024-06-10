using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoSonoro : MonoBehaviour, IPegavel
{
    public AudioClip clip;

    public void Pegar()
    {
        PuzzleFase2 controller = FindObjectOfType<PuzzleFase2>();
        controller.clipPlayed(clip);
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
