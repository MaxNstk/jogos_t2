using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fase3Controller : MonoBehaviour, IDataPersistence
{
    public AudioClip succesClip;

    public List<AudioClip> clipSequence;
    public List<AudioClip> currentPlayerSequence;


    AudioManagerScript am;

    int errorCount = 0;
    int errorLimit = 3;

    bool onGoing = false;
    bool gameEnd = false;

    void Update()
    {
    }

    void Start()
    {
        am = FindObjectOfType<AudioManagerScript>();
    }

    public void StartGame()
    {
        if (onGoing) { return; }
        currentPlayerSequence = new List<AudioClip>() {};
        onGoing = true;
        am.Stop();
        updateScore();
    }

    internal void clipPlayed(AudioClip clip)
    {
        StartCoroutine(HandleClipPlayed(clip));
    }

    IEnumerator HandleClipPlayed(AudioClip clip)
    {
        if (!onGoing)
        {
            yield return StartCoroutine(am.PlayClipWaiting(clip));
        }else {
            if (am.IsPlaying()) { 
                yield break; 
            }
            currentPlayerSequence.Add(clip);

            if (!ClipIsRight()) // se estiver errado remove
            {
                yield return StartCoroutine(am.PlayClipWaiting(am.failClip));
                currentPlayerSequence.RemoveAt(currentPlayerSequence.Count-1);
                errorCount++;
                if (errorCount == errorLimit) {
                    GoToCheckPoint();
                }
            }
            else // Está certo
            {
                yield return StartCoroutine(am.PlayClipWaiting(succesClip));

                // Fechou o game
                if (currentPlayerSequence.Count == clipSequence.Count)
                {
                    EndGame();
                }
            }
            updateScore();
        }
    }

    void updateScore()
    {
        string text = $"Ordenados: {currentPlayerSequence.Count}/{clipSequence.Count}";
        GameObject.Find("Pontuacao").GetComponent<Text>().text = text;
    }

    private void EndGame()
    {
        gameEnd = true;
        am.PlayClipWaiting(succesClip);
        Debug.Log("Parabéns acertasse tudo");
        onGoing = false;

        DataPersistanceManager.instance.saveGame();

        SceneManager.LoadScene(5);

    }

    private void GoToCheckPoint()
    {
        Debug.Log("Voltar para o checkpoint");
    }

    public bool ClipIsRight()
    {
        // verifica a lista correta e o input de usuário
        return currentPlayerSequence[currentPlayerSequence.Count - 1] == clipSequence[currentPlayerSequence.Count - 1];
    }

    public void LoadData(GameData data)
    {
        //todo
    }

    public void SaveData(GameData data)
    {
        //if (!data.hasPassedPhase2) { return; }

        data.currentPlayerTime = GameController.instance.timeTaken;
        if (gameEnd)
        {
            data.playerNames.Add(data.currentPlayerName);
            data.playertimes.Add(data.currentPlayerTime);
            data.currentPlayerTime = 0;
            data.currentPlayerName = null;
        }
    }
}
