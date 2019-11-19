using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerTriggers : MonoBehaviour
{

    [HideInInspector] private PlayerMovement player;

    private void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Kill"))
            GameManager.instance.Dead();

        if (other.tag.Equals("Reset"))
            GameManager.instance.DeletePortals();

    }

}
