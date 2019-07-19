using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private Text levelText = null, butterfliesText = null;

    [SerializeField] private PhaseCounter[] allPhaseCounters = null;

    [SerializeField] private Animator fade = null, generalUI = null;

    [HideInInspector] private PhaseCounter phaseCounter;

    [SerializeField] private Image timer = null;

    //WIN MENU

    [SerializeField] private Image winBK = null;
    [SerializeField] private Text winText = null;
    [SerializeField] private Image winButtonBK = null;
    [SerializeField] private Text winButtonText = null;
    [SerializeField] private GameObject winReward = null;
    [SerializeField] private Text winRewardText = null;


    public void DrawLevelNumber()
    {
        levelText.text = "LEVEL " +Format(PlayerPrefs.GetInt("Level", 1));
    }

    public void DrawButterfliesNumber(int number)
    {
        butterfliesText.text = Format(number);
    }

    public void SelectPhaseCounter(int n)
    {
        phaseCounter = allPhaseCounters[n - 1];
        phaseCounter.gameObject.SetActive(true);
        phaseCounter.TurnOffAll();
    }

    public bool NextPhaseCounter()
    {
        return phaseCounter.TurnOnNext();
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

    public void StartTimer(float time)
    {
        timer.fillAmount = 1;
        StopAllCoroutines();
        StartCoroutine(enumeratorTimer(time));
    }

    public void StopTimer()
    {
        timer.fillAmount = 0;
        StopAllCoroutines();
    }

    IEnumerator enumeratorTimer(float time)
    {
        yield return new WaitForSeconds(.1f);
        if (timer.fillAmount > 0)
        {
            timer.fillAmount -= .1f/time;
            StartCoroutine(enumeratorTimer(time));
        }
        else
        {
            if (LevelGenerator.isBonus)
                ChangeGeneralUI();
            else
                GameController.instance.Lose();
        }
    }

    public float GetTimerTime()
    {
        return timer.fillAmount;
    }

    public void Win(int score)
    {
        winRewardText.text = Format(score);
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score", 0)+score);
        ChangeGeneralUI();
    }

    public void LoseSetUp()
    {
        winBK.color = new Color(241, 171, 176);
        winText.text = "Level Failed...";
        winButtonBK.color = new Color(207, 80, 84);
        winButtonText.text = "Continue";
        winReward.SetActive(false);
    }

    //Score Animation


}
