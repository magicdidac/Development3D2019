using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractionCube : Pickable
{
    [SerializeField] private LineRenderer lineR = null;

    [HideInInspector] public bool active = false;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (active)
        {
            lineR.enabled = true;
            Laser.Make(lineR);
        }
        else
            lineR.enabled = false;

        active = false;
    }

}
