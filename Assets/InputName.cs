using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputName : MonoBehaviour, IDataPersistence
{
    public void LoadData(GameData data)
    {
        //
    }

    public void SaveData(ref GameData data)
    {
        data.currentScore = new PlayerScore();
        data.currentScore.name = "Max";
        data.currentScore.score = 0;
    }

    // 0 StartGame
    // 1 InformarNome
    // 2 Ranking
    //3 Jogo
    // EndGame


    public void StartGame()
    {
        SceneManager.LoadScene(3);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
