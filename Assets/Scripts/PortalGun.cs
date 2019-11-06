using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour
{
    [SerializeField] private GameObject blueParticles = null;
    [SerializeField] private GameObject orangeParticles = null;
    [SerializeField] private GameObject bluePortalPrefab = null;
    [SerializeField] private GameObject orangePortalPrefab = null;

    [HideInInspector] private GameManager gm;

    private void Start()
    {
        gm = GameManager.instance;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;

            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                if (hit.transform.tag.Equals("Printable"))
                {
                    Destroy(Instantiate(blueParticles, hit.point, Quaternion.identity), .5f);
                    gm.ChangeBluePortal(CreatePortal(bluePortalPrefab, hit));
                }
            }

        }

        if (Input.GetButtonDown("Fire2"))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                if (hit.transform.tag.Equals("Printable"))
                {

                    Destroy(Instantiate(orangeParticles, hit.point, Quaternion.identity), .5f);
                    gm.ChangeOrangePortal(CreatePortal(orangePortalPrefab, hit));
                }
            }

        }
    }

    private GameObject CreatePortal(GameObject portalPrefab, RaycastHit hit)
    {
        GameObject portal = Instantiate(portalPrefab, hit.point, Quaternion.identity);

        portal.transform.forward = hit.normal;

        return portal;
    }

}
