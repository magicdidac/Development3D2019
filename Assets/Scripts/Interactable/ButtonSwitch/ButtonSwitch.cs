using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonSwitch : Interactable
{
    [SerializeField] protected Interactable dotsPath = null;

    [HideInInspector] public bool isActive { get; protected set; }

    public void Deactivate()
    {
        isActive = false;
    }

}
