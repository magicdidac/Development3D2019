using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGun : Gun
{

    private new void Start()
    {
        base.Start();
        Shoot();
    }

    public override void Reload() { }

    public override void Shoot()
    {

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            try
            {
                hit.collider.GetComponent<Damager>().GetDamage(damage);
            }
            catch { }
        }

        InstantiateParticles();

        Invoke("Shoot", 2);

    }
}
