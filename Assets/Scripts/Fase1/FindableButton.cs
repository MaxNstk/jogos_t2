using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindableButton : MonoBehaviour, IPegavel
{

    public bool wasFound = false;
    public void Pegar()
    {
        this.wasFound = true;
        PuzzleFase1 controller = FindObjectOfType<PuzzleFase1>();
        controller.foundButtons++;
        controller.UpdateFindableObject();
        Kill();
    }

    internal void setActive()
    {
        this.gameObject.tag = "Pegar";
        GetComponent<AudioSource>().Play();
    }

    public void Kill()
    {      
        Destroy(gameObject); 
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
