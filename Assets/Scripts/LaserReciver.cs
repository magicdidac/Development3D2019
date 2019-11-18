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

    public override void InteractPositive()
    {
        if (isActive)
            return;

        dotsPath.InteractPositive();
        GameManager.instance.audioManager.PlayAtPosition("Button-InteractPositive", transform);
        isActive = true;
    }

    public override void InteractNegative()
    {
        if (!isActive)
            return;
        dotsPath.InteractNegative();
        GameManager.instance.audioManager.PlayAtPosition("Button-InteractNegative", transform);
        isActive = false;
    }

}
