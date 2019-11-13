using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilarButton : Interactable
{
    [SerializeField] private float waitTime = .5f;

    [SerializeField] private List<Interactable> interactableObjects = new List<Interactable>();

    [HideInInspector] private float lastTime = 0;

    public override bool CanInteract()
    {
        return (lastTime + waitTime < Time.time);
    }

    public override void Interact()
    {
        lastTime = Time.time;

        //TODO: Make Animation

        foreach(Interactable i in interactableObjects)
            i.Interact();

        Invoke("DisableDots", waitTime - .05f);

    }

    private void DisableDots()
    {
        interactableObjects[0].Interact();
    }

}
