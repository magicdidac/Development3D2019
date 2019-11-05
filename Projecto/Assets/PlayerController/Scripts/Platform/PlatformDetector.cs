using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDetector : MonoBehaviour
{
    [HideInInspector] private Platform plat;
    [HideInInspector] private Vector3 nextPos;
    [HideInInspector] private float nextTime = 0;

    private void Start()
    {
        plat = transform.parent.GetComponent<Platform>();
        nextPos = (plat.isPlayerRequired)? transform.position : plat.NextPos();
    }

    public void MoveOverTime()
    {
        Invoke("Move", plat.waitTime);
    }

    public void Move()
    {
        if (plat.isPlayerRequired)
        {
            GameManager.instance.audioManager.PlaySound("Activate");
            nextPos = plat.NextPos();
        }
    }

    public bool IsPlayerRequired()
    {
        return plat.isPlayerRequired;
    }

    public bool BackToStart()
    {
        return plat.backToStart;
    }

    private void FixedUpdate()
    {
        bool areEqual = Utility.CompareVectors3(nextPos, transform.position);

        if(!areEqual && Time.time > nextTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPos, plat.speed * Time.deltaTime);
        }
        else if(areEqual && !plat.isPlayerRequired)
        {
            nextTime = Time.time + plat.waitTime;
            nextPos = plat.NextPos();
        }
    }


}
