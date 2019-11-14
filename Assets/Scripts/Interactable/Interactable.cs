using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    public virtual bool CanInteract()
    {
        return false;
    }

    public virtual void Interact()
    {
        throw new System.Exception("This object (" + name + ") has not implemented Interact() function.");
    }

    public virtual void InteractPositive()
    {
        throw new System.Exception("This object (" + name + ") has not implemented InteractPositive() function.");
    }

    public virtual void InteractNegative()
    {
        throw new System.Exception("This object (" + name + ") has not implemented InteractNegative() function.");
    }

}
