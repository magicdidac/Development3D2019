using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagerWithShield : Damager
{

    [SerializeField] public int maxShield = 100;
    [HideInInspector] public int shield;

    private new void Start()
    {
        base.Start();
        shield = 100;
    }

    public void AddShield(int amount)
    {
        shield += amount;
        if (shield > maxShield)
            shield = maxShield;
    }

    public override void GetDamage(int amount)
    {
        if (shield > 0)
        {
            int shieldPercentage = (amount * 75) / 100;

            if(shield >= shieldPercentage)
            {
                shield -= shieldPercentage;
                health -= amount - shieldPercentage;
            }
            else
            {
                health -= amount - shield;
                shield = 0;
            }

        }
        else if(health > 0)
            health -= amount;

    }

    public bool HaveMaxShield()
    {
        return shield >= maxShield;
    }

}
