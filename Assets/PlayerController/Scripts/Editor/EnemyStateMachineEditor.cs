using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyStateMachine))]
public class EnemyStateMachineEditor : Editor
{

    private void OnSceneGUI()
    {
        EnemyStateMachine esm = (EnemyStateMachine)target;

        if (esm.currentState != null)
            Handles.Label(esm.transform.position + (Vector3.up * 3), "State: " + esm.currentState.ToString(), EditorStyles.boldLabel);
        else
            Handles.Label(esm.transform.position + (Vector3.up * 3), "State: Null", EditorStyles.boldLabel);

        Handles.color = Color.blue;

        Vector3 oldPos = (Application.isPlaying)? esm.initialPos : esm.transform.position;
        foreach(Transform t in esm.positions)
        {
            Handles.DrawLine(oldPos, t.position);
            oldPos = t.position;
        }

        Handles.DrawWireArc(esm.transform.position, Vector3.up, Vector3.forward, 360, esm.maxAttackDistance);


        Handles.color = Color.red;
        Handles.DrawWireArc(esm.transform.position, Vector3.up, Vector3.forward, 360, esm.maxChaseDistance);
        Handles.DrawWireArc(esm.transform.position, Vector3.up, Vector3.forward, 360, esm.minChaseDistance);

        if (!Application.isPlaying)
            return;

        if (esm.directCastPlayer)
        {
            Handles.color = Color.cyan;
            Handles.DrawLine(esm.transform.position, esm.player.position);
        }

    }

}
