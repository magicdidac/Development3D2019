using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{

    [SerializeField] private Portal portal = null;

    [HideInInspector] private Transform playerCamera;

    private void Start()
    {
        playerCamera = Camera.main.transform;
    }

    private void Update()
    {
        if (portal.otherPortal == null)
            return;

        /*Vector3 playerOffsetFromPortal = playerCamera.position - portal.otherPortal.transform.position;
        transform.position = portal.transform.position - playerOffsetFromPortal;

        float anglularDifferenceBetweenPortalRotation = Quaternion.Angle(portal.transform.rotation, portal.otherPortal.transform.rotation);

        Quaternion portalRotationalDifference = Quaternion.AngleAxis(anglularDifferenceBetweenPortalRotation, Vector3.up);
        Vector3 newCameraDiraction = portalRotationalDifference * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDiraction, Vector3.up);
        portal.otherPortal.cam.nearClipPlane = Mathf.Abs(portal.otherPortal.cam.transform.localPosition.z);

        /*float zDistance = playerCamera.transform.position.z - portal.transform.position.z;
        float xDistance = playerCamera.transform.position.x - portal.transform.position.x;

        float angle = Mathf.Atan(zDistance / xDistance);

        portal.otherPortal.cam.transform.eulerAngles = new Vector3(portal.otherPortal.cam.transform.eulerAngles.x, 180 + angle, portal.otherPortal.cam.transform.eulerAngles.z);
        portal.otherPortal.cam.transform.position = portal.otherPortal.transform.position - (Vector3.forward * zDistance);

        //portal.otherPortal.cam.nearClipPlane = Vector3.Distance(portal.otherPortal.cam.transform.position, portal.otherPortal.transform.position);
        portal.otherPortal.cam.nearClipPlane = Mathf.Abs(portal.otherPortal.cam.transform.localPosition.z);*/
    }

}
