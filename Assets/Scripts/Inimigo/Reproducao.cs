using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Reproducao : MonoBehaviour
{
    public GameObject novoAsset;
    public float tempoParaReproduzir = 10f;
    public bool deletarPrefabAtual = true;
    public bool automatizado = true;

    private bool reproduziu = false;
    public float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (reproduziu || !automatizado)
            return;

        if (timer >= tempoParaReproduzir)
        {
            Reproduzir();
        }
    }


    public void Reproduzir()
    {
        reproduziu = true;
        Vector3 position = transform.position;
        position.y += 2;
        if (deletarPrefabAtual)
            Destroy(gameObject);
        Instantiate(novoAsset, position, Quaternion.identity);
        if (deletarPrefabAtual)
            this.enabled = false;
    }
}

