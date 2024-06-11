using SunTemple;
using UnityEngine;
using UnityEngine.UI;


public class PuzzleFase1 : MonoBehaviour
{

    public Text score;
    public int totalButtons;
    public int foundButtons = 0;

    public GameObject portaAbrir; 

    private FindableButton[] buttons;

    void Start()
    {
        UpdateFindableObject();
        totalButtons = buttons.Length;
    }

    void Update()
    {
    
    }

    public void updateScore()
    {
        string text = $"Encontrados: {foundButtons}/{totalButtons}";
        Debug.Log(text);
        score.text = text;
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
