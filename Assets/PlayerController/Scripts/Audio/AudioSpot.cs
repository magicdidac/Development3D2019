using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpot : MonoBehaviour
{

    [HideInInspector] private AudioSource source;

    public void Play(AudioClip audio)
    {
        source = GetComponent<AudioSource>();

        source.clip = audio;
        source.Play();
        Destroy(gameObject, audio.length+.1f);
    }


}
