using SunTemple;
using UnityEngine;
using UnityEngine.UI;


public class PuzzleFase1 : MonoBehaviour, IDataPersistence
{

    public int totalButtons;
    public int foundButtons = 0;

    private bool phaseCompleted = false;

    public GameObject portaAbrir; 

    private FindableButton[] buttons;

    void Start()
    {
        buttons = FindObjectsOfType<FindableButton>();
        totalButtons = buttons.Length;
        UpdateFindableObject();
    }

    void Update()
    {
    
    }

    public void updateScore()
    {
        string text = $"Encontrados: {foundButtons}/{totalButtons}";
        GameObject.Find("Pontuacao").GetComponent<Text>().text = text;
    }

    public void UpdateFindableObject()
    {
        {
            buttons = FindObjectsOfType<FindableButton>();
            updateScore();

            DataPersistanceManager.instance.saveGame();

            foreach (FindableButton button in buttons)
            {
                if (!button.wasFound) {
                    button.setActive();
                    return;
                }
            }
            PhaseCompleted();
        }
    }

    public void PhaseCompleted()
    {
        phaseCompleted = true;

        portaAbrir.GetComponent<Door>().Destrancar();
        AudioManagerScript am = FindObjectOfType<AudioManagerScript>();
        am.PlayClip(am.successClip);

        DataPersistanceManager.instance.saveGame();
    }
    public void LoadData(GameData data)
    {
       //TODO fase 1 completa loadar a partir da fase 2
    }

    public void SaveData(GameData data)
    {
        data.hasPassedPhase1 = phaseCompleted;
        data.currentPlayerTime = GameController.instance.timeTaken;
    }
}
