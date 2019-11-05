using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Platform : MonoBehaviour
{

    [HideInInspector] public Vector3 initialPos;
    [HideInInspector] private Vector3 oldPos;
    [HideInInspector] public List<Transform> nextPos = new List<Transform>();
    [HideInInspector] public float speed = 1;
    [HideInInspector] public bool isPlayerRequired;
    [HideInInspector] public bool backToStart;
    [HideInInspector] public float waitTime = .5f;

    [HideInInspector] private Transform platform;
    [HideInInspector] private int nextIndexPos = 0;

    private void Awake()
    {
        platform = transform.GetChild(0);

        initialPos = platform.position;
        oldPos = initialPos;
    }


    public Vector3 NextPos()
    {
        if(nextIndexPos >= nextPos.Count)
        {
            nextIndexPos = 0;
            oldPos = initialPos;
            return initialPos;
        }
        else
        {
            oldPos = nextPos[nextIndexPos].position;
            nextIndexPos++;
            return oldPos;
        }
    }


    private void OnDrawGizmos()
    {
        if (platform == null)
            platform = transform.GetChild(0);

        
        Gizmos.color = new Color(0, 255, 0, .5f);

        Gizmos.DrawCube(platform.position, platform.localScale * 1.1f);

        Gizmos.color = new Color(255, 0, 0, .5f);
        
        if (nextPos == null || nextPos.Count == 0)
            return;

        Vector3 oldPos;

        if (!Application.isPlaying)
            oldPos = platform.position;
        else
            oldPos = this.initialPos;

        if (!isPlayerRequired)
        {
            foreach (Transform t in nextPos)
            {
                Gizmos.color = new Color(255, 0, 0, .5f);
                Gizmos.DrawCube(t.position, platform.localScale);
                oldPos = t.position;
            }
        }
        else
        {
            if(nextPos[0] == null)
            {
                nextPos = new List<Transform>();
                return;
            }

            Gizmos.color = new Color(255, 0, 0, .5f);
            Gizmos.DrawCube(nextPos[0].position, platform.localScale);
        }

    }

}
