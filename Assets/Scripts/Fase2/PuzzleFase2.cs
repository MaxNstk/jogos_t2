using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleFase2 : MonoBehaviour
{
    public List<AudioClip> clips;

    public AudioClip errorClip;
    public AudioClip nextStageClip;

    public List<int> clipSequence = new List<int> {0, 3, 0, 1, 2, 2, 1, 0, 2, 3};

    public List<int> currentPlayerSequence = new List<int> {};

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
            Debug.Log($"Tocando o clip {clipIndex}");
            src.clip = clips[clipIndex];
            src.Play();
            Debug.Log($"Esperando por {src.clip.length}");
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

        int playedIdx = GetClipIndex(clip);
        currentPlayerSequence.Add(playedIdx);

        Debug.Log($"Clip played: {clip}");

        // verifica o clip
        if (!ClipIsRight())
        {
            Debug.Log($"Clip is wrong");
            StartCoroutine(PlayClip(errorClip));
            ResetPhase();
            return;
        }
        Debug.Log($"Clip is right");

        // verifica se acabou a sequencia
        if (currentPlayerSequence.Count == this.currentPhase)
        {
            Debug.Log($"Going to the next stage");
            StartCoroutine(PlayClip(clip));
            this.currentPhase++;
            this.currentPlayerSequence = new List<int> { };
            StartCoroutine(PlayCurrentPhaseSequence());
        }

    }

    IEnumerator PlayClip(AudioClip clip)
    {
        src.clip = clip;
        src.Play();
        yield return new WaitForSeconds(clip.length);
    }

    void Update()
    {

    }

}
