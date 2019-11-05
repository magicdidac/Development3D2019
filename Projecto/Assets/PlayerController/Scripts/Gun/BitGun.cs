using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitGun : Gun
{

    [HideInInspector] private PlayerControls controls;


    private new void Start()
    {
        base.Start();

        controls = transform.parent.parent.parent.GetComponent<FPSController>().controls;

        controls.Player.Fire.performed += _ => Shoot();
        controls.Player.Reload.performed += _ => Reload();
    }

    public override void Shoot()
    {
        if (gunAmmo <= 0)
        {
            gm.audioManager.PlaySound("NoAmmoShoot");
            Reload();
            return;
        }

        if (reloading)
            return;

        if (Time.time < lastTime + cadence)
            return;

        player.animationController.StartAnimation("Shoot",false);

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit))
        {
            if (!hit.collider.isTrigger)
            {
                try
                {
                    hit.collider.GetComponent<DamageZone>().ReviveDamage(damage, hit.point);
                } catch { InstantiateDecal(hit.point, hit.normal, hit.transform); }
            }
        }

        InstantiateParticles();

        lastTime = Time.time;
        gunAmmo--;
    }

    public override void Reload()
    {
        if (gunAmmo == maxLoader)
            return;

        if (reloading)
            return;

        reloading = true;

        player.animationController.StartAnimation("Reload",false);

    }

    private void NowReload()
    {
        ammo -= maxLoader - gunAmmo;
        gunAmmo = maxLoader;
        reloading = false;
    }

    

}
