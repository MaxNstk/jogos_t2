using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindableButton : MonoBehaviour, IPegavel
{

    public bool wasFound = false;
    public void Pegar()
    {
        Debug.Log("Peguei mais um");
        this.wasFound = true;
        ButtonController controller = FindObjectOfType<ButtonController>();
        controller.foundButtons++;
        controller.UpdateFindableObject();
        Destroy(gameObject); 
    }

    internal void setActive()
    {
        this.gameObject.tag = "Pegar";
        GetComponent<AudioSource>().Play();
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
