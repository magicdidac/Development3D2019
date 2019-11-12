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

        Vector3 eulerAngles = portal.transform.rotation.eulerAngles;
        Quaternion rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + 180, eulerAngles.z);
        Matrix4x4 worldMatrix = Matrix4x4.TRS(portal.transform.position, rotation, portal.transform.localScale);

        Vector3 reflectedPosition = worldMatrix.inverse.MultiplyPoint3x4(playerCamera.position);
        Vector3 reflectedDirection = worldMatrix.inverse.MultiplyVector(playerCamera.forward);
        portal.otherPortal.cam.transform.position = portal.otherPortal.transform.TransformPoint(reflectedPosition);
        portal.otherPortal.cam.transform.forward = portal.otherPortal.transform.TransformDirection(reflectedDirection);
        

        portal.cam.nearClipPlane = Vector3.Distance(portal.cam.transform.position, portal.transform.position) + .1f;

    }

}
