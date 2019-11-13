using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Sprite emptyCrosshair = null;
    [SerializeField] private Sprite blueCrosshair = null;
    [SerializeField] private Sprite orangeCrosshair = null;
    [SerializeField] private Sprite bothCrosshair = null;
    [Space]
    [SerializeField] private Image crosshair = null;

    [HideInInspector] private GameManager gm;

    private void Start()
    {
        gm = GameManager.instance;

        gm.uiManager = this;
    }

    public void ReloadUI()
    {
        ReloadCrosshair();
    }


    private void ReloadCrosshair()
    {
        if (gm.bluePortal == null && gm.orangePortal == null)
            crosshair.sprite = emptyCrosshair;
        else if (gm.bluePortal != null && gm.orangePortal == null)
            crosshair.sprite = blueCrosshair;
        else if (gm.bluePortal == null && gm.orangePortal != null)
            crosshair.sprite = orangeCrosshair;
        else
            crosshair.sprite = bothCrosshair;
    }

}
