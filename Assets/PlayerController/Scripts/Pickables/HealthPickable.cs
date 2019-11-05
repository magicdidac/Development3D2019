using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickable : Pickable
{

    [SerializeField] private int healthGive = 10;

    public override InteractMessage GetInteractMessage()
    {
        return (!gm.player.dmgShield.HaveMaxHealth()) ? new InteractMessage() : new InteractMessage("Health full");
    }

    protected override void NowGetPickable()
    {
        gm.player.dmgShield.AddHealth(healthGive);
        Destroy(gameObject);
    }

}
