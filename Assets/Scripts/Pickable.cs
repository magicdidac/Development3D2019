using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{

    [SerializeField] private float force = 2;

    [HideInInspector] private Rigidbody rb;
    [HideInInspector] private Transform target = null;
    [HideInInspector] private bool colliding = false;
    [HideInInspector] private bool canDrop = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (target == null)
            return;

        rb.MovePosition(target.position);

        transform.forward = Vector3.MoveTowards(transform.forward, target.forward, 5);

        if (canDrop && colliding && Vector3.Distance(target.position, transform.position) > .37f)
        {
            SetTarget(null);
        }

    }

    private void GetOutOfHere()
    {
        SetTarget(null);

        rb.AddForce(transform.forward * force, ForceMode.Impulse);

    }

    public void SetTarget(Transform target)
    {
        if(this.target == target)
        {
            GetOutOfHere();
            return;
        }

        if(target != null)
        {
            canDrop = false;
            Invoke("ResetCanDrop", .5f);
            this.target = target;
            rb.useGravity = false;
            rb.angularDrag = 3;
        }
        else
        {
            this.target = null;
            rb.useGravity = true;
            rb.angularDrag = .05f;
        }
    }

    private void ResetCanDrop()
    {
        canDrop = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        colliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        colliding = false;
    }

}
