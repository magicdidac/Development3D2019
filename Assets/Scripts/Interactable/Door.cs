using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    [HideInInspector] protected Animator anim;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        CheckAllTriggersPositiveOrNegative();
    }

    public override void InteractPositive()
    {
        if (anim.GetBool("Open"))
            return;

        GameManager.instance.audioManager.PlayAtPosition("Door-Open", transform);

        anim.SetBool("Open", true);
    }
    
    public override void InteractNegative()
    {
        if (!anim.GetBool("Open"))
            return;

        GameManager.instance.audioManager.PlayAtPosition("Door-Close", transform);
        anim.SetBool("Open", false);
    }
}
