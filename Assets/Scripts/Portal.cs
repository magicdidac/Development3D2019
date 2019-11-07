using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    [SerializeField] public Camera cam = null;
    [SerializeField] public Collider trigger = null;

    private float distance;
    [HideInInspector] public Collider wallCollider;
    [HideInInspector] private Portal otherPortal;
    [HideInInspector] private GameManager gm;

    private void Start()
    {
        gm = GameManager.instance;
    }

    private void Update()
    {
        if(otherPortal == null)
        {

            return;
        }

        ChangeCamera();

        CheckTeleport();
        
    }

    private void ChangeCamera()
    {

        Vector3 eulerAngles = transform.rotation.eulerAngles;
        Quaternion rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + 180, eulerAngles.z);
        Matrix4x4 worldMatrix = Matrix4x4.TRS(transform.position, rotation, transform.localScale);

        Vector3 reflectedPosition = worldMatrix.inverse.MultiplyPoint3x4(Camera.main.transform.position);
        Vector3 reflectedDirection = worldMatrix.inverse.MultiplyVector(Camera.main.transform.forward);
        otherPortal.cam.transform.position = otherPortal.transform.TransformPoint(reflectedPosition);
        otherPortal.cam.transform.forward = otherPortal.transform.TransformDirection(reflectedDirection);

        cam.nearClipPlane = Vector3.Distance(cam.transform.position, this.transform.position) + .5f;

    }

    private void CheckTeleport()
    {
        /*Transform player = gm.player.transform;
        distance = (player.position - transform.position).magnitude;

        if(distance < .4f)
        {
            Debug.Log(distance);
            gm.player.TeleportToPortal(otherPortal);
        }*/

    }

    public void Teleport()
    {
        otherPortal.trigger.enabled = false;
        gm.player.TeleportToPortal(otherPortal);
    }

    public void SetOtherPortal(Portal otherPortal)
    {
        this.otherPortal = otherPortal;
    }

    public Portal GetOtherPortal()
    {
        return otherPortal;
    }

    private void OnTriggerEnter(Collider other)
    {
        this.wallCollider.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(gameObject.name);
        this.wallCollider.enabled = true;
        trigger.enabled = true;
    }

}
