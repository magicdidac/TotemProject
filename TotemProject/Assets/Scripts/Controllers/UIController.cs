using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private Text levelText, butterfliesText;

    [SerializeField] private Animator fade, generalUI;

    public void DrawLevelNumber(int number)
    {
        levelText.text = "LEVEL " +Format(number+1);
    }

    public void DrawButterfliesNumber(int number)
    {
        butterfliesText.text = Format(number);
    }

    public void ChangeFade()
    {
        fade.SetTrigger("Change");
    }

    public void ChangeGeneralUI()
    {
        generalUI.SetTrigger("Change");
    }


    private string Format(int i)
    {
        if (i < 10)
            return string.Format("00{0}", i);
        if (i < 100)
            return string.Format("0{0}", i);

        return ""+i;
    }

}
