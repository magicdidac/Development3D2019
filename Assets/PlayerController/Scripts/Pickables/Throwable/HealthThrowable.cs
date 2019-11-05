using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthThrowable : ThrowablePickable
{
    [SerializeField] private int healthGive = 10;

    public override InteractMessage GetInteractMessage()
    {
        try {
            return (!gm.player.dmgShield.HaveMaxHealth()) ? new InteractMessage() : new InteractMessage("Health full");
        }
        catch { return new InteractMessage("Health full"); }
    }

    protected override void NowGetPickable()
    {
        gm.player.dmgShield.AddHealth(healthGive);
        Destroy(gameObject);
    }
}
