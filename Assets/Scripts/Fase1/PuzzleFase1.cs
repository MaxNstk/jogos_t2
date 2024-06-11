using SunTemple;
using UnityEngine;
using UnityEngine.UI;


public class PuzzleFase1 : MonoBehaviour
{

    public int totalButtons;
    public int foundButtons = 0;

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

            foreach (FindableButton button in buttons)
            {
                if (!button.wasFound) {
                    button.setActive();
                    return;
                }
            }
            portaAbrir.GetComponent<Door>().Destrancar();
            AudioManagerScript am = FindObjectOfType<AudioManagerScript>();
            am.PlayClip(am.successClip);

        }


    }

}
