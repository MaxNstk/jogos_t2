using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void ReiniciarJogo()
    {
        SceneManager.LoadScene(1);
    }

    public void CarregarJogo()
    {
        // TODO carregar save
    }

    public void Ranking()
    {
        // TODO RANKING
    }

    public void SairJogo()
    {
        Application.Quit();
    }
}
