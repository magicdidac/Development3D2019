using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundButton : ButtonSwitch
{

    public override void InteractPositive()
    {
        dotsPath.InteractPositive();
        GameManager.instance.audioManager.PlayAtPosition("Button-InteractPositive", transform);
        isActive = true;
    }

    public override void InteractNegative()
    {
        dotsPath.InteractNegative();
        GameManager.instance.audioManager.PlayAtPosition("Button-InteractNegative", transform);
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
