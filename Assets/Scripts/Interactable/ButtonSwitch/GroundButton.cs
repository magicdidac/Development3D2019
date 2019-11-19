using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundButton : ButtonSwitch
{

    [HideInInspector] private Animator anim;

    [HideInInspector] private Collider otherObject;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (otherObject && !otherObject.enabled)
        {
            InteractNegative();
            otherObject = null;
        }
    }

    public override void InteractPositive()
    {
        dotsPath.InteractPositive();
        anim.SetBool("isDown", true);
        GameManager.instance.audioManager.PlayAtPosition("Button-InteractPositive", transform);
        isActive = true;
    }

    public override void InteractNegative()
    {
        if (!isActive)
            return;

        dotsPath.InteractNegative();
        anim.SetBool("isDown", false);
        GameManager.instance.audioManager.PlayAtPosition("Button-InteractNegative", transform);
        isActive = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(otherObject == null)
        {
            otherObject = other;
            InteractPositive();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == otherObject)
        {
            InteractNegative();
            otherObject = null;
        }
    }

}
