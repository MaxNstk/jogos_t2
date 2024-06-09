using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcoesObjeto : MonoBehaviour
{
    private IdentificarObjeto idObjetos;
    private bool pegou = false;


    // Start is called before the first frame update
    void Start()
    {
        idObjetos = GetComponent<IdentificarObjeto>();    
    }

    private void Pegar()
    {
        IPegavel obj = idObjetos.GetObjPegar().GetComponent<IPegavel>();
        obj.Pegar();

        Destroy(idObjetos.GetObjPegar());
        idObjetos.EsconderTexto();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && idObjetos.GetObjPegar() != null)
        {
            Pegar();
        }
        
        if (Input.GetKeyDown(KeyCode.F) && idObjetos.GetObjArrastar() != null)
        {
            if (!pegou)
            {
                Arrastar();
            }
            else
            {
                Soltar();
            }
            pegou = !pegou;
        }
    }

    private void Arrastar()
    {
        GameObject obj = idObjetos.GetObjArrastar();
        obj.AddComponent<DragDrop>();
        obj.GetComponent<DragDrop>().Ativar();
        idObjetos.EsconderTexto();
        idObjetos.enabled = false;
    }

    private void Soltar()
    {
        GameObject obj = idObjetos.GetObjArrastar();
        Destroy(obj.GetComponent<DragDrop>());
        idObjetos.EsconderTexto();
        idObjetos.enabled = true;
    }
}
