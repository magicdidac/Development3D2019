using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : ConnectPoint
{
    [SerializeField] private SpriteRenderer spr = null;
    [SerializeField] private Sprite disabledSprite = null;
    [SerializeField] private Sprite enabledSprite = null;

    public override void Disable()
    {
        spr.color = Variables.blueColor;
        spr.sprite = disabledSprite;
    }

    public override void Enable()
    {
        spr.color = Variables.orangeColor;
        spr.sprite = enabledSprite;
    }
}