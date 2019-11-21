using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerController : MonoBehaviour
{
    [HideInInspector] private PlayerMovementController playerMovement = null;

    [SerializeField] private PlayerCameraController playerCamera = null;
    [SerializeField] private Animator anim = null;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovementController>();
        playerMovement.Initialice(anim);
    }

    /** Update **/

    private void Update()
    {
        playerMovement.Move();
    }

    /** Punchs **/

    public void EnableLeftHandPunch(bool enableHandPunch)
    {

    }

    public void EnableRightHandPunch(bool enableHandPunch)
    {

    }

    public void EnableKick(bool enableHandPunch)
    {

    }
}
