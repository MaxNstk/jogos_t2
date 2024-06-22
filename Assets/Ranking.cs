using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour, IDataPersistence
{
    
    public void LoadData(GameData data)
    {
        Text texto = GameObject.Find("TextoPlacar").GetComponent<Text>();

        texto.text = "Nome do jogador - Tempo decorrido" + "\n";

        // Combine playerNames and playertimes into a list of tuples
        List<(string playerName, float playerTime)> playerData = new List<(string, float)>();

        for (int i = 0; i < data.playerNames.Count; i++)
        {
            playerData.Add((data.playerNames[i], data.playertimes[i]));
        }

        // Sort the playerData list based on playerTime (ascending)
        playerData.Sort((a, b) => a.playerTime.CompareTo(b.playerTime));

        // Clear the text
        texto.text = "";

        // Generate the sorted text
        foreach (var player in playerData)
        {
            texto.text += $"{player.playerName} - {player.playerTime}\n";
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
