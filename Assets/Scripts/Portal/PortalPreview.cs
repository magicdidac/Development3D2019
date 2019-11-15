using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPreview : MonoBehaviour
{

    [HideInInspector] public float size { get; private set; }
    [HideInInspector] private Vector3 initialScale;


    private void Start()
    {
        initialScale = transform.localScale;

        size = 1;
    }

    private void Update()
    {

        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        size += mouseWheel;

        if (size < .5f)
            size = .5f;

        if (size > 2)
            size = 2;


        transform.localScale = initialScale * size;

    }

}
