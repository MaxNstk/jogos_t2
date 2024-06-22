using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour, IDataPersistence
{
    public bool continueGame = false;
    public bool hasSomeonePlaying = false;
    public void ReiniciarJogo()
    {
        SceneManager.LoadScene(1);
    }

    public void CarregarJogo()
    {
        DataPersistanceManager.instance.LoadGame();

        if (!hasSomeonePlaying) { return; }

        continueGame = true;
        DataPersistanceManager.instance.saveGame();
        SceneManager.LoadScene(4);
    }

    public void Ranking()
    {
        SceneManager.LoadScene(2);
    }

    public void Sobre()
    {
        SceneManager.LoadScene(3);
    }

    public void SairJogo()
    {
        Application.Quit();
    }

    public void LoadData(GameData data)
    {
        hasSomeonePlaying = data.currentPlayerName != "";
    }

    public void SaveData(GameData data)
    {
        data.needsToBeContinued = continueGame;
    }
}
