using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Instance
    [HideInInspector] public static GameController instance = null;

    //Game Store
    [HideInInspector] public GameStore store = new GameStore();

    //Controllers
    [HideInInspector] public UIController uiController;

    //Totems
    [SerializeField] public GameObject redTotem, greenTotem, blueTotem;
    [HideInInspector] public List<Totem> totems;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        uiController = GetComponent<UIController>();

        InitializePlayerPrefs();
        InitializeTotem();
        uiController.DrawLevelNumber(PlayerPrefs.GetInt("Level"));
        uiController.DrawButterfliesNumber(PlayerPrefs.GetInt("Butterflies"));
        uiController.ChangeFade();

    }

    private void InitializePlayerPrefs()
    {
        PlayerPrefs.GetInt("Level", 0);
        PlayerPrefs.GetInt("Butterflies", 0);
    }

    private void InitializeTotem() { InitializeTotem(0); }

    private void InitializeTotem(int phaseIndex)
    {
        int totemsCount = store.gameLevels[PlayerPrefs.GetInt("Level")].phases[phaseIndex].totems;

        for (int i = 0; i<totemsCount; i++)
        {
            InstantiateTotem(i);
        }

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


    public void RemoveTotem(string str)
    {
        Totem.Type type = StringToTotemType(str);

        Debug.Log(totems[0].totem);

        if (totems[0].type == type)
        {
            Destroy(totems[0].totem);
            totems.RemoveAt(0);

            if (totems.Count <= 0)
            {
                Debug.Log("WIN");
            }

        }
        else
            Debug.Log("LOSE");
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
