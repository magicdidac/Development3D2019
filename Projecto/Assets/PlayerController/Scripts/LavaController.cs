using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{

    [SerializeField] public Vector3 limitPosition = Vector3.zero;
    [SerializeField] public Vector3 retrurnPosition = Vector3.zero;
    [SerializeField] public float speed = 1;

    [SerializeField] private List<LavaLine> lines = new List<LavaLine>();


    [Space]
    [Header("Debug")]
    [SerializeField] private Vector3 lineScale = Vector3.zero;

    private void Awake()
    {
        foreach(LavaLine l in lines)
        {
            l.lc = this;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 255, 0, .25f);
        Gizmos.DrawCube(transform.position + retrurnPosition, lineScale);
        
        Gizmos.color = new Color(255, 0, 0, .25f);
        Gizmos.DrawCube(transform.position + limitPosition, lineScale);
    }

}
