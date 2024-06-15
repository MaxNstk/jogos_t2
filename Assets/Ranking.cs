using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour, IDataPersistence
{
    
    public void LoadData(GameData data)
    {
        Text texto = GameObject.Find("TextoPlacar").GetComponent<Text>();

        texto.text = "Nome do jogador - Tempo decorrido"+"\n";

        for (int i=0;i<data.playerNames.Count; i++)
        {
            texto.text = texto.text + "\n" +
                $"{data.playerNames[i]} - {data.playertimes[i]}";
        }   
    }

    public void SaveData(GameData data)
    {
        //throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        DataPersistanceManager.instance.LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
