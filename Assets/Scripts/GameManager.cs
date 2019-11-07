using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [HideInInspector] public static GameManager instance { get; private set; }

    [SerializeField] public PlayerMovement player = null;

    [HideInInspector] private Portal bluePortal;
    [HideInInspector] private Portal orangePortal;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
    }

    public void ChangeBluePortal(Portal newPortal, Collider col)
    {
        if(bluePortal != null)
            Destroy(bluePortal.gameObject);

        bluePortal = newPortal;
        bluePortal.wallCollider = col;

        if (orangePortal != null)
        {
            orangePortal.SetOtherPortal(bluePortal);
            bluePortal.SetOtherPortal(orangePortal);
        }
    }

    public void ChangeOrangePortal(Portal newPortal, Collider col)
    {
        if(orangePortal != null)
            Destroy(orangePortal.gameObject);

        orangePortal = newPortal;
        orangePortal.wallCollider = col;

        if (bluePortal != null)
        {
            bluePortal.SetOtherPortal(orangePortal);
            orangePortal.SetOtherPortal(bluePortal);
        }
    }

}
