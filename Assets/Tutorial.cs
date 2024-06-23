using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public RawImage imagem;
    private AudioSource audioSource;
    private GameObject cursor;
    private int fase;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cursor = GameObject.FindGameObjectWithTag("Cursor");
        this.SetFase(1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            audioSource.Play();
            imagem.gameObject.SetActive(!imagem.gameObject.activeSelf);
            cursor.SetActive(!imagem.gameObject.activeSelf);
        }
    }

    public void SetFase(int novaFase)
    {
        this.fase = novaFase;
        Texture2D texture = Resources.Load<Texture2D>("Tutorials/Fase " + this.fase);
        imagem.texture = texture;
    }
}
