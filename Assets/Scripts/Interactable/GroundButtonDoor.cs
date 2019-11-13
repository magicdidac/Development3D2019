using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundButtonDoor : Door
{
    public override bool CanInteract()
    {
        return false;
    }
}
