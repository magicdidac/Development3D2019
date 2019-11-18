using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilarButton : ButtonSwitch
{
    [SerializeField] private float waitTime = .5f;

    [HideInInspector] private float lastTime = 0;
    [HideInInspector] private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override bool CanInteract()
    {
        return (lastTime + waitTime < Time.time);
    }

    public override void Interact()
    {
        base.Interact();
    }

    public override void InteractPositive()
    {
        lastTime = Time.time;

        anim.SetBool("isDown", true);

        GameManager.instance.audioManager.PlayAtPosition("Switch-InteractPositive", transform);

        isActive = true;

        dotsPath.InteractPositive();
        Invoke("DisableDots", waitTime - .05f);

    }

    public override void InteractNegative()
    {
        isActive = false;

        anim.SetBool("isDown", false);

        GameManager.instance.audioManager.PlayAtPosition("Switch-InteractNegative", transform);
    }

    private void DisableDots()
    {
        dotsPath.InteractNegative();
        InteractNegative();
    }

}
