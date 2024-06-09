using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour, IPegavel
{

    private MovimentarPersonagem personagem;

    void IPegavel.Pegar()
    {
        personagem.AtualizarVida(50);
    }

    // Start is called before the first frame update
    void Start()
    {
        personagem = GameObject.FindObjectOfType<MovimentarPersonagem>();
    }

    }
