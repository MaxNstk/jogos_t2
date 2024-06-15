using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private float startTime;
    private float endTime;
    private bool gameFinished = false;
    public float timeTaken;

    private Text timeText;

    public static GameController instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Record the start time when the game starts
        startTime = Time.time;
        timeText = GameObject.Find("TempoDecorrido").GetComponent<Text>();

    }

    void Update()
    {
        // Check if the game has finished
        if (!gameFinished)
        {
            // Update the time taken continuously until the game is finished
            timeTaken += Time.deltaTime;
            string text = $"Tempo decorrido: {timeTaken}";
            timeText.text = text;
        }
    }

    public void FinishGame()
    {
        // Record the end time when the game finishes
        endTime = Time.time;
        gameFinished = true;
        // Calculate the time taken to finish the game
        timeTaken = endTime - startTime;
        // You can use this timeTaken variable for whatever you need, like storing it or displaying it
        Debug.Log("Time taken: " + timeTaken);
    }
}
