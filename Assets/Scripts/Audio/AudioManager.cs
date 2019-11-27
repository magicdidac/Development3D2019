using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    [HideInInspector] private GameManager gm;

    [SerializeField] private GameObject audioSpotPrefab = null;
    [Space]
    [SerializeField] private List<Sound> sounds = new List<Sound>();
    [Space]
    [SerializeField] private List<SoundCollections> soundCollections = new List<SoundCollections>();

    private void Start()
    {
        gm = GameManager.instance;
        gm.audioManager = this;

        foreach (Sound s in sounds)
        {
            if (!s.is3D)
            {
                s.source = gameObject.AddComponent<AudioSource>();

                ApplySettings(s.source, s);

                s.source.spatialBlend = 0;
            }
        }

        foreach (SoundCollections sc in soundCollections)
        {
            sc.Instantiate();
        }

        Play("Song-MainTheme");

    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds.ToArray(), sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (s.is3D)
        {
            Debug.LogWarning("Sound: " + name + " is a 3D sound use PlayAtPosition(...) method instead of Play(...)");
            return;
        }

        s.source.Play();

    }

    private void Play(Sound s)
    {
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (s.is3D)
        {
            Debug.LogWarning("Sound: " + name + " is a 3D sound use PlayAtPosition(...) method instead of Play(...)");
            return;
        }

        s.source = Instantiate(audioSpotPrefab, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        
        ApplySettings(s.source, s);

        s.source.spatialBlend = 0;

        s.source.Play();

        if (!s.loop)
            Destroy(s.source.gameObject, s.clip.length + .1f);
    }

    public void PlayAtPosition(string name, Vector3 position)
    {
        PlayAtPosition(name, position, null);
    }

    public void PlayAtPosition(string name, Transform parent)
    {
        PlayAtPosition(name, parent.position, parent);
    }

    public void PlayAtPosition(string name, Vector3 position, Transform parent)
    {
        PlayAtPosition(Array.Find(sounds.ToArray(), sound => sound.name == name), position, parent);
    }

    private void PlayAtPosition(Sound s, Vector3 position, Transform parent)
    {
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (!s.is3D)
        {
            Debug.LogWarning("Sound: " + name + " is a non 3D sound use Play(...) method instead of PlayAtPosition(...)");
            return;
        }

        s.source = Instantiate(audioSpotPrefab, position, Quaternion.identity).GetComponent<AudioSource>();

        s.source.transform.parent = parent;

        ApplySettings(s.source, s);

        s.source.spatialBlend = 1;

        s.source.Play();

        if (!s.loop)
            Destroy(s.source.gameObject, s.clip.length + .1f);
    }

    public void PlaySoundOfCollectionAtPosition(string name, Transform parent)
    {
        PlaySoundOfCollectionAtPosition(name, parent.position, parent);
    }

    public void PlaySoundOfCollectionAtPosition(string name, Vector3 position, Transform parent)
    {
        PlayAtPosition(Array.Find(soundCollections.ToArray(), sound => sound.name == name).GetSound(), position, parent);
    }

    public void PlaySoundOfCollectionAtPosition(string name, Vector3 position)
    {
        PlaySoundOfCollectionAtPosition(name, position, null);
    }

    public void PlaySoundOfCollection(string name)
    {
        Play(Array.Find(soundCollections.ToArray(), sound => sound.name == name).GetSound());
    }

    public void StopSound(string name)
    {
        StopSound(Array.Find(sounds.ToArray(), sound => sound.name == name));
    }

    private void StopSound(Sound s)
    {
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found! [trying to stop]");
            return;
        }

        s.source.Stop();

    }

    private void ApplySettings(AudioSource source, Sound s)
    {
        source.playOnAwake = false;
        source.clip = s.clip;

        source.volume = s.volume;
        source.loop = s.loop;
    }

    public void SetListenerEnableTo(bool b)
    {
        gm.player.GetComponent<AudioListener>().enabled = b;
    }

}