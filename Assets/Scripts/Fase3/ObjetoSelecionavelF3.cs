using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoSelecionavelF3 : MonoBehaviour, IPegavel
{
    public AudioClip clip;

    public void Pegar()
    {
        Fase3Controller controller = FindObjectOfType<Fase3Controller>();
        controller.clipPlayed(clip);
    }

    void Start()
    {
    }

    void Update()
    {
    }

}
