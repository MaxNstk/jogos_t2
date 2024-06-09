using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvoInimigoComum : MonoBehaviour, ILevarDano
{

    public int vida = 30;

    void Update()
    {
        if (vida <= 0)
        {
            Morrer();
        }
    }

    public void LevarDano(int dano)
    {
        vida -= dano;
    }

    private void Morrer()
    {
        Destroy(gameObject);
    }
}
