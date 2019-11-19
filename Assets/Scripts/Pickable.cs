using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{

    [SerializeField] private float force = 2;
    [SerializeField] private float minVelocity = 10f;


    [HideInInspector] private Rigidbody rb;
    [HideInInspector] private Animator anim;
    [HideInInspector] private Transform target = null;
    [HideInInspector] private bool colliding = false;
    [HideInInspector] private bool canDrop = true;
    [HideInInspector] private Vector3 lastFrameVelocity;

    [HideInInspector] private float size = 1;
    [HideInInspector] private Vector3 initialScale;

    [HideInInspector] private Vector3 forwardOffset;

    protected virtual void Start()
    {
        initialScale = transform.localScale;

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        lastFrameVelocity = rb.velocity;
    }

    protected virtual void FixedUpdate()
    {
        if (target == null)
            return;

        rb.velocity = ((target.position - transform.position) * Time.deltaTime * 1000);

        transform.forward = Vector3.MoveTowards(transform.forward, new Vector3(target.forward.x, 0,target.forward.z), Time.deltaTime * 5);

        if (canDrop && colliding && Vector3.Distance(target.position, transform.position) > 2)
        {
            Drop();
            GameManager.instance.player.gun.ResetTarget(this);
        }

    }

    public virtual void Pick(Transform target)
    {
        canDrop = false;
        Invoke("ResetCanDrop", .5f);
        rb.useGravity = false;
        rb.angularDrag = 3;

        forwardOffset = transform.forward;

        this.target = target;
    }

    public virtual void Throw()
    {
        rb.AddForce(target.forward * force, ForceMode.Impulse);

        rb.useGravity = true;
        rb.angularDrag = .05f;

        this.target = null;
    }

    public virtual void Drop()
    {
        rb.useGravity = true;
        rb.angularDrag = .05f;

        this.target = null;
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

    public void Bounce(Vector3 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

        rb.velocity = direction * Mathf.Max(speed, minVelocity);
    }

    public virtual void Dead()
    {
        Drop();

        GameManager.instance.player.gun.ResetTarget(this);

        rb.useGravity = false;

        rb.velocity = rb.velocity.normalized;

        GetComponent<Collider>().enabled = false;
        anim.SetTrigger("Change");

        GameManager.instance.audioManager.PlayAtPosition("Pickable-Dead", transform);
    }

    public void DestroyPickable()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Reset"))
        {
            Dead();
        }
    }

    public void TeleportToPortal(Portal portal, float offset)
    {
        if (target != null)
        {
            transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Gun");
            return;
        }

        float velocity = rb.velocity.magnitude;

        transform.position = GetOtherPortalPosition(portal, offset);

        Vector3 direction = portal.transform.InverseTransformDirection(-transform.forward);
        transform.forward = portal.otherPortal.transform.TransformDirection(direction);

        size = size * (portal.otherPortal.portalSize / portal.portalSize);

        if (size > 2)
            size = 2;

        if (size < .5f)
            size = .5f;

        transform.localScale = initialScale * size;

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


    public void ResetAfterPortalExit()
    {
        transform.GetChild(0).gameObject.layer = 0;
        transform.gameObject.layer = LayerMask.NameToLayer("Pickable");
    }
}
