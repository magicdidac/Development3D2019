using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour
{
    [SerializeField] private GameObject bluePortalPrefab = null;
    [SerializeField] private GameObject orangePortalPrefab = null;
    [SerializeField] private GameObject portalChecker = null;
    [SerializeField] private GameObject bluePreview = null;
    [SerializeField] private GameObject orangePreview = null;
    [SerializeField] private Transform objectsTarget = null;

    [HideInInspector] private GameManager gm;
    [HideInInspector] private Transform preview;
    [HideInInspector] private PortalChecker checker;
    [HideInInspector] private bool canCreatePortals = true;

    [HideInInspector] private Pickable pickable;

    private void Start()
    {
        gm = GameManager.instance;
    }

    private void FixedUpdate()
    {
        if (!canCreatePortals)
            return;

        if (Input.GetButton("Fire1"))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                if (hit.transform.tag.Equals("Printable"))
                    MovePreview(hit, bluePreview);
            }
        }

        if (Input.GetButton("Fire2"))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                if (hit.transform.tag.Equals("Printable"))
                    MovePreview(hit, orangePreview);
            }
        }

    }

    private void Update()
    {

        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5))
            {
                if (hit.transform.tag.Equals("Interactable"))
                {
                    Interactable i = hit.transform.GetComponent<Interactable>();
                    if (i.CanInteract())
                    {
                        i.Interact();
                    }
                }
            }
        }

        if (canCreatePortals && Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;

            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5))
            {
                if (hit.transform.tag.Equals("Pickable"))
                {
                    pickable = hit.transform.GetComponent<Pickable>();
                    pickable.Pick(objectsTarget);
                    canCreatePortals = false;
                    return;
                }
            }

        }

        if(!canCreatePortals && Input.GetButtonDown("Fire1"))
        {
            pickable.Throw();
            ResetTarget();
            return;
        }

        if (!canCreatePortals && Input.GetButtonDown("Fire2"))
        {
            pickable.Drop();
            ResetTarget();
            return;
        }

        if (!canCreatePortals)
            return;

        if (Input.GetButtonUp("Fire1"))
        {
            RaycastHit hit;

            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                if (hit.transform.tag.Equals("Printable"))
                {
                    gm.ChangeBluePortal(CreatePortal(bluePortalPrefab, hit), hit.transform.GetComponent<Collider>());
                }
            }

        }

        if (Input.GetButtonUp("Fire2"))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                if (hit.transform.tag.Equals("Printable"))
                {
                    gm.ChangeOrangePortal(CreatePortal(orangePortalPrefab, hit), hit.transform.GetComponent<Collider>());
                }
            }

        }
    }

    public void ResetTarget()
    {
        pickable = null;
        canCreatePortals = true;
    }

    private void MovePreview(RaycastHit hit, GameObject previewPrefab)
    {
        CreateChecker(hit);

        if (!checker.CanPlace()) {
            if (preview != null)
                Destroy(preview.gameObject);

            return;
        }
        
        if(preview == null)
            preview = Instantiate(previewPrefab, hit.point, Quaternion.identity).transform;

        preview.position = hit.point;
        preview.forward = hit.normal;


    }

    private Portal CreatePortal(GameObject portalPrefab, RaycastHit hit)
    {
        if (preview != null)
            Destroy(preview.gameObject);

        CreateChecker(hit);

        if (!checker.CanPlace())
        {
            if (checker != null)
                Destroy(checker.gameObject);

            return null;
        }

        if (checker != null)
            Destroy(checker.gameObject);

        GameObject portal = Instantiate(portalPrefab, hit.point, Quaternion.identity);

        portal.transform.forward = hit.normal;

        if (hit.normal.y != 0)
            portal.transform.eulerAngles += Vector3.forward * gm.player.transform.eulerAngles.y;

        return portal.GetComponent<Portal>();
    }

    private void CreateChecker(RaycastHit hit)
    {
        if (checker == null)
            checker = Instantiate(portalChecker, hit.point, Quaternion.identity).GetComponent<PortalChecker>();

        checker.transform.position = hit.point;
        checker.transform.forward = hit.normal;
    }

}
