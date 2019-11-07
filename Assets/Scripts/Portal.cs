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

    private void Update()
    {
        if(otherPortal == null)
        {

            return;
        }

        ChangeCamera();       

    }

    private void FixedUpdate()
    {
        if (showMock)
        {
            if (mockTransform == null)
                mockTransform = Instantiate(playerMock, transform.position, Quaternion.identity).transform;

            ChangeMock();

        }
        else if (mockTransform != null)
            Destroy(mockTransform.gameObject);
    }

    private void ChangeMock()
    {
        float xDistance = gm.player.transform.position.x - otherPortal.transform.position.x;
        float yDistance = gm.player.transform.position.y - otherPortal.transform.position.y;
        float zDistance = gm.player.transform.position.z - otherPortal.transform.position.z;


        xDistance = -xDistance;

        zDistance = -zDistance;

        Vector3 newPosition = transform.position + new Vector3(xDistance, yDistance, zDistance);

        Debug.Log(newPosition);

        mockTransform.position = newPosition;

    }

    private void ChangeCamera()
    {

        Vector3 eulerAngles = transform.rotation.eulerAngles;
        Quaternion rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + 180, eulerAngles.z);
        Matrix4x4 worldMatrix = Matrix4x4.TRS(transform.position, rotation, transform.localScale);

        Vector3 reflectedPosition = worldMatrix.inverse.MultiplyPoint3x4(playerCamera.position);
        Vector3 reflectedDirection = worldMatrix.inverse.MultiplyVector(playerCamera.forward);
        otherPortal.cam.transform.position = otherPortal.transform.TransformPoint(reflectedPosition);
        otherPortal.cam.transform.position = new Vector3(otherPortal.cam.transform.position.x, playerCamera.position.y, otherPortal.cam.transform.position.z);
        otherPortal.cam.transform.forward = otherPortal.transform.TransformDirection(reflectedDirection);

        cam.nearClipPlane = Vector3.Distance(cam.transform.position, this.transform.position) + .5f;

    }

    public void SetOtherPortal(Portal otherPortal)
    {
        this.otherPortal = otherPortal;
    }

    private void OnTriggerEnter(Collider other)
    {
        this.wallCollider.enabled = false;
        otherPortal.showMock = true;
    }

    private void OnTriggerExit(Collider other)
    {
        this.wallCollider.enabled = true;
        otherPortal.showMock = false;
    }

}
