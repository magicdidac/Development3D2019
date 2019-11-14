using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField] private Portal portal = null;
    [SerializeField] private float offset = .05f;
    [SerializeField] private string playerTag = "Player";

    [HideInInspector] private PlayerMovement player;
    [HideInInspector] private Pickable pickable;
    [HideInInspector] public bool playerIsOverlapping = false;
    [HideInInspector] public bool pickableIsOverlapping = false;

    private void Start()
    {
        player = GameManager.instance.player;
    }

    private void Update()
    {
        if (playerIsOverlapping && portal.otherPortal != null)
        {
            if (HasPlayerCrossed())
            {
                player.TeleportToPortal(portal, offset);
                portal.wallCollider.enabled = true;
                playerIsOverlapping = false;
            }
            return;
        }

        if (pickableIsOverlapping && portal.otherPortal != null)
        {
            if (HasPickableCrossed())
            {
                pickable.TeleportToPortal(portal, offset);
                portal.wallCollider.enabled = true;
                pickableIsOverlapping = false;
            }
        }
    }

    private bool HasPlayerCrossed()
    {
        Vector3 forward = portal.transform.forward;
        Vector3 distance = portal.transform.position - player.transform.position;

        if(!Utilities.IsForwarNearToZero(forward.z))
        {
            if (forward.z < 0 && distance.z <= offset)
                return true;
            else if (forward.z > 0 && distance.z >= -offset)
                return true;
            else
                return false;
        }else if(!Utilities.IsForwarNearToZero(forward.x))
        {
            if (forward.x < 0 && distance.x <= offset)
                return true;
            else if (forward.x > 0 && distance.x >= -offset)
                return true;
            else
                return false;
        }

        throw new System.Exception("The forward has not been contemplated.");
    }

    private bool HasPickableCrossed()
    {
        Vector3 forward = portal.transform.forward;
        Vector3 distance = portal.transform.position - pickable.transform.position;

        if (!Utilities.IsForwarNearToZero(forward.z))
        {
            if (forward.z < 0 && distance.z <= offset)
                return true;
            else if (forward.z > 0 && distance.z >= -offset)
                return true;
            else
                return false;
        }
        else if (!Utilities.IsForwarNearToZero(forward.x))
        {
            if (forward.x < 0 && distance.x <= offset)
                return true;
            else if (forward.x > 0 && distance.x >= -offset)
                return true;
            else
                return false;
        }

        throw new System.Exception("The forward has not been contemplated.");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(playerTag))
            playerIsOverlapping = true;

        if (other.GetComponent<Pickable>())
        {
            pickable = other.GetComponent<Pickable>();
            pickableIsOverlapping = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals(playerTag))
            playerIsOverlapping = false;

        if (other.GetComponent<Pickable>())
        {
            pickable = null;
            pickableIsOverlapping = false;
        }

    }

}
