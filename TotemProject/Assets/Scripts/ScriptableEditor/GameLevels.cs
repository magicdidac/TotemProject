using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameLevels
{
    [HideInInspector] private int maxPhases = 5;
    [SerializeField] public List<Phase> phases = new List<Phase>();

    public void AddPhase()
    {
        if (phases.Count < maxPhases)
            phases.Add(new Phase());
        else
            Debug.LogWarning("You cannot add more phases, the maximum size is "+ maxPhases);
    }

    public void RemovePhase()
    {
        if (phases.Count <= 1)
        {
            Debug.LogWarning("The list must have length of at least one");
            return;
        }

        phases.RemoveAt(phases.Count - 1);
    }

}
