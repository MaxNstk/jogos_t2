using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour, IDataPersistence
{

    // 0 StartGame
    // 1 InformarNome
    // 2 Ranking
    // Jogo
    // EndGame

    public void ReiniciarJogo()
    {
        SceneManager.LoadScene(1);
    }

    public void CarregarJogo()
    {
        SceneManager.LoadScene(0);
    }

    public void Ranking()
    {
        SceneManager.LoadScene(2);
    }

    public void SairJogo()
    {
        Application.Quit();
    }

    public void LoadData(GameData data)
    {
        Debug.Log("loading data");
    }

    public void SaveData(ref GameData data)
    {
        Debug.Log("saving data");
    }
}
