using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFase2 : MonoBehaviour, IPegavel
{
    public void Pegar()
    {
        FindObjectOfType<PuzzleFase2>().StartGame();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
