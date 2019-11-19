using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPreview : MonoBehaviour
{

    [HideInInspector] public float size;
    [HideInInspector] private Vector3 initialScale;


    private void Start()
    {
        initialScale = transform.localScale;

        size = 1;
    }

    private void Update()
    {
        transform.localScale = initialScale * size;
    }

}
