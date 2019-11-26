using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [HideInInspector] private AudioManager audioManager;


    private void Start()
    {
        audioManager = GameManager.instance.audioManager;
    }

    public void Play(string name)
    {
        if (!audioManager)
            audioManager = GameManager.instance.audioManager;

        audioManager.Play(name);
    }

    public void PlayAtPosition(string name)
    {
        if (!audioManager)
            audioManager = GameManager.instance.audioManager;

        audioManager.PlayAtPosition(name, transform);
    }

    public void PlayCollection(string name)
    {
        if (!audioManager)
            audioManager = GameManager.instance.audioManager;

        audioManager.PlaySoundOfCollection(name);

    }

}
