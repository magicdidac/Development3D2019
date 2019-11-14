using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Pickable
{

    [SerializeField] private LineRenderer lineR = null;

    [HideInInspector] private bool isActive = true;

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
                hit.transform.GetComponent<PlayerMovement>().Dead();

            if (hit.transform.GetComponent<Turret>())
                hit.transform.GetComponent<Turret>().Dead();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Pickable>())
            isActive = false;

    }

}
