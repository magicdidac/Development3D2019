using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundButton : ButtonSwitch
{

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

    private void OnTriggerEnter(Collider other)
    {
        InteractPositive();
    }

    private void OnTriggerExit(Collider other)
    {
        InteractNegative();
    }

}
