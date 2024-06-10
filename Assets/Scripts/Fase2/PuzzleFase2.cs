using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleFase2 : MonoBehaviour
{
    public List<AudioClip> clips;
    public AudioClip errorClip;
    public AudioClip nextStageClip;
    public List<int> clipSequence = new List<int> { 0, 3, 0, 1, 2, 2, 1, 0, 2, 3 };
    public List<int> currentPlayerSequence = new List<int> { };
    public int currentPhase = 1;

    AudioSource src;

    void Start()
    {
        src = GetComponent<AudioSource>();
        StartCoroutine(PlayCurrentPhaseSequence());
    }

    IEnumerator PlayCurrentPhaseSequence()
    {
        for (int idx = 0; idx < this.currentPhase; idx++)
        {
            int clipIndex = clipSequence[idx];
            src.clip = clips[clipIndex];
            src.Play();
            yield return new WaitForSeconds(src.clip.length);
        }
    }

    public int GetClipIndex(AudioClip clip)
    {
        return clips.IndexOf(clip);
    }

    public bool ClipIsRight()
    {
        // Checa se o clip adicionado é o correto
        return currentPlayerSequence[currentPlayerSequence.Count - 1] == clipSequence[currentPlayerSequence.Count - 1];
    }

    public void ResetPhase()
    {
        this.currentPhase = 1;
        this.currentPlayerSequence = new List<int> { };
    }

    internal void clipPlayed(AudioClip clip)
    {
        StartCoroutine(HandleClipPlayed(clip));
    }

    IEnumerator HandleClipPlayed(AudioClip clip)
    {
        yield return StartCoroutine(PlayClip(clip));

        int playedIdx = GetClipIndex(clip);
        currentPlayerSequence.Add(playedIdx);

        // verifica o clip
        if (!ClipIsRight())
        {
            yield return StartCoroutine(WaitSeconds(1));
            Debug.Log($"Clip is wrong");
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
            this.currentPlayerSequence = new List<int> { };
            yield return StartCoroutine(WaitSeconds(1));
            yield return StartCoroutine(PlayCurrentPhaseSequence());
        }
    }

    IEnumerator PlayClip(AudioClip clip)
    {
        Debug.Log("Playing a clip right now");
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
