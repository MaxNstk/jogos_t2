using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFase3 : MonoBehaviour, IPegavel
{
    public void Pegar()
    {
        FindObjectOfType<Fase3Controller>().StartGame();
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
