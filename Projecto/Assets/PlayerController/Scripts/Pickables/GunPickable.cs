using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickable : Pickable
{
    public override InteractMessage GetInteractMessage()
    {
        return (!gm.player.haveGun) ? new InteractMessage() : new InteractMessage("Inventory full");
    }

    protected override void NowGetPickable()
    {
        gm.player.haveGun = true;
        gm.player.gun.gameObject.SetActive(true);
        gm.player.uiController.SetActiveGunInfo();
        Destroy(gameObject);
    }
}
