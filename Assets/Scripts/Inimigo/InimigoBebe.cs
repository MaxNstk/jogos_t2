using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InimigoBebe : MonoBehaviour, ILevarDano
{
    private NavMeshAgent agente;
    private GameObject player;
    private Animator anim;
    public float distanciaDoAtaque = 2.0f;

    public int vida = 30;
    public int dano = 5;

    public AudioClip somMorte;
    public AudioClip somPasso;

    private AudioSource audioSrc;

    private FieldOfView fov;
    private PatrulharAleatorio pal;

    public bool morto = false;


    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();

        fov = GetComponent<FieldOfView>();
        pal = GetComponent<PatrulharAleatorio>();
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
        GetComponent<Reproducao>().enabled = false;

        agente.isStopped = true;
        anim.SetBool("PodeAndar", false);
        anim.SetBool("PararAtaque", true);
        anim.SetBool("Morreu", true);
        morto = true;
        FindObjectOfType<MovimentarPersonagem>().AdicionarPontuacao(1);
        GetComponent<CapsuleCollider>().enabled = false;
        fov.enabled = false;
        this.enabled = false;
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

