using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectPath : MonoBehaviour
{

    [SerializeField] public GameObject pointPrefab = null;

    [HideInInspector] private List<ConnectLine> lines = new List<ConnectLine>();

    private void Start()
    {
        lines = new List<ConnectLine>(transform.GetComponentsInChildren<ConnectLine>());

        Disable();
    }

    public void CreatePath()
    {
        lines = new List<ConnectLine>(transform.GetComponentsInChildren<ConnectLine>());

        foreach(ConnectLine cl in lines)
        {
            cl.parent = this;
            cl.CreateLine();
        }

    }

    public void ClearLines()
    {
        foreach(ConnectLine l in lines)
        {
            l.Clear();
        }
    }

    public void Enable()
    {

       foreach(ConnectLine l in lines)
        {
            l.Enable();
        }
    }

    public void Disable()
    {
        foreach (ConnectLine l in lines)
        {
            l.Disable();
        }
    }

}
