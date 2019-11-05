using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Pickable
{
    [HideInInspector] private KeyDoor door;

    public override InteractMessage GetInteractMessage()
    {
        return new InteractMessage();
    }

    public void SetDoor(KeyDoor door)
    {
        this.door = door;
    }

    protected override void NowGetPickable()
    {
        door.KeyCollected();
        Destroy(gameObject);
    }
}
