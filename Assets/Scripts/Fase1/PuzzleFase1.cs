using SunTemple;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class PuzzleFase1 : MonoBehaviour, IDataPersistence
{

    public int totalButtons;
    public int foundButtons = 0;

    private bool phaseCompleted = false;

    public GameObject portaAbrir;

    private Image imagemVisao;
    private FindableButton[] buttons;

    void Start()
    {
        GameObject painelVisao = GameObject.FindGameObjectWithTag("Visao");
        if (painelVisao != null)
        {
            imagemVisao = painelVisao.GetComponent<Image>();
        }

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
            DataPersistanceManager.instance.saveGame();

        }
    }

    public void PhaseCompleted()
    {
        phaseCompleted = true;
        foundButtons = FindObjectsOfType<FindableButton>().Count();

        foreach (FindableButton button in FindObjectsOfType<FindableButton>())
        {
            button.Kill();
        }
        if (imagemVisao != null)
        {
            Color color = imagemVisao.color;
            color.a = 0f;
            imagemVisao.color = color;
        }

        portaAbrir.GetComponent<Door>().Destrancar();
        try
        {
            AudioManagerScript am = FindObjectOfType<AudioManagerScript>();
            am.PlayClip(am.successClip);
        }
        catch
        {

        }
    }

    public void LoadData(GameData data)
    {
       //TODO fase 1 completa loadar a partir da fase 2
    }

    public void SaveData(GameData data)
    {
        if (data.hasPassedPhase1) { return; }
        data.hasPassedPhase1 = phaseCompleted;
        data.currentPlayerTime = GameController.instance.timeTaken;
    }
}
