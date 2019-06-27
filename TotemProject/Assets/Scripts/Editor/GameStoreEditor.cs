using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(GameController))]
public class GameStoreEditor : Editor
{

    private List<bool> show = new List<bool>();

    public override void OnInspectorGUI()
    {

        GameStore store = ((GameController)target).store;
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Level"))
            store.AddLevel();

        if(GUILayout.Button("Remove Level"))
            store.RemoveLevel();

        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        for (int i = 0; i < store.gameLevels.Count; i++)
        {
            if (show.Count < store.gameLevels.Count)
                show.Add(false);

            GameLevels gl = store.gameLevels[i];
            GUILayout.BeginHorizontal();

            show[i] = EditorGUILayout.Foldout(show[i], "Level " + (i + 1));

            if (GUILayout.Button("+"))
                gl.AddPhase();

            if (GUILayout.Button("-"))
                gl.RemovePhase();

            GUILayout.EndHorizontal();

            if (show[i])
            {

                GUILayout.Space(10);
                if (gl.phases.Count == 0)
                    gl.AddPhase();

                for (int j = 0; j < gl.phases.Count; j++)
                {
                    Phase phase = gl.phases[j];

                    if (phase.totems <= 0)
                        phase.totems = 1;

                    phase.totems = EditorGUILayout.IntField("Totems of Phase " + (j + 1), phase.totems);

                }
            }

            GUILayout.Space(10);

        }

        GUILayout.Space(15);

        base.OnInspectorGUI();
    }

}
