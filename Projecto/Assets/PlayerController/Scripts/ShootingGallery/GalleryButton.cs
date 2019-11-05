using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryButton : InteractableObject
{

    [SerializeField] public bool canInteract = true;
    [SerializeField] private ShootingGallery gallery = null;

    public override InteractMessage GetInteractMessage()
    {
        return (canInteract) ? new InteractMessage("Activate", true) : new InteractMessage("Wait...");
    }

    public override void Interact()
    {
        GameManager.instance.audioManager.PlaySound("Activate");
        canInteract = false;
        gallery.GalleryStart();
    }
}
