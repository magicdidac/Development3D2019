using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{

    [HideInInspector] public enum Type { Pickable, Door }

    [HideInInspector] protected Type selfType = Type.Pickable;


    public abstract InteractMessage GetInteractMessage();

    public abstract void Interact();

}
