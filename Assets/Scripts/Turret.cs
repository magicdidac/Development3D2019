using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Turret : Pickable
{

    [SerializeField] private LineRenderer lineR = null;

    [HideInInspector] private bool isActive = true;

    [HideInInspector] private GameManager gm;

    protected override void Start()
    {
        base.Start();
        gm = GameManager.instance;
    }

    private void Update()
    {
        if (!isActive)
        {
            lineR.enabled = false;
            return;
        }

        lineR.enabled = true;

        RaycastHit hit;

        if (Physics.Raycast(lineR.transform.position, lineR.transform.forward, out hit))
        {
            lineR.SetPosition(0, lineR.transform.position);
            lineR.SetPosition(1, hit.point);

            if (hit.transform.GetComponent<PlayerMovement>())
                GameManager.instance.Dead();

            if (hit.transform.GetComponent<Turret>())
                hit.transform.GetComponent<Turret>().Dead();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Pickable>())
        {
            gm.audioManager.PlaySoundOfCollectionAtPosition("Turret-Die", transform.position);
            isActive = false;
        }

    }

    public override void Pick(Transform target)
    {
        base.Pick(target);
        gm.audioManager.PlaySoundOfCollectionAtPosition("Turret-Pick", transform);

    }

    public override void Drop()
    {
        base.Drop();
        gm.audioManager.PlaySoundOfCollectionAtPosition("Turret-Drop", transform);
    }

    public override void Throw()
    {
        base.Throw();
        gm.audioManager.PlaySoundOfCollectionAtPosition("Turret-Throw", transform);
    }

    public override void Dead()
    {
        base.Dead();
        Destroy(transform.GetComponentInChildren<AudioSource>().gameObject);
        isActive = false;
        gm.audioManager.PlaySoundOfCollectionAtPosition("Turret-Die", transform.position);
    }


}
