using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectLine : MonoBehaviour
{

    [HideInInspector] public ConnectPath parent;

    [HideInInspector] private Transform startPoint;
    [HideInInspector] private Transform endPoint;
    [HideInInspector] private Transform storage;

    [HideInInspector] private List<ConnectPoint> midpoints = new List<ConnectPoint>();

    private void Awake()
    {
        startPoint = transform.GetChild(0);
        endPoint = transform.GetChild(1);
        storage = transform.GetChild(2);

        midpoints = new List<ConnectPoint>(storage.GetComponentsInChildren<ConnectPoint>());

    }

    public void CreateLine()
    {
        startPoint = transform.GetChild(0);
        endPoint = transform.GetChild(1);
        storage = transform.GetChild(2);

        midpoints = new List<ConnectPoint>(storage.GetComponentsInChildren<ConnectPoint>());

        DestroyLine();

        endPoint.rotation = startPoint.rotation;

        if (endPoint.position.x != startPoint.position.x)
        {
            float distance = Mathf.Abs(endPoint.position.x - startPoint.position.x);
            float sign = Mathf.Sign(endPoint.position.x - startPoint.position.x);

            for(float i = .5f; i < distance; i += .5f)
            {
                GameObject newPoint = Instantiate(parent.pointPrefab, new Vector3(startPoint.position.x + (sign * i), startPoint.position.y, startPoint.position.z), Quaternion.identity);
                newPoint.transform.rotation = startPoint.rotation;
                newPoint.transform.parent = storage;
                midpoints.Add(newPoint.GetComponent<ConnectPoint>());
            }

        }
        else if (endPoint.position.y != startPoint.position.y)
        {
            float distance = Mathf.Abs(endPoint.position.y - startPoint.position.y);
            float sign = Mathf.Sign(endPoint.position.y - startPoint.position.y);

            for (float i = .5f;  i < distance; i += .5f)
            {
                GameObject newPoint = Instantiate(parent.pointPrefab, new Vector3(startPoint.position.x, startPoint.position.y + (sign * i), startPoint.position.z), Quaternion.identity);
                newPoint.transform.rotation = startPoint.rotation;
                newPoint.transform.parent = storage;
                midpoints.Add(newPoint.GetComponent<ConnectPoint>());
            }
        }
        else if (endPoint.position.z != startPoint.position.z)
        {
            float distance = Mathf.Abs(endPoint.position.z - startPoint.position.z);
            float sign = Mathf.Sign(endPoint.position.z - startPoint.position.z);

            for (float i = .5f; i < distance; i += .5f)
            {
                GameObject newPoint = Instantiate(parent.pointPrefab, new Vector3(startPoint.position.x, startPoint.position.y, startPoint.position.z + (sign * i)), Quaternion.identity);
                newPoint.transform.rotation = startPoint.rotation;
                newPoint.transform.parent = storage;
                midpoints.Add(newPoint.GetComponent<ConnectPoint>());
            }
        }
        else
            throw new System.Exception("Direction not supported");

    }

    private void DestroyLine()
    {
        foreach(ConnectPoint g in midpoints)
        {
            DestroyImmediate(g.gameObject);
        }

        midpoints.Clear();

    }

    public void Clear()
    {

        midpoints = new List<ConnectPoint>(storage.GetComponentsInChildren<ConnectPoint>());
        DestroyLine();
    }
 
    public void Enable()
    {
        startPoint.GetComponent<ConnectPoint>().Enable();
        endPoint.GetComponent<ConnectPoint>().Enable();

        foreach(ConnectPoint p in midpoints)
        {
            p.Enable();
        }

    }

    public void Disable()
    {
        startPoint.GetComponent<ConnectPoint>().Disable();
        endPoint.GetComponent<ConnectPoint>().Disable();

        foreach (ConnectPoint p in midpoints)
        {
            p.Disable();
        }

    }

}
