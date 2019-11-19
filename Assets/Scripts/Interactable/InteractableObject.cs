using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : Interactable
{

    [SerializeField] protected List<ButtonSwitch> triggers = new List<ButtonSwitch>();


    protected void CheckAllTriggersPositiveOrNegative()
    {
        if (triggers.Count == 0)
        {
            InteractNegative();
            return;
        }

        foreach (ButtonSwitch b in triggers)
        {
            if (!b.isActive)
            {
                InteractNegative();
                return;
            }
        }

        InteractPositive();

    }

    protected void CheckAllTriggers()
    {
        if (triggers.Count == 0)
            return;

        foreach (ButtonSwitch b in triggers)
        {
            if (!b.isActive)
                return;
        }

        Interact();
    }

    protected void DeactivateTriggers()
    {
        foreach(ButtonSwitch b in triggers)
        {
            b.Deactivate();
        }
    }

    private void OnDrawGizmos()
    {
        foreach(ButtonSwitch b in triggers)
        {
            if (b != null)
            {
                Gizmos.DrawLine(transform.position, b.transform.position);
            }
        }
    }

}
