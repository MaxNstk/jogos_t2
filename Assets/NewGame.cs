using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class NewGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartNewGame()
    {
        string name = GameObject.Find("TextoNome").GetComponent<Text>().text;
        Debug.Log("Nome do camarada: "+name);
        // TODO SALVAR
        SceneManager.LoadScene(4);
    }
}
