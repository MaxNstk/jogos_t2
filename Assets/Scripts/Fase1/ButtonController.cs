using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonController : MonoBehaviour
{

    public Text score;
    public int totalButtons;
    public int foundButtons = 0;

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
            Debug.Log("TODO ABRIR PORTA. Acabaram-se todos botões");
        }


    }

}
