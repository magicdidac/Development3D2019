using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection
{
    [HideInInspector] private FPSController player;

    public TriggerDetection(FPSController player)
    {
        this.player = player;
    }

    public void TriggerEnter(Collider other)
    {
        if (other.tag == "Pickable" && other.GetComponent<Pickable>().GetInteractMessage().canInteract)
        {
            other.GetComponent<Pickable>().GetWithTrigger();
        }
        else if (other.tag == "Checkpoint")
        {
            other.GetComponent<Checkpoint>().EnableCheckpoint(player);
        }
        else if (other.GetComponent<PlatformDetector>() != null)
        {
            player.transform.parent = other.transform;

            if(other.GetComponent<PlatformDetector>().IsPlayerRequired())
                other.GetComponent<PlatformDetector>().Move();

        }
        else if (other.tag == "DeadZone")
        {
            player.uiController.Dead();
        }
        else if (other.tag == "Door" && other.GetComponent<AutomaticDoor>() != null)
        {
            other.GetComponent<InteractableObject>().Interact();
        }
    }

    public void TriggerStay(Collider other)
    {

    }

    public void TriggerExit(Collider other)
    {
        if(other.GetComponent<PlatformDetector>())
        {
            PlatformDetector plat = other.GetComponent<PlatformDetector>();

            if (plat.BackToStart())
                plat.MoveOverTime();

            player.transform.parent = null;
        }
        else if (other.tag == "Door" && other.GetComponent<AutomaticDoor>() != null)
        {
            other.GetComponent<InteractableObject>().Interact();
        }
    }


}
