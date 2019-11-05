using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{

    [SerializeField] protected float damageMultiplier = 1;
    [SerializeField] protected Damager dmg = null;

    public virtual void ReviveDamage(int amount, Vector3 hitPoint)
    {
        int damage = (int)(amount * damageMultiplier);
        dmg.GetDamage(damage);
        FloatingTextController.CreateFloatingText(damage.ToString(), hitPoint);
    }

}
