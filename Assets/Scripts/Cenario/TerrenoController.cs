using SunTemple;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrenoController : MonoBehaviour
{
    public List<InimigoComum> enemies = new List<InimigoComum>();
    private bool morreramTodosInimigos = false;
    private bool BossMorreu = false;
    public Boss boss;
    public Door portaLateral;
    public Door portaPrincipal;

    public AudioSource somFundo;
    public AudioSource sonsadicionais;

    public AudioClip audioSalaInicial;
    public AudioClip audioFora;
    public AudioClip audioBoss;

    public AudioClip portaDestrancar;

    private bool fundoPausado = false;

    public int inimigosRemanescentes;

    void Start()
    {
        somFundo = GetComponents<AudioSource>()[0];
        sonsadicionais = GetComponents<AudioSource>()[1];

        somFundo.clip = audioSalaInicial;
        somFundo.Play();
    }

    void Update()
    {
        if (!morreramTodosInimigos & inimigosRemanescentes <= 0) {
            morreramTodosInimigos = true;
            portaLateral.GetComponent<Door>().Destrancar();
        }

        if (!BossMorreu)
        {
            if (boss.morto)
            {
                BossMorreu = true;
                portaPrincipal.GetComponent<Door>().Destrancar();
            }
        }


        if (!sonsadicionais.isPlaying && fundoPausado)
        {
            RetomarFundo();
        }
    }

    public void PausarFundo()
    {
        if (somFundo.isPlaying)
        {
            somFundo.Pause();
            fundoPausado = true;
        }
    }

    public void TrocarSomFundo(AudioClip clip)
    {
        somFundo.clip = clip;   
        somFundo.Play();
    }

    public void RetomarFundo()
    {
        somFundo.UnPause();
        fundoPausado = false;
    }

    public void TocarSomAdicional(AudioClip clip)
    {
        sonsadicionais.PlayOneShot(clip);
    }

    public void InimigoMorreu()
    {
        this.inimigosRemanescentes--;
    }
}
