using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.StandaloneInputModule;


public class NewGame : MonoBehaviour, IDataPersistence
{

    private string playerName; 

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
        playerName = GameObject.Find("TextoNome").GetComponent<Text>().text;
        DataPersistanceManager.instance.saveGame();
        // TODO SALVAR
        SceneManager.LoadScene(4);
    }

    public void LoadData(GameData data)
    {
        //
    }

    public void SaveData(GameData data)
    {
        data.currentPlayerName = playerName;
    }
}
