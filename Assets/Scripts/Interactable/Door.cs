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
        anim.SetBool("Open", true);
    }
    
    public override void InteractNegative()
    {
        anim.SetBool("Open", false);
    }
}
