using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    [HideInInspector] private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
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
