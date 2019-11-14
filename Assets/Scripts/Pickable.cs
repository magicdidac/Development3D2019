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

    protected virtual void FixedUpdate()
    {
        if (target == null)
            return;

        rb.velocity = ((target.position - transform.position) * Time.deltaTime * 1000);

        transform.forward = Vector3.MoveTowards(transform.forward, new Vector3(target.forward.x, 0, target.forward.z), Time.deltaTime * 5);

        if (canDrop && colliding && Vector3.Distance(target.position, transform.position) > 2)
        {
            SetTarget(null);
            GameManager.instance.player.gun.ResetTarget();
        }

    }

    private void GetOutOfHere()
    {
        SetTarget(null);

        rb.AddForce(transform.forward * force, ForceMode.Impulse);

    }

    public virtual void Pick(Transform target)
    {
        SetTarget(target);
    }

    public virtual void Throw()
    {
        SetTarget(this.target);
    }

    public virtual void Drop()
    {
        SetTarget(null);
    }


    private void SetTarget(Transform target)
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

    public void TeleportToPortal(Portal portal, float offset)
    {
        float velocity = rb.velocity.magnitude;

        transform.position = GetOtherPortalPosition(portal, offset);

        Vector3 direction = portal.transform.InverseTransformDirection(-transform.forward);
        transform.forward = portal.otherPortal.transform.TransformDirection(direction);

        rb.velocity = transform.forward * velocity;

        portal.otherPortal.showMock = false;
    }

    private Vector3 GetOtherPortalPosition(Portal portal, float offset)
    {
        Vector3 otherForward = portal.otherPortal.transform.forward;
        Vector3 other = portal.otherPortal.transform.position;

        offset += .01f;

        Vector3 l_Position = portal.transform.InverseTransformPoint(transform.position);
        Vector3 returnPosition = portal.otherPortal.transform.TransformPoint(l_Position);

        if (!Utilities.IsForwarNearToZero(otherForward.z))
            returnPosition.z = other.z + (otherForward.z * offset);
        else if (!Utilities.IsForwarNearToZero(otherForward.x))
            returnPosition.x = other.x + (otherForward.x * offset);

        return returnPosition;
    }

}
