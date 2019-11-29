using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : AEnemy
{

    [Header("Koopa")]
    [SerializeField] private GameObject shell = null;

    [HideInInspector] private bool shellSpawned;

    public override void DeathParticles()
    {
        base.DeathParticles();
        Instantiate(shell, transform.position+Vector3.up, Quaternion.identity);
    }

}
