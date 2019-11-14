using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotsPath : Interactable
{
    [SerializeField] private ConnectPath path = null;

    public override void InteractPositive()
    {
        path.Enable();
    }

    public override void InteractNegative()
    {
        path.Disable();
    }

}
