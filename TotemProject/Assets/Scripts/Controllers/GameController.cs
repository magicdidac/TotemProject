using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Instance
    [HideInInspector] public static GameController instance = null;
    
    //Controllers
    [HideInInspector] public UIController uiController;
    [SerializeField] public CameraController cameraController;

    //Totems
    [SerializeField] public GameObject redTotem, greenTotem, blueTotem;
    [HideInInspector] public List<Totem> totems = new List<Totem>();

    [HideInInspector] private int phaseCount = 0;
    [HideInInspector] private int[] level;
    [HideInInspector] private int score = 0;

    //Times
    [SerializeField] private float timePerTotem = 1;
    [SerializeField] private float timePerBonus = 7;

    private void Awake()
    {
        instance = this;

        uiController = GetComponent<UIController>();

        InitializePlayerPrefs();
        InitializeTotem();

        uiController.DrawLevelNumber();
        uiController.DrawButterfliesNumber(PlayerPrefs.GetInt("Score"));

        Invoke("ChangeFade", .5f);
    }

    private void ChangeFade()
    {
        uiController.ChangeFade();
    }

    private void InitializePlayerPrefs()
    {
        PlayerPrefs.GetInt("Level", 1);
        PlayerPrefs.GetInt("Score", 0);
    }

    private void InitializeTotem()
    {

        level = LevelGenerator.Generate().ToArray();

        uiController.SelectPhaseCounter(level.Length);
        InitializeTotem(0);

    }

    private void InitializeTotem(int phaseIndex)
    {
        int totemsCount = level[phaseIndex];

        for (int i = 0; i<totemsCount; i++)
        {
            InstantiateTotem(i);
        }

        ChangeCameraPosition();

    }

    private void InstantiateTotem(int pos)
    {
        Vector3 position = new Vector3(-1, 2.5f * pos, 0);

        switch (Random.Range(0, 3))
        {
            case 0: //Red
                totems.Add(new Totem(Totem.Type.Red, Instantiate(redTotem, position, Quaternion.identity)));
                break;
            case 1: //Green
                totems.Add(new Totem(Totem.Type.Green, Instantiate(greenTotem, position, Quaternion.identity)));
                break;
            case 2: //Blue
                totems.Add(new Totem(Totem.Type.Blue, Instantiate(blueTotem, position, Quaternion.identity)));
                break;
            default:
                Debug.LogError("The value is wrong.");
                break;
        }

    }

    private void ChangeCameraPosition()
    {
        cameraController.SetNewPosition(new Vector3(0, 3.66f + totems[totems.Count-1].totem.transform.position.y, -10));
    }


    public void RemoveTotem(string str)
    {
        Totem.Type type = StringToTotemType(str);
        int lastIndex = totems.Count - 1;

        if (totems[lastIndex].type == type)
        {
            Destroy(totems[lastIndex].totem, .5f);
            Vector3 force = ((totems.Count % 2 == 0) ? Vector3.right : Vector3.left) * 50;
            totems[lastIndex].totem.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            AddScore();
            totems.RemoveAt(lastIndex);

            if (totems.Count <= 0)
            {
                uiController.StopTimer();
                phaseCount++;
                Invoke("NextPhase", .5f);
                return;
            }

            uiController.StartTimer(timePerTotem);
            ChangeCameraPosition();

        }
        else
            Lose();
    }

    private void NextPhase()
    {
        if(phaseCount < level.Length)
            uiController.ChangeFade();

        Invoke("CreateNextPhase", .5f);
    }

    private void CreateNextPhase()
    {
        if (uiController.NextPhaseCounter()) //WIN
        {
            uiController.Win(score);
            return;
        }
        InitializeTotem(phaseCount);
        uiController.ChangeFade();
    }

    public void reloadScene()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 1) + 1);
        PlayerPrefs.Save();
        uiController.ChangeFade();
        SceneManager.LoadSceneAsync(0);
    }

    public void PlayButton()
    {

        if (LevelGenerator.isBonus)
            uiController.StartTimer(timePerBonus);

        uiController.ChangeGeneralUI();
    }

    public void Lose()
    {
        Debug.Log("LOSE");
        uiController.LoseSetUp();
        uiController.ChangeGeneralUI();
        uiController.StopTimer();
    }

    private void AddScore()
    {
        if (LevelGenerator.isBonus)
        {
            score += 5;
            return;
        }

        float time = uiController.GetTimerTime();

        if (time == 0)
            score += 5;
        else if (time > .75f)
            score += 3;
        else if (time > .5f)
            score += 2;
        else
            score += 1;

    }

    private Totem.Type StringToTotemType(string str)
    {
        switch (str)
        {
            case "R":
                return Totem.Type.Red;
            case "G":
                return Totem.Type.Green;
            case "B":
                return Totem.Type.Blue;
            default:
                throw new System.Exception("Invalid value");
        }
    }
}
