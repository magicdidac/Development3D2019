using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Gun
{
    [HideInInspector] private PlayerControls controls;
    [HideInInspector] private bool shooting = false;

    private new void Start()
    {
        base.Start();

        controls = transform.parent.parent.parent.GetComponent<FPSController>().controls;

        controls.Player.Fire.performed += _ => shooting = true;
        controls.Player.Fire.canceled += _ => shooting = false;
        controls.Player.Reload.performed += _ => Reload();
    }

    private void Update()
    {
        if (shooting)
            Shoot();
    }

    public override void Shoot()
    {
        if (reloading || Time.time < lastTime + cadence)
            return;

        if(ammo <= 0 && gunAmmo <= 0)
        {
            gm.audioManager.PlaySound("NoAmmoShoot");
            shooting = false;
            return;
        }

        if(gunAmmo <= 0)
        {
            gm.audioManager.PlaySound("NoAmmoShoot");
            Reload();
            return;
        }

        InstantiateParticles();
        gm.audioManager.PlaySound("Shoot");
        player.animationController.StartAnimation("Shoot", false);

        lastTime = Time.time;
        gunAmmo--;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit))
        {
            if (!hit.collider.isTrigger)
            {
                try
                {
                    hit.collider.GetComponent<DamageZone>().ReviveDamage(damage, hit.point);
                }
                catch { InstantiateDecal(hit.point, hit.normal, hit.transform); }
            }
        }

    }

    public override void Reload()
    {
        if (gunAmmo == maxLoader || reloading || ammo == 0)
            return;

        reloading = true;
        gm.audioManager.PlaySound("Reload");
        player.animationController.StartAnimation("Reload", false);
    }

    private void NowReload()
    {
        int oldAmmo = ammo;
        ammo = Mathf.Max(ammo - (maxLoader - gunAmmo),0);
        gunAmmo = Mathf.Min(maxLoader, gunAmmo + oldAmmo);
        reloading = false;
    }
}
