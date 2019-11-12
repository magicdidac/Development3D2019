using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilarButton : Interactable
{
    [SerializeField] private float waitTime = .5f;

    [SerializeField] private Interactable interactableObject = null;

    [HideInInspector] private float lastTime = 0;

    public override bool CanInteract()
    {
        return interactableObject.CanInteract() && (lastTime + waitTime < Time.time);
    }

    public override void Interact()
    {
        lastTime = Time.time;

        //TODO: Make Animation

        interactableObject.Interact();

    }
}
