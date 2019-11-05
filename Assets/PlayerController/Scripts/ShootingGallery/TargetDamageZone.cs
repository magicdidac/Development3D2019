using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDamageZone : DamageZone
{

    public override void ReviveDamage(int amount, Vector3 hitPoint)
    {
        if(dmg.health > 0)
            FloatingTextController.CreateFloatingText("HIT", hitPoint);

        dmg.GetDamage(amount);
    }
}
