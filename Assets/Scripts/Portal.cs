using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    [SerializeField] public Camera cam = null;
    [SerializeField] public Collider trigger = null;
    [SerializeField] public PortalTrigger myTrigger = null;
    [SerializeField] private GameObject playerMock = null;

    private float distance;
    [HideInInspector] public Collider wallCollider;
    [HideInInspector] public Portal otherPortal;
    [HideInInspector] private GameManager gm;
    [HideInInspector] private Transform playerCamera;
    [HideInInspector] private Transform mockTransform;
    [HideInInspector] public bool showMock = false;

    private void Start()
    {
        gm = GameManager.instance;
        playerCamera = Camera.main.transform;
    }

    /*private void ChangeMock()
    {
        float xDistance = gm.player.transform.position.x - otherPortal.transform.position.x;
        float yDistance = gm.player.transform.position.y - otherPortal.transform.position.y;
        float zDistance = gm.player.transform.position.z - otherPortal.transform.position.z;


        xDistance = -xDistance;

        zDistance = -zDistance;

        Vector3 newPosition = transform.position + new Vector3(xDistance, yDistance, zDistance);

        Debug.Log(newPosition);

        mockTransform.position = newPosition;

    }*/

    public void SetOtherPortal(Portal otherPortal)
    {
        this.otherPortal = otherPortal;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (otherPortal == null)
            return;

        this.wallCollider.enabled = false;
        otherPortal.showMock = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (otherPortal == null)
            return;

        this.wallCollider.enabled = true;
        otherPortal.showMock = false;
    }

}
