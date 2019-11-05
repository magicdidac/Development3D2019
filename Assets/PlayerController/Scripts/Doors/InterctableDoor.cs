using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterctableDoor : Door
{
    public override InteractMessage GetInteractMessage()
    {
        return new InteractMessage("Use", true);
    }

    protected override void Start()
    {
        base.Start();
        BoxCollider b = gameObject.AddComponent<BoxCollider>();
        b.size = door.localScale;
        b.isTrigger = true;

    }
}
