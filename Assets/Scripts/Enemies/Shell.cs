using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Shell : MonoBehaviour
{

    [SerializeField] private float throwForce = 5;
    [SerializeField] private Collider col = null;

    [HideInInspector] private float lastPlayerHit;
    [HideInInspector] private Rigidbody rb;
    [HideInInspector] private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetFloat("Speed", GetSpeed());
        //rb.velocity = new Vector3(rb.velocity.x, -1, rb.velocity.z);
    }

    private void OnTriggerStay(Collider other)
    {
        /*if(transform.parent == null && other.GetComponent<PlayerController>() && Input.GetButtonDown("Interact"))
        {
            transform.parent = other.GetComponent<PlayerController>().TakeShell(this);
            transform.localPosition = Vector3.zero;
            rb.isKinematic = true;
            col.enabled = false;
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (transform.parent == null && collision.gameObject.GetComponent<AEnemy>() && GetSpeed() > .1f)
        {
            collision.gameObject.GetComponent<AEnemy>().Die();
        }


        if (transform.parent == null && collision.gameObject.GetComponent<PlayerController>() && GetSpeed() > .1f && Time.time > lastPlayerHit + 1)
        {
            lastPlayerHit = Time.time;
            collision.gameObject.GetComponent<PlayerController>().Hit();
            return;
        }
            
        if (transform.parent == null && collision.gameObject.GetComponent<PlayerController>() && GetSpeed() <= .1f)
        {
            Throw();
        }

    }

    private float GetSpeed()
    {
        return Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z);
    }

    public void Take(Transform parent)
    {
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
        rb.isKinematic = true;
        col.enabled = false;
    }

    public void Throw()
    {
        Vector3 forward = transform.forward;

        if (transform.parent != null)
            forward = transform.parent.forward;

        lastPlayerHit = Time.time-.75f;
        transform.parent = null;
        col.enabled = true;
        rb.isKinematic = false;
        rb.AddForce(forward * throwForce, ForceMode.Impulse);
    }

}
