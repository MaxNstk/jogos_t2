using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class InimigoComum : MonoBehaviour, ILevarDano
{
    private NavMeshAgent agente;
    private GameObject player;
    private Animator anim;
    public float distanciaDoAtaque = 2.0f;

    public int vida = 50;
    public int dano = 10;

    public AudioClip somMorte;
    public AudioClip somPasso;
    public AudioClip somReproducao;

    private AudioSource audioSrc;

    private FieldOfView fov;
    private PatrulharAleatorio pal;

    public bool morto = false;

    private Reproducao reproducao;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();

        fov = GetComponent<FieldOfView>();
        pal = GetComponent<PatrulharAleatorio>();

        reproducao = GetComponent<Reproducao>();
    }

    void Update()
    {

        if (reproducao.timer >= reproducao.tempoParaReproduzir & !morto)
        {
            reproducao.timer = 0f;
            StartCoroutine(Reproduzir(5));
        }

        if (vida <= 0)
        {
            Morrer();
            return;
        }

        if (!agente.isStopped)
        {
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
    }

    IEnumerator Reproduzir(int time)
    {
        // anim.SetBool("PondoOvo", true);
        Parar();
        audioSrc.clip = somReproducao;
        yield return new WaitForSeconds(time);
        reproducao.timer = 0f;
        audioSrc.Play();
        reproducao.Reproduzir();
        Continuar();
        // anim.SetBool("PondoOvo", false);
    }
        
    private void Parar()
    {
        agente.isStopped = true;
        anim.SetBool("PodeAndar", false);
        anim.SetBool("PararAtaque", true);
    }

    private void Continuar()
    {
        agente.isStopped = false;
        anim.SetBool("PodeAndar", true);
        anim.SetBool("PararAtaque", false);
    }

    private void Morrer()
    {
        audioSrc.clip = somMorte;
        audioSrc.Play();
        Parar();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().rotation = Quaternion.identity;

        anim.SetBool("Morreu", true);
        morto = true;
        FindObjectOfType<MovimentarPersonagem>().AdicionarPontuacao(10);
        FindObjectOfType<TerrenoController>().InimigoMorreu();

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

