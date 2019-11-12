using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDispenser : Interactable
{
    [SerializeField] private GameObject cube = null;
    [SerializeField] private Transform spawnPoint = null;

    public override bool CanInteract()
    {
        return true;
    }

    public override void Interact()
    {
        Instantiate(cube, spawnPoint.position, Quaternion.identity);
    }
}
