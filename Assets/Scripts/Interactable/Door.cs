using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Door : Interactable
{

    [HideInInspector] private Animator anim;

    [HideInInspector] private bool isOpen = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override void Interact()
    {
        isOpen = !isOpen;
        anim.SetBool("Open", isOpen);
    }
}
