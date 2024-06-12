using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool hasReachedSavePoint1;
    public bool hasReachedSavePoint2;

    public List<PlayerScore> scoreList;
    public PlayerScore currentScore;

    public GameData()
    {
        hasReachedSavePoint1 = false;
        hasReachedSavePoint2 = false;
        scoreList = new List<PlayerScore>();
        currentScore = new PlayerScore();
    }
}
