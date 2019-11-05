using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : Door
{

    [Header("Automatic Door")]

    [SerializeField] private Vector3 detectionOffset = Vector3.zero;

    protected override void Start()
    {
        base.Start();

        BoxCollider b = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        b.isTrigger = true;
        b.size = door.localScale + detectionOffset;

    }

    public override InteractMessage GetInteractMessage()
    {
        return new InteractMessage(false);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(closePosition, door.localScale + detectionOffset);

    }

}
