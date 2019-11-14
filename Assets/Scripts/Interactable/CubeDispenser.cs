using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDispenser : InteractableObject
{
    [SerializeField] private GameObject cube = null;
    [SerializeField] private Transform spawnPoint = null;

    private void Update()
    {
        CheckAllTriggers();
    }

    public override void Interact()
    {
        Instantiate(cube, spawnPoint.position, Quaternion.identity);

        DeactivateTriggers();

    }
}
