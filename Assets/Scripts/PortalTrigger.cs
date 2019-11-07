using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField] private Portal portal = null;
    [SerializeField] private float offset = .05f;

    [HideInInspector] private PlayerMovement player;
    [HideInInspector] public bool playerIsOverlapping = false;

    private void Start()
    {
        player = GameManager.instance.player;
    }

    private void Update()
    {
        /*if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.transform.position - portal.transform.position;
            float dotProduct = Vector3.Dot(portal.transform.up, portalToPlayer);

            if(dotProduct < 0f)
            {
                portal.otherPortal.myTrigger.playerIsOverlapping = false;
                /*
                float rotationDiff = -Quaternion.Angle(portal.transform.rotation, portal.otherPortal.transform.rotation);
                rotationDiff += portal.transform.eulerAngles.y - portal.otherPortal.transform.eulerAngles.y;
                //player.transform.Rotate(Vector3.up, rotationDiff);

                Vector3 direction = portal.otherPortal.transform.InverseTransformDirection(-transform.forward);
                transform.forward = portal.transform.TransformDirection(direction);

                Vector3 positionOffset = Quaternion.Euler(0, rotationDiff, 0) * portalToPlayer;
                player.transform.position = portal.otherPortal.transform.position + positionOffset;
                *//*
                Debug.Log(portal.cam.transform.position.y + " <-> " + Camera.main.transform.position.y);
                player.TeleportToPortal(portal.otherPortal);

                playerIsOverlapping = false;
            }
        }*/

        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.transform.position - portal.transform.position;
            

            if((portal.transform.forward.z < 0f && portalToPlayer.z > -offset) || (portal.transform.forward.z > 0f && portalToPlayer.z < offset))
            {
                player.TeleportToPortal(portal, (portal.transform.forward.z < 0f)? -offset : offset);

                playerIsOverlapping = false;
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        /*Debug.Log(portal.name);
        portal.Teleport();*/

        if (other.tag == "Player")
        {
            playerIsOverlapping = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        Gizmos.DrawLine(portal.transform.position, portal.transform.position + (Vector3.forward * offset));

    }

}
