using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{

    [SerializeField] public string name = "";
    [SerializeField] public AudioClip clip = null;
    [Range(0f, 1f)]
    [SerializeField] public float volume = 1f;
    [SerializeField] public bool is3D = false;
    [SerializeField] public bool loop = false;

    [HideInInspector] public AudioSource source = null;

    public Sound(string name, AudioClip clip, float volume, bool is3D, bool loop)
    {
        this.name = name;
        this.clip = clip;
        this.volume = volume;
        this.is3D = is3D;
        this.loop = loop;
    }
}
