using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Checkpoint : MonoBehaviour
{
    [HideInInspector] private Renderer rend;
    [HideInInspector] private GameManager gm;

    [SerializeField] private Material offMaterial = null;
    [SerializeField] private Material onMaterial = null;

    [HideInInspector] public Quaternion playerRotation;

    private void Start()
    {
        this.tag = "Checkpoint";

        gm = GameManager.instance;

        rend = GetComponent<Renderer>();

        if (!Application.isEditor)
            rend.enabled = false;
    }

    public void EnableCheckpoint(FPSController player)
    {
        if (gm.checkpoint != null)
            gm.checkpoint.DisableCheckpoint();

        gm.checkpoint = this;

        playerRotation = player.transform.rotation;

        if(Application.isEditor)
            rend.material = onMaterial;
    }

    public void DisableCheckpoint()
    {
        if (Application.isEditor)
            rend.material = offMaterial;
    }

    public void SetPlayerStats()
    {

        gm.player.dmgShield.shield = gm.player.dmgShield.maxShield;
        gm.player.dmgShield.health = gm.player.dmgShield.maxHealth;

        Invoke("ChangePosPlayer", 1f);
    }

    private void ChangePosPlayer()
    {
        gm.player.TeleportTo(transform.position);

        /*gm.player.characterController.enabled = false;
        gm.player.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        gm.player.transform.rotation = playerRotation; // Doesn't work
        gm.player.characterController.enabled = true;*/

        gm.uiController.UnFade();
    }

}
