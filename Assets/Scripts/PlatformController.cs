using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    [SerializeField] private Transform startPoint = null;
    [SerializeField] private Transform endPoint = null;

    [SerializeField] private float speed = 2;

    [SerializeField] private float waitTime = 1.5f;
    [HideInInspector] private float lastTime = -1.5f;

    [HideInInspector] private Vector3 nextPoint;
    [HideInInspector] private Quaternion nextRotation;

    private void Start()
    {
        nextPoint = endPoint.position;
        nextRotation = startPoint.rotation;
        transform.position = startPoint.position;
    }

    private void FixedUpdate()
    {
        if(transform.position == nextPoint)
        {
            ChangeNextPoint();
            lastTime = Time.time;
        }

        if (lastTime + waitTime < Time.time)
            transform.position = Vector3.MoveTowards(transform.position, nextPoint, speed * Time.fixedDeltaTime);
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, nextRotation, Time.deltaTime * 200);
        }

    }

    private void ChangeNextPoint()
    {
        nextPoint = (nextPoint == startPoint.position) ? endPoint.position : startPoint.position;
        nextRotation = (nextPoint == startPoint.position) ? endPoint.rotation: startPoint.rotation;
    }

}
