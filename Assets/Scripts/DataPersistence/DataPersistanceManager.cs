using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]

    [SerializeField] private string fileName = "data.json";

    public static DataPersistanceManager instance {  get; private set; }
    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;


    private void Awake()
    {
        instance = this;
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if (this.gameData == null) {
            Debug.Log("iNITIALIZING DATA TO DEFAULTS");
            NewGame();
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void saveGame()
    {
        if (this.gameData == null)
        {
            LoadGame();
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }
        dataHandler.Save(gameData);
    }

    public void endGame() {
        // Todo create end game
    }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        // FindObjectsofType takes in an optional boolean to include inactive gameobjects
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
