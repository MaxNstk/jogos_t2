using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;
using System;

public class DataPersistanceManager : MonoBehaviour
{

    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private FileDataHandler dataHandler;

    private GameData gameDAta;
    public static DataPersistanceManager Instance {  get; private set; }

    private List<IDataPersistence> dataPersistenceObjects;

    private void Awake()
    {
        Instance = this; 
    }

    public void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = findAllDataPersistenceObjects();
        LoadGame();
    }

    private List<IDataPersistence> findAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public void NewGame()
    {
        this.gameDAta = new GameData();
    }

    public void LoadGame()
    {
        this.gameDAta = dataHandler.Load();

        if (this.gameDAta == null) {
            Debug.Log("iNITIALIZING DATA TO DEFAULTS");
            NewGame();
        }

        foreach (IDataPersistence dataPersistence in dataPersistenceObjects)
        {
            dataPersistence.LoadData(gameDAta);
        }
    }

    public void saveGame()
    {
        foreach (IDataPersistence dataPersistence in dataPersistenceObjects)
        {
            dataPersistence.SaveData(ref gameDAta);
        }
        dataHandler.Save(gameDAta);
    }
    public void onGameExit()
    {
        saveGame();
    }

    public void endGame() {
        // Todo create end game
    }
}
