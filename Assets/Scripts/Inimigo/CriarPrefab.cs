using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriarPrefab : MonoBehaviour
{
    public GameObject novoAsset;
    public float tempoParaTransformar = 10f;
    private float timer = 0f;
    public bool deletarPrefabAtual = true;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= tempoParaTransformar)
            Criar();
    }

    void Criar()
    {
        Vector3 position = transform.position;
        if (deletarPrefabAtual)
            Destroy(gameObject);
        Instantiate(novoAsset, position, Quaternion.identity);
    }
}
