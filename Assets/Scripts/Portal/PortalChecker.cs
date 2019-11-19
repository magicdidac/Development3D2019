using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalChecker : MonoBehaviour
{
    [SerializeField] private List<Transform> points = new List<Transform>();

    public void ChangeSize(float size)
    {
        transform.localScale = Vector3.one * size;
    }

    public bool CanPlace()
    {
        foreach(Transform t in points)
        {
            RaycastHit hit;

            Vector3 initialRayPosition = transform.position + (transform.forward * .3f);

            if (Physics.Raycast(initialRayPosition, (t.position - initialRayPosition).normalized, out hit))
            {
                Debug.DrawLine(initialRayPosition, hit.point);

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
