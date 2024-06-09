using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MovimentarPersonagem : MonoBehaviour
{
    public CharacterController controle;
    public float velocidade = 6f;
    public float alturaPulo = 6f;
    public float gravidade = -20f;

    public AudioClip somPulo;
    public AudioSource audioSrc;

    public Transform chechaChao;
    public float raioEsfera = 0.4f;
    public LayerMask chaoMask;
    public bool estaNoChao;

    Vector3 velocidadeCai;

    private Transform cameraTransform;
    private bool estahAbaixado = false;
    private bool levantarBloqueado;
    public float alturaLevantado, alturaAbaixado, posicaoCameraEmPe, posicaoCameraAbaixado;

    public int vida = 100;
    public Slider sliderVida;
    private int pontos = 0;
    public AudioSource passos;

    public Text pontuacao;
    

    void Start()
    {
        controle = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (vida <= 0)
        {
            FimDeJogo();
            return;
        }
           
        estaNoChao = Physics.CheckSphere(chechaChao.position, raioEsfera, chaoMask);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 mover = transform.right * x + transform.forward * z;
        controle.Move(mover * velocidade * Time.deltaTime);

        ChecarBloqueioAbaixado();

        if (!levantarBloqueado && estaNoChao && Input.GetButtonDown("Jump"))
        {
            velocidadeCai.y = Mathf.Sqrt(alturaPulo * -2f * gravidade);
            audioSrc.clip = somPulo;
            audioSrc.Play();
        }

        if (!estaNoChao) {
            velocidadeCai.y += gravidade * Time.deltaTime;
        }
        controle.Move(velocidadeCai * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            AgacharLevantar();
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            passos.enabled = true;
        }
        else
        {
            passos.enabled = false;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(chechaChao.position, raioEsfera);
    }

    private void AgacharLevantar()
    {
        if (levantarBloqueado || estaNoChao == false )
        { return; }
        estahAbaixado = !estahAbaixado;
        if (estahAbaixado)
        {
            controle.height = alturaAbaixado;
            cameraTransform.localPosition = new Vector3(0, posicaoCameraAbaixado, 0);
        } else
        {
            controle.height = alturaLevantado;
            cameraTransform.localPosition = new Vector3(0, posicaoCameraEmPe, 0);
        }
    }

    private void ChecarBloqueioAbaixado()
    {
        //Debug.DrawRay(cameraTransform.position, Vector3.up * 1.1f, Color.red);
        RaycastHit hit;
        levantarBloqueado = Physics.Raycast(cameraTransform.position, Vector3.up, out hit, 1.1f);
    }

    public void AtualizarVida(int novaVida)
    {
        vida = Mathf.CeilToInt(Mathf.Clamp(vida + novaVida, 0, 100));
        sliderVida.value = vida;
    }

    private void FimDeJogo()
    {
        //Time.timeScale = 0;

        //Camera.main.GetComponent<AudioListener>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);

        //telaFimJogo.SetActive(true);
        //estahVivo = false;
    }

    public void AdicionarPontuacao(int pontos)
    {
        this.pontos += pontos;
        pontuacao.text = "Pontuação: "+(this.pontos).ToString();
    }

}
