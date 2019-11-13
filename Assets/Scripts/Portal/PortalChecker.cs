using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalChecker : MonoBehaviour
{
    [SerializeField] private List<Transform> points = new List<Transform>();
    [SerializeField] private Transform initialPoint = null;

    public bool CanPlace()
    {
        foreach(Transform t in points)
        {
            RaycastHit hit;

            if (Physics.Raycast(initialPoint.position, (t.position - initialPoint.position).normalized, out hit))
            {
                if (!hit.transform.tag.Equals("Printable"))
                    return false;

                if (hit.normal != transform.forward)
                    return false;

            }
            else
                return false;

        }

        return true;
    }

}
