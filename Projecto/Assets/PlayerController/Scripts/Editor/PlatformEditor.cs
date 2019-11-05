using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Platform))]
public class PlatformEditor : Editor
{
    private Platform p;

    private bool showPoints = true;

    public override void OnInspectorGUI()
    {

        p = (Platform)target;

        if (p.nextPos == null)
            p.nextPos = new List<Transform>();

        EditorGUILayout.LabelField("Platform Editor", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        p.speed = EditorGUILayout.FloatField("Move speed", p.speed);
        p.waitTime = EditorGUILayout.FloatField("Stop time (in s)", p.waitTime);

        if (p.isPlayerRequired = EditorGUILayout.Toggle("Is Player Required", p.isPlayerRequired))
        {
            while (p.nextPos.Count > 1){
                DeletePoint(p.nextPos.Count-1);
            }
            p.backToStart = EditorGUILayout.Toggle("Back To Start", p.backToStart);
        }

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();

        showPoints = EditorGUILayout.Foldout(showPoints, "Platform Points");
        EditorGUILayout.Space();

        if (GUILayout.Button("-", EditorStyles.miniButtonLeft))
        {
            DeletePoint(p.nextPos.Count - 1);
        }

        GUI.enabled = p.nextPos.Count > 0;

        if(GUILayout.Button("cls", EditorStyles.miniButtonMid))
        {
            foreach (Transform t in p.nextPos)
            {
                try
                {
                    DestroyImmediate(t.gameObject);
                }
                catch { }
            }

            p.nextPos.Clear();
        }

        if (GUILayout.Button("+", EditorStyles.miniButtonRight))
        {
            GameObject g = new GameObject();
            g.name = "Point" + Format(p.nextPos.Count);
            g.transform.parent = p.transform;
            g.transform.localPosition = new Vector3(1.5f, 1, 1.5f);
            p.nextPos.Add(g.transform);
            Selection.activeGameObject = g;
        }

        GUI.enabled = true;

        EditorGUILayout.EndHorizontal();
        if (p.nextPos.Count > 0 && showPoints)
        {
            EditorGUILayout.Space();

            foreach (Transform t in p.nextPos)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("- " + t.name + "    " + t.localPosition);
                if (GUILayout.Button("Focus", EditorStyles.miniButton))
                {
                    Selection.activeGameObject = t.gameObject;
                }

                EditorGUILayout.EndHorizontal();
            }
        }


        if (GUI.changed)
            EditorUtility.SetDirty(p);

    }

    private void DeletePoint(int index)
    {
        DestroyImmediate(p.nextPos[index].gameObject);

        p.nextPos.RemoveAt(index);
    }

    private static string Format(int n)
    {
        if (n < 10)
            return "0" + n;
        else
            return n+"";
    }

}
