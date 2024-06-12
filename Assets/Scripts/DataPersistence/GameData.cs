using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool hasReachedSavePoint;

    public GameData()
    {
        hasReachedSavePoint = false;
    }
}