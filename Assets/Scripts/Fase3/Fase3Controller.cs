using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase3Controller : MonoBehaviour
{
    public AudioClip errorClip;
    public AudioClip succesClip;

    public List<AudioClip> clipSequence;
    public List<AudioClip> currentPlayerSequence;

    AudioSource src;

    int errorCount = 0;

    bool onGoing = false;

    void Update()
    {
    }

    void Start()
    {
        src = GetComponent<AudioSource>();
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
            yield return StartCoroutine(PlayClip(clip));
        }else {
            currentPlayerSequence.Add(clip);

            if (!ClipIsRight()) // se estiver errado remove
            {
                yield return StartCoroutine(PlayClip(errorClip));
                currentPlayerSequence.RemoveAt(currentPlayerSequence.Count-1);
                errorCount++;
            }
            else // Está certo
            {
                yield return StartCoroutine(PlayClip(succesClip));

                // Fechou o game
                if (currentPlayerSequence.Count == clipSequence.Count)
                {
                    Debug.Log("Parabéns acertasse tudo");
                    onGoing = false;
                }
            }
        }
    }

    public bool ClipIsRight()
    {
        // verifica a lista correta e o input de usuário
        return currentPlayerSequence[currentPlayerSequence.Count - 1] == clipSequence[currentPlayerSequence.Count - 1];
    }

    IEnumerator PlayClip(AudioClip clip)
    {
        src.clip = clip;
        src.Play();
        yield return new WaitForSeconds(clip.length);
    }

    IEnumerator WaitSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }



}
