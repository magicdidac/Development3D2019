using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickable : Pickable
{
    [SerializeField] private int shieldGive = 10;

    public override InteractMessage GetInteractMessage()
    {
        return (!gm.player.dmgShield.HaveMaxShield()) ? new InteractMessage() : new InteractMessage("Shield full");
    }

    protected override void NowGetPickable()
    {
        gm.player.dmgShield.AddShield(shieldGive);
        Destroy(gameObject);
    }
}
