using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{

    [SerializeField] private string song = "Song-Radio";
    [HideInInspector] private bool played = false;

    private void Update()
    {
        if (!played)
        {
            GameManager.instance.audioManager.PlayAtPosition(song, transform);
            played = true;
        }
    }

}
