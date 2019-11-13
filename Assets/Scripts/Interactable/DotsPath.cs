using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotsPath : Interactable
{
    [SerializeField] private ConnectPath path = null;

    [HideInInspector] private bool enable = false;

    public override bool CanInteract()
    {
        return false;
    }

    public override void Interact()
    {
        enable = !enable;

        if (enable)
            path.Enable();
        else
            path.Disable();

    }
}
