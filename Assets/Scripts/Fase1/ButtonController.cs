using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    void Start()
    {
        UpdateFindableObject();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateFindableObject()
    {
        {
            // Find all active instances of the script YourScriptName
            FindableButton[] buttons = FindObjectsOfType<FindableButton>();

            // Loop through the array and do something with each object
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
