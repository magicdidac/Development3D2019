using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadWalls : MonoBehaviour
{
    [HideInInspector] private AudioManager audioManager;
    [SerializeField] private Transform audioSpawn = null;

    private void Start()
    {
        audioManager = GameManager.instance.audioManager;
    }


    public void PlayImpact()
    {
        audioManager.PlaySoundAtPosition("DeadWallImpact", (audioSpawn != null) ? audioSpawn.position : transform.position);
    }

}
