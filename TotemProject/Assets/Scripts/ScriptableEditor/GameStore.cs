using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameStore
{
    [SerializeField] public List<GameLevels> gameLevels = new List<GameLevels>();

    public void AddLevel()
    {
        gameLevels.Add(new GameLevels());
    }

    public void RemoveLevel()
    {
        if (gameLevels.Count == 0)
        {
            Debug.LogWarning("Trying to remove an element on empty list");
            return;
        }

        gameLevels.RemoveAt(gameLevels.Count-1);
    }

}
