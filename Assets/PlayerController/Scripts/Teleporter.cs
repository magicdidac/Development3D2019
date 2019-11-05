using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [HideInInspector] private FPSController player;
    [SerializeField] private Vector3 teleportOffset = Vector3.zero;

    private void Start()
    {
        player = GameManager.instance.player;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FPSController>())
        {
            player.TeleportTo(player.transform.position + teleportOffset);
            GameManager.instance.ChangeLevel();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255, 0, 0, .25f);
        Gizmos.DrawSphere(transform.position + teleportOffset, .5f);
        
        Gizmos.color = new Color(0, 255, 0, .25f);
        Gizmos.DrawSphere(transform.position, .5f);
    }

}
