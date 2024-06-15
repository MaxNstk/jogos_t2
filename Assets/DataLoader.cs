using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour, IDataPersistence
{

    public Vector3 phase3InitialPosition;
    public Vector3 phase2InitialPosition;


    public void LoadData(GameData data)
    {
        if (!data.needsToBeContinued) { return; }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CharacterController characterController = player.GetComponent<CharacterController>();
        MovimentarPersonagem personagem = player.GetComponent<MovimentarPersonagem>();

        characterController.enabled = false;
        personagem.enabled = false;

        if (data.hasPassedPhase2)
        {
            Debug.Log("Começando da fase 3");

            GetComponent<PuzzleFase1>().PhaseCompleted();
            GetComponent<PuzzleFase2>().EndGame();

            player.transform.position = phase3InitialPosition;
            characterController.enabled = true;
            personagem.enabled = true;

            // GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().Move(phase3InitialPosition);
            return;
        }
        if (data.hasPassedPhase1)
        {
            Debug.Log("Começando da fase 2  ");
            GetComponent<PuzzleFase1>().PhaseCompleted();

            player.transform.position = phase2InitialPosition;
            characterController.enabled = true;
            personagem.enabled = true;

            //GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().Move(phase2InitialPosition);
            return;
        }   
    }

    public void SaveData(GameData data)
    {
        data.needsToBeContinued = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
