using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleFase2 : MonoBehaviour
{
    public List<AudioClip> clips;
    public AudioClip errorClip;
    public AudioClip nextStageClip;
    public List<int> clipSequence;
    public List<int> currentPlayerSequence;

    public int currentPhase = 1;
    public int phasesAmount = 10;

    AudioSource src;

    void Start()
    {
        src = GetComponent<AudioSource>();
        ResetPhase();
    }

    void CreateNewClipSequence()
    {
        this.clipSequence = new List<int>();
        for (int i = 0; i < this.phasesAmount; i++)
        {
            int randomNumber = UnityEngine.Random.Range(0, clips.Count);
            Debug.Log($"Novo indice: {randomNumber}");
            clipSequence.Add(randomNumber);
        }
    }

    IEnumerator PlayCurrentPhaseSequence()
    {
        // plays the number of clips depending on the player phase
        for (int idx = 0; idx < this.currentPhase; idx++)
        {
            int clipIndex = clipSequence[idx];
            Debug.Log($"clipIndex: {clipIndex}");
            AudioClip clip = clips[clipIndex];
            yield return StartCoroutine(PlayClip(clip));
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
        StartCoroutine(HandleClipPlayed(clip));
    }

    IEnumerator HandleClipPlayed(AudioClip clip)
    {
        // toca o clip e adiciona a lista de tocados
        yield return StartCoroutine(PlayClip(clip));
        int playedIdx = GetClipIndex(clip);
        currentPlayerSequence.Add(playedIdx);

        // verifica se o é o correto clip, se não, restarta a fase
        if (!ClipIsRight())
        {
            yield return StartCoroutine(PlayClip(errorClip));
            ResetPhase();
            yield break;
        }

        // verifica se acabou a sequencia
        if (currentPlayerSequence.Count == this.currentPhase)
        {
            yield return StartCoroutine(WaitSeconds(1));
            yield return StartCoroutine(PlayClip(nextStageClip));
            this.currentPhase++;
            this.currentPlayerSequence = new List<int> {};
            yield return StartCoroutine(WaitSeconds(1));
            yield return StartCoroutine(PlayCurrentPhaseSequence());
        }
    }

    IEnumerator PlayClip(AudioClip clip)
    {
        src.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
    }

    IEnumerator WaitSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    void Update()
    {
    }
}
