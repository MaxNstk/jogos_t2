using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss : MonoBehaviour, ILevarDano
{
    private NavMeshAgent agente;
    private GameObject player;
    private Animator anim;
    public float distanciaDoAtaque = 2.0f;

    public int vida = 50;
    public int dano = 10;

    public AudioClip somMorte;
    public AudioClip somPasso;

    public AudioClip somGrunhido;

    public AudioSource audioSrc;
    public AudioSource audioSrcSomFundo;

    private FieldOfView fov;
    private PatrulharAleatorio pal;

    public bool morto = false;

    public bool viuPersonagem = false;

    private TerrenoController tc;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();

        fov = GetComponent<FieldOfView>();
        pal = GetComponent<PatrulharAleatorio>();
        tc = FindObjectOfType<Terrain>().GetComponent<TerrenoController>();

    }

    void Update()
    {

        if (vida <= 0)
        {
            Morrer();
            return;
        }

        if (fov.podeVerPlayer)
        {
            if (!viuPersonagem)
            {
                viuPersonagem = true;
                tc.TrocarSomFundo(tc.audioBoss);
                audioSrc.clip = somGrunhido;
                audioSrc.Play();
            }
            VaiAtrasJogador();
        }
        else
        {
            anim.SetBool("PararAtaque", true);
            CorrigirRigiSair();
            agente.isStopped = false;
            pal.Andar();
        }

    }

    private void Morrer()
    {
        audioSrc.clip = somMorte;
        audioSrc.Play();
        anim.SetBool("Morreu", true);
        morto = true;
        audioSrcSomFundo.Stop();
        FindObjectOfType<MovimentarPersonagem>().AdicionarPontuacao(30);
        GetComponent<CapsuleCollider>().enabled = false;
        fov.enabled = false;
        this.enabled = false;
        tc.TrocarSomFundo(tc.audioSalaInicial);
        
    }

    private void VaiAtrasJogador()
    {
        float distanciaDoPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanciaDoPlayer < distanciaDoAtaque)
        {
            agente.isStopped = true;
            Debug.Log("Ataque");

            anim.SetTrigger("Ataque");
            anim.SetBool("PodeAndar", false);
            anim.SetBool("PararAtaque", false);
            CorrigirRigiEntrar();
        }
        if (distanciaDoPlayer >= distanciaDoAtaque + 1)
        {
            anim.SetBool("PararAtaque", true);
            CorrigirRigiSair();
        }
        if (anim.GetBool("PodeAndar"))
        {
            agente.isStopped = false;
            agente.SetDestination(player.transform.position);
            anim.ResetTrigger("Ataque");
        }
    }

    private void CorrigirRigiEntrar()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }
    private void CorrigirRigiSair()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public void LevarDano(int dano)
    {
        vida -= dano;
        agente.isStopped = true;
        anim.SetTrigger("LevouTiro");
        anim.SetBool("PodeAndar", false);
    }

    public void DarDano()
    {
        player.GetComponent<MovimentarPersonagem>().AtualizarVida(-dano);
    }

    public void Passo()
    {
        audioSrc.PlayOneShot(somPasso, 0.05f);
    }
}

