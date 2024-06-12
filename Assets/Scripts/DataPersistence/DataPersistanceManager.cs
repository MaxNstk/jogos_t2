using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataPersistanceManager : MonoBehaviour
{

    private GameData gameDAta;
    public static DataPersistanceManager Instance {  get; private set; }

    private void Awake()
    {
        Instance = this; 
    }

    public void Start()
    {
        LoadGame();
    }

    public void NewGame()
    {
        this.gameDAta = new GameData();
    }

    public void LoadGame()
    {
        if (this.gameDAta == null) {
            Debug.Log("iNITIALIZING DATA TO DEFAULTS");
            NewGame();
        }
    }

    public void saveGame()
    {
            
    }
    public void onGameExit()
    {
        saveGame();
    }

    public void endGame() {
        // Todo create end game
    }
}
