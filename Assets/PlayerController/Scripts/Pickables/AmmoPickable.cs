using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickable : Pickable
{

    [SerializeField] private int ammoGive = 10;

    public override InteractMessage GetInteractMessage()
    {
        return (gm.player.haveGun && !gm.player.gun.HaveMaxAmmo()) ? new InteractMessage() : new InteractMessage("Ammo full");
    }

    protected override void NowGetPickable()
    {
        gm.player.gun.AddAmmo(ammoGive);
        Destroy(gameObject);
    }
}
