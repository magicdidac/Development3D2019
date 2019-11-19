using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Elevator : MonoBehaviour
{
    [SerializeField] private bool isReciver = false;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private Transform nextElevatorTransform = null;

    [HideInInspector] private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isOpen", isOpen);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            bool areOpen = isOpen;

            if (!isReciver)
            {
                if (isOpen)
                {
                    isOpen = false;
                    Invoke("GoToNext", 3f);
                }
                else
                    isOpen = true;
            }
            else
                isOpen = true;

            if (areOpen != isOpen)
            {
                GameManager.instance.audioManager.PlayAtPosition((isOpen) ? "Door-Open" : "Door-Close", transform);

                if (!isOpen)
                    Invoke("PlayLoop", .55f);
                else
                    GameManager.instance.audioManager.StopSound("Elevator-Loop");
            }

            anim.SetBool("isOpen", isOpen);
        }
    }

    private void PlayLoop()
    {
        GameManager.instance.audioManager.Play("Elevator-Loop");
    }


    private void GoToNext()
    {
        if (!nextElevatorTransform)
        {
            GameManager.instance.GoToNextRoom();
            return;
        }

        Transform player = GameManager.instance.player.transform;

        Vector3 offset = player.position - transform.position;

        Vector3 nextPlayerPosition = nextElevatorTransform.position + offset;

        GameManager.instance.player.TeleportTo(nextPlayerPosition);

    }

    private void OnDrawGizmos()
    {
        if (nextElevatorTransform != null)
            Gizmos.DrawLine(transform.position, nextElevatorTransform.position);

    }

}
