using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundButton : MonoBehaviour
{

    [SerializeField] private Interactable objectToInteract = null;
    [SerializeField] private Interactable path = null;

    private void OnTriggerEnter(Collider other)
    {
        objectToInteract.Interact();
        path.Interact();

    }

    private void OnTriggerExit(Collider other)
    {
        objectToInteract.Interact();
        path.Interact();
    }

}
