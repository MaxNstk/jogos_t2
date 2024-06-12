using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataPersistanceManager : MonoBehaviour
{

    private GameData gameData;
    public static DataPersistanceManager instance {  get; private set; }

    private void Awake()
    {
        instance = this; 
    }

    public void Start()
    {
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        if (this.gameData == null) {
            Debug.Log("iNITIALIZING DATA TO DEFAULTS");
            NewGame();
        }
    }

    public void saveGame()
    {
            
    }
    public void onApplicationQuit()
    {
        saveGame();
    }

    public void endGame() {
        // Todo create end game
    }
}
