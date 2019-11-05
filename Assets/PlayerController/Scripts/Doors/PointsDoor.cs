using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsDoor : InterctableDoor
{
    [SerializeField] private TMP_Text text = null;

    public override InteractMessage GetInteractMessage()
    {
        return (int.Parse(text.text) >= 1000) ? new InteractMessage("Use", true) : new InteractMessage("Need 1000 points");
    }
}
