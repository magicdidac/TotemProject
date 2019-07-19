using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator
{

    private readonly static int[, ,] numbers = new int[,,]
    {
        { {100,-1}, {-1,-1}, {-1,-1}, {-1,-1}, {-1,-1} }, // Bonus Level
        { {10,15}, {12,18}, {-1,-1}, {-1,-1}, {-1,-1} }, // Level 1
        { {15,20}, {15,22}, {20,22}, {-1,-1}, {-1,-1} }, // Level 2
        { {10,15}, {15,20}, {15,20}, {20,25}, {-1,-1} }, // Level 3
        { {10,15}, {15,20}, {19,23}, {23,30}, {30,40} }, // Level 4
    };

    public static bool isBonus { get { return ((PlayerPrefs.GetInt("Level", 1) % 5) == 0); } private set { } }

    public static List<int> Generate()
    {
        int level = PlayerPrefs.GetInt("Level", 1)%5;
        List<int> phases = new List<int>();

        Debug.Log("Level: " + level + " (" + PlayerPrefs.GetInt("Level", 1) + ")");

        if (!isBonus)
        {
            for(int i = 0; i < (level+1); i++)
            {
                int[] range = { numbers[level, i, 0], numbers[level, i, 1] };
                phases.Add(Random.Range(range[0], range[1] + 1));
            }
        }
        else // BONUS LEVEL
        {
            phases.Add(numbers[level, 0, 0]);
        }

        return phases;

    }

}
