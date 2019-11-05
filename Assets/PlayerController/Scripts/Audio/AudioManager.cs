using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{

    private GameObject audioSpot;
    private Transform player;
    private List<AudioClip> audios;

    public AudioManager(GameObject audioSpot, List<AudioClip> audios)
    {
        this.audioSpot = audioSpot;
        this.audios = audios;
        this.player = GameManager.instance.player.transform;
    }

    public void PlaySoundAtPosition(string audio, Vector3 position)
    {
        AudioSpot spot = GameObject.Instantiate(audioSpot, position, Quaternion.identity).GetComponent<AudioSpot>();

        spot.Play(GetAudioClip(audio));

    }

    public void PlaySound(string audio)
    {
        PlaySoundAtPosition(audio, player.position);
    }

    private AudioClip GetAudioClip(string name)
    {
        foreach(AudioClip a in audios)
        {
            if (a.name.Equals(name))
                return a;
        }

        return null;
    }

}
