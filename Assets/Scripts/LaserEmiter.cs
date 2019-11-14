using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmiter : MonoBehaviour
{
    [SerializeField] private LineRenderer lineR = null;

    private void Update()
    {
        Laser.Make(lineR);
    }
}
