using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaLine : MonoBehaviour
{
    [HideInInspector] public LavaController lc = null;

    private void FixedUpdate()
    {
        bool areEqual = Utility.CompareVectors3(transform.position, lc.transform.position + lc.limitPosition);

        if (areEqual)
            transform.position = lc.transform.position + lc.retrurnPosition;

        transform.position = Vector3.MoveTowards(transform.position, lc.transform.position + lc.limitPosition, lc.speed * Time.deltaTime);
    }

}
