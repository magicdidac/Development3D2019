using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [HideInInspector] public static GameManager instance { get; private set; }

    [SerializeField] public PlayerMovement player = null;

    [HideInInspector] public Portal bluePortal { get; private set; }
    [HideInInspector] public Portal orangePortal { get; private set; }
    [HideInInspector] public UIManager uiManager;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
    }

    public void ChangeBluePortal(Portal newPortal, Collider col)
    {
        if (newPortal == null)
            return;

        if (bluePortal != null)
        {
            bluePortal.trigger.enabled = true;
            Destroy(bluePortal.gameObject);
        }

        bluePortal = newPortal;
        bluePortal.wallCollider = col;

        if (orangePortal != null)
        {
            orangePortal.SetOtherPortal(bluePortal);
            bluePortal.SetOtherPortal(orangePortal);
        }

        uiManager.ReloadUI();

    }

    public void ChangeOrangePortal(Portal newPortal, Collider col)
    {
        if (newPortal == null)
            return;

        if (orangePortal != null)
        {
            orangePortal.trigger.enabled = true;
            Destroy(orangePortal.gameObject);
        }

        orangePortal = newPortal;
        orangePortal.wallCollider = col;

        if (bluePortal != null)
        {
            bluePortal.SetOtherPortal(orangePortal);
            orangePortal.SetOtherPortal(bluePortal);
        }

        uiManager.ReloadUI();

    }

    public void DeletePortals()
    {
        if(bluePortal != null)
            bluePortal.DeletePortal();
        if(orangePortal != null)
            orangePortal.DeletePortal();

        bluePortal = null;
        orangePortal = null;

        uiManager.ReloadUI();

    }

}
