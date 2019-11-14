using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReciver : ButtonSwitch
{
    [HideInInspector] public bool active = false;

    protected void FixedUpdate()
    {
        if (active)
            InteractPositive();
        else
            InteractNegative();

        active = false;
    }

    private void LateUpdate()
    {
        
    }

    public override void InteractPositive()
    {
        dotsPath.InteractPositive();
        isActive = true;
    }

    public override void InteractNegative()
    {
        dotsPath.InteractNegative();
        isActive = false;
    }

}
