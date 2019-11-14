using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Laser
{
    
    public static void Make(LineRenderer lineR)
    {
        RaycastHit hit;

        if (Physics.Raycast(lineR.transform.position, lineR.transform.forward, out hit))
        {
            lineR.SetPosition(0, lineR.transform.position);
            lineR.SetPosition(1, hit.point);

            if (hit.transform.GetComponent<PlayerMovement>())
                hit.transform.GetComponent<PlayerMovement>().Dead();

            if (hit.transform.GetComponent<Turret>())
                hit.transform.GetComponent<Turret>().Dead();

            if (hit.transform.GetComponent<RefractionCube>())
                hit.transform.GetComponent<RefractionCube>().active = true;
            
            if (hit.transform.GetComponent<LaserReciver>())
                hit.transform.GetComponent<LaserReciver>().active = true;

        }
    }

}
