using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ConnectPath))]
public class ConnectPathEditor : Editor
{

    private ConnectPath instance;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        instance = (ConnectPath)target;

        if(GUILayout.Button("Generate Path"))
        {
            instance.CreatePath();
        }

        if (GUILayout.Button("Clear"))
        {
            instance.ClearLines();
        }

    }
}
