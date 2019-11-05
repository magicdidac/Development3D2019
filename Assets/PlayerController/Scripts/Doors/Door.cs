using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Door : InteractableObject
{

    [HideInInspector] protected bool isOpened = false;

    [Header("Door")]
    [SerializeField] protected Vector3 openPositiom = Vector3.zero;
    [HideInInspector] protected Vector3 closePosition = Vector3.zero;

    [SerializeField] protected float speed = 2;

    [HideInInspector] protected Transform door;

    protected virtual void Start()
    {
        this.selfType = Type.Door;
        this.door = transform.GetChild(0);
        closePosition = door.localPosition;
    }

    public virtual void UseDoor()
    {
        GameManager.instance.audioManager.PlaySound("Door");
        isOpened = !isOpened;
    }

    private void FixedUpdate()
    {
        if (isOpened && door.localPosition != closePosition + openPositiom)
            door.localPosition = Vector3.MoveTowards(door.localPosition, closePosition + openPositiom, speed * Time.deltaTime);
        else if (!isOpened && door.localPosition != closePosition)
            door.localPosition = Vector3.MoveTowards(door.localPosition, closePosition, speed * Time.deltaTime);
    }

    protected virtual void OnDrawGizmos()
    {
        if (door == null)
            door = transform.GetChild(0);

        if (!Application.isPlaying)
            closePosition = door.localPosition;


        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.color = new Color(0, 255, 0, .5f);

        Gizmos.DrawCube(closePosition, door.localScale * 1.1f);

        if (openPositiom == Vector3.zero)
            return;

        Gizmos.color = new Color(255, 0, 0, .5f);


        Gizmos.DrawCube(closePosition + openPositiom, door.localScale * 1.1f);

    }

    public override void Interact()
    {
        UseDoor();
    }
}
