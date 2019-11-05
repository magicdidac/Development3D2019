using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldThrowable : ThrowablePickable
{
    [SerializeField] private int shieldGive = 10;

    public override InteractMessage GetInteractMessage()
    {
        try
        {
            return (!gm.player.dmgShield.HaveMaxShield()) ? new InteractMessage() : new InteractMessage("Shield full");
        }
        catch { return new InteractMessage("Shield full"); }
    }

    protected override void NowGetPickable()
    {
        gm.player.dmgShield.AddShield(shieldGive);
        Destroy(gameObject);
    }
}
