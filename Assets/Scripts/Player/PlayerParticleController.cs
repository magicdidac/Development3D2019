using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleController : MonoBehaviour
{

    [SerializeField] private ParticleSystem runParticles = null;

    [HideInInspector] private PlayerController player;

    private void LateUpdate()
    {

        if (player == null)
            player = GameManager.instance.player;


        switch (player.GetCurrentState())
        {
            case "RunState":
                if (!runParticles.isEmitting)
                {
                    runParticles.Play();
                }
                break;
            default:
                if (runParticles.isPlaying)
                {
                    runParticles.Stop();
                }
                break;
        }

    }

}
