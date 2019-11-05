using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoThrowable : ThrowablePickable
{
    [SerializeField] private int ammoGive = 10;

    public override InteractMessage GetInteractMessage()
    {
        try
        {

            return (gm.player.haveGun && !gm.player.gun.HaveMaxAmmo()) ? new InteractMessage() : new InteractMessage("Ammo full");

        }catch { return new InteractMessage("Ammo full"); }
    }

    protected override void NowGetPickable()
    {
        gm.player.gun.AddAmmo(ammoGive);
        Destroy(gameObject);
    }
}
