using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase3Controller : MonoBehaviour
{
    public AudioClip succesClip;

    public List<AudioClip> clipSequence;
    public List<AudioClip> currentPlayerSequence;


    AudioManagerScript am;


    int errorCount = 0;
    int errorLimit = 3;

    bool onGoing = false;

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
        }
    }

    private void EndGame()
    {
        am.PlayClipWaiting(succesClip);
        Debug.Log("Parabéns acertasse tudo");
        onGoing = false;
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

}
