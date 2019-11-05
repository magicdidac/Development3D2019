using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectDetection
{

    [HideInInspector] private Camera camera;
    [HideInInspector] private float maxActionDistance;
    [HideInInspector] private LayerMask layer;
    [HideInInspector] private FPSController player;
    [HideInInspector] private GameManager gm;

    public InteractableObjectDetection(Camera camera, float maxActionDistance, FPSController player)
    {
        this.gm = GameManager.instance;

        this.camera = camera;
        this.maxActionDistance = maxActionDistance;
        this.layer = LayerMask.GetMask("Interactable");
        this.player = player;
    }

    public void NameMustBeChange()
    {
        NameMustBeChange(layer);
    }

    public void NameMustBeChange(LayerMask layer)
    {
        RaycastHit hit;

        hit = DoRaycast(layer);
        if (hit.transform == null) //Check if really hit
        {
            // gm.uiController.SetCantActionButton(false);
            gm.uiController.DesactiveActionInfo();
            return;
        }

        InteractableObject io = hit.transform.GetComponent<InteractableObject>();

        gm.uiController.ActiveActionInfo(io.GetInteractMessage());

        if (io.GetInteractMessage().canInteract && player.actionInput)
        {
            player.actionInput = false;
            io.Interact();
        }else if(!io.GetInteractMessage().canInteract && player.actionInput)
        {
            player.actionInput = false;
            gm.audioManager.PlaySound("Error");
        }

    }

    public RaycastHit DoRaycast(LayerMask layer)
    {
        RaycastHit hit;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxActionDistance, layer))
            return hit;

        return new RaycastHit();

    }

    public RaycastHit DoRaycast()
    {
        return DoRaycast(layer);

    }

}
