using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField] private Portal portal = null;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(portal.name);
        portal.Teleport();
    }

}
