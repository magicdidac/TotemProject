using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseCounter : MonoBehaviour
{
    [SerializeField] private Toggle[] toggles = null;

    public void TurnOffAll() {
        toggles[0].isOn = true;
        for (int i = 1; i < toggles.Length; i++)
        {
            toggles[i].isOn = false;
        }
    }

    public bool TurnOnNext()
    {
        foreach(Toggle t in toggles)
        {
            if (!t.isOn)
            {
                t.isOn = true;
                return false;
            }
        }

        //All phases completed

        return true;

    }

}
