using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [HideInInspector] public static GameManager instance { get; private set; }

    [HideInInspector] private GameObject bluePortal;
    [HideInInspector] private GameObject orangePortal;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
    }

    public void ChangeBluePortal(GameObject newPortal)
    {
        if(bluePortal != null)
            Destroy(bluePortal);

        bluePortal = newPortal;
    }

    public void ChangeOrangePortal(GameObject newPortal)
    {
        if(orangePortal != null)
            Destroy(orangePortal);

        orangePortal = newPortal;
    }

}
