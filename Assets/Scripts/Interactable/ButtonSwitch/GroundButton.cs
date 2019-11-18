using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundButton : ButtonSwitch
{

    [HideInInspector] private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
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
        dotsPath.InteractNegative();
        anim.SetBool("isDown", false);
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
