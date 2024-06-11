using SunTemple;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleFase2 : MonoBehaviour
{
    public List<AudioClip> clips;
    public AudioClip nextStageClip;
    public List<int> clipSequence;
    public List<int> currentPlayerSequence;

    public int currentPhase = 1;
    public int phasesAmount = 10;

    AudioManagerScript am;

    bool onGoing = false;
    bool gameEnd = false;

    public GameObject endPhaseDoor;


    void Start()
    {
       am = FindObjectOfType<AudioManagerScript>();
    }

    public void StartGame()
    {
        if (onGoing || gameEnd) { return; }
        onGoing = true;
        ResetPhase();
    }

    void CreateNewClipSequence()
    {
        this.clipSequence = new List<int>();
        for (int i = 0; i < this.phasesAmount; i++)
        {
            int randomNumber = UnityEngine.Random.Range(0, clips.Count);
            clipSequence.Add(randomNumber);
        }
    }

    IEnumerator PlayCurrentPhaseSequence()
    {
        // plays the number of clips depending on the player phase
        for (int idx = 0; idx < this.currentPhase; idx++)
        {
            int clipIndex = clipSequence[idx];
            AudioClip clip = clips[clipIndex];
            yield return StartCoroutine(am.PlayClipWaiting(clip));
            yield return new WaitForSeconds(clip.length);
        }
    }

    public int GetClipIndex(AudioClip clip)
    {
        return clips.IndexOf(clip);
    }

    public bool ClipIsRight()
    {
        // verifica a lista correta e o input de usuário
        return currentPlayerSequence[currentPlayerSequence.Count - 1] == clipSequence[currentPlayerSequence.Count - 1];
    }

    public void ResetPhase()
    {
        this.currentPhase = 1;
        this.currentPlayerSequence = new List<int> {};
        CreateNewClipSequence();
        StartCoroutine(PlayCurrentPhaseSequence());
    }

    internal void clipPlayed(AudioClip clip)
    {
        if (!this.onGoing) { return; }
        StartCoroutine(HandleClipPlayed(clip));
    }

    IEnumerator HandleClipPlayed(AudioClip clip)
    {
        // toca o clip e adiciona a lista de tocados
        yield return StartCoroutine(am.PlayClipWaiting(clip));
        int playedIdx = GetClipIndex(clip);
        currentPlayerSequence.Add(playedIdx);

        // verifica se o é o correto clip, se não, restarta a fase
        if (!ClipIsRight())
        {
            yield return StartCoroutine(am.PlayClipWaiting(am.failClip));
            ResetPhase();
            yield break;
        }

        // verifica se acabou a sequencia
        if (currentPlayerSequence.Count == this.currentPhase)
        {
            if (this.currentPhase == this.phasesAmount)
            {
                EndGame();
                yield break;
            }
            yield return StartCoroutine(WaitSeconds(1));
            yield return StartCoroutine(am.PlayClipWaiting(nextStageClip));
            this.currentPhase++;            
            this.currentPlayerSequence = new List<int> {};
            yield return StartCoroutine(WaitSeconds(1));
            yield return StartCoroutine(PlayCurrentPhaseSequence());
        }
    }

    void EndGame()
    {
        endPhaseDoor.GetComponent<Door>().Destrancar();
        AudioManagerScript am = FindObjectOfType<AudioManagerScript>();
        am.PlayClip(am.successClip);
        this.onGoing = false;
        gameEnd = true;

    }

    IEnumerator WaitSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    void Update()
    {
    }
}
