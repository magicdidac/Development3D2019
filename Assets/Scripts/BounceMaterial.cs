using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceMaterial : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Pickable>())
        {
            collision.transform.GetComponent<Pickable>().Bounce(collision.contacts[0].normal);
        }
    }

}
