using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField] private Portal portal = null;
    [SerializeField] private float offset = .05f;

    [HideInInspector] private PlayerMovement player;
    [HideInInspector] public bool playerIsOverlapping = false;
    [HideInInspector] public bool playerTeleported = false;

    private void Start()
    {
        player = GameManager.instance.player;
    }

    private void Update()
    {
        if (!playerTeleported && playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.transform.position - portal.transform.position;

            if ((portal.transform.forward.z < 0f && portalToPlayer.z > -offset) || (portal.transform.forward.z > 0f && portalToPlayer.z < offset) || (portal.transform.forward.x < 0f && portalToPlayer.x > -offset) || (portal.transform.forward.x > 0f && portalToPlayer.x < offset))
            {
                portal.otherPortal.myTrigger.playerTeleported = true;
                player.TeleportToPortal(portal, offset, (portal.transform.forward.z != 0));
                
                playerIsOverlapping = false;
            }
        }/*else if (playerTeleported)
        {
            Vector3 portalToPlayer = player.transform.position - portal.transform.position;
            if ((portal.transform.forward.z < 0f && portalToPlayer.z < -offset*2) || (portal.transform.forward.z > 0f && portalToPlayer.z > offset*2) || (portal.transform.forward.x < 0f && portalToPlayer.x < -offset*2) || (portal.transform.forward.x > 0f && portalToPlayer.x > offset*2))
            {
                Debug.Log("YAS", gameObject);
                playerTeleported = false;
            }
        }*/

    }

    private void OnTriggerEnter(Collider other)
    {

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
            playerTeleported = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        Gizmos.DrawLine(portal.transform.position, portal.transform.position + (Vector3.forward * offset));

    }

}
