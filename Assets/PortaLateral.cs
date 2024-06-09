using SunTemple;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaPrincipal : MonoBehaviour
{
    Door porta;
    bool portaFechada = true;

    void Start()
    {
        porta = GetComponent<Door>();
    }

    // Update is called once per frame
    void Update()
    {
        if (portaFechada)
        {
            if (!porta.DoorClosed) {
                portaFechada=false;
                TerrenoController tc = FindObjectOfType<TerrenoController>();
                tc.TrocarSomFundo(tc.audioFora);
            }
        }
    }
}
