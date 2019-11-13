using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPoint : ConnectPoint
{
    [SerializeField] private SpriteRenderer spr = null;

    public override void Disable()
    {
        spr.color = Variables.blueColor;
    }

    public override void Enable()
    {
        spr.color = Variables.orangeColor;

    }
}
