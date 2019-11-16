using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{

    private void Start()
    {
        GameManager.instance.audioManager.PlayAtPosition("Song-Radio", transform);
    }

}
