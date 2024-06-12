using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool hasPassedPhase1;
    public bool hasPassedPhase2;

    public string currentPlayerName;
    public float currentPlayerTime;

    public List<string> playerNames;
    public List<float> playertimes;


    public GameData()
    {
        hasPassedPhase1 = false;
        hasPassedPhase2 = false;

        currentPlayerName = "";
        currentPlayerTime = 0;

        playertimes = new List<float>();
        playerNames = new List<string>();

    }
}
