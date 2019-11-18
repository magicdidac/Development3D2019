using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDispenser : InteractableObject
{
    [SerializeField] private GameObject cube = null;
    [SerializeField] private Transform spawnPoint = null;

    [HideInInspector] private Pickable cubeInstance = null;

    private void Update()
    {
        CheckAllTriggers();
    }

    public override void Interact()
    {
        if(cubeInstance != null)
        {
            cubeInstance.Dead();
        }

        cubeInstance = Instantiate(cube, spawnPoint.position, Quaternion.identity).GetComponent<Pickable>();

        DeactivateTriggers();

    }
}
