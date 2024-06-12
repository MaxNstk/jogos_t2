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
}
