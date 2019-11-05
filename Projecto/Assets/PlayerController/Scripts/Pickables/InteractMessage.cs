using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMessage
{
    public string message { get; private set; }
    public bool canInteract { get; private set; }

    public InteractMessage() : this("Take", true) { }

    public InteractMessage(bool canInteract) : this((canInteract) ? "Take" : "", canInteract) { }

    public InteractMessage(string message) : this(message, false) { }

    public InteractMessage(string message, bool canInteract)
    {
        this.canInteract = canInteract;
        this.message = message;
    }


}
