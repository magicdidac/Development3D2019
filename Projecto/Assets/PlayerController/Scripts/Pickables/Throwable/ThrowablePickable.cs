using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class ThrowablePickable : Pickable
{
    [HideInInspector] private Rigidbody rb;

    [Header("Throwable")]
    [SerializeField] private float force = 1;
    [SerializeField] private float maxDispersion = 2;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;

        rb.AddForce(new Vector3(Random.Range(-maxDispersion, maxDispersion), force, Random.Range(-maxDispersion, maxDispersion)), ForceMode.Impulse);

    }

    protected virtual void Update()
    {
        if (goToPlayer)
        {
            triggerCol.enabled = false;
            return;
        }

        if (rb.velocity.y < -0.1f)
            triggerCol.isTrigger = false;
        else
            triggerCol.isTrigger = true;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (goToPlayer)
            return;

        if (collision.gameObject.tag == "Untagged")
        {
            triggerCol.isTrigger = true;
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

}
