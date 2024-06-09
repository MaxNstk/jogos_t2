using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoBebeCresce : MonoBehaviour
{
    public GameObject inimigoAdulto;
    public float tempoParaEclodir = 10f;
    private float timer = 0f;

    public int vida = 30;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= tempoParaEclodir)
        {
            Vector3 position = transform.position;
            Destroy(gameObject);
            Instantiate(inimigoAdulto, position, Quaternion.identity);
        }
    }
}
