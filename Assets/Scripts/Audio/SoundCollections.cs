using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SoundCollections
{
    [SerializeField] public string name = "";
    [Range(0f, 1f)]
    [SerializeField] public float volume = 1f;
    [SerializeField] public bool loop = false;
    [SerializeField] private List<AudioClip> clips = new List<AudioClip>();
    [HideInInspector] private List<Sound> sounds = new List<Sound>();
    [HideInInspector] private List<Sound> lastSounds = new List<Sound>();

    public void Instantiate()
    {
        foreach(AudioClip clip in clips)
        {
            sounds.Add(new Sound(clip.name, clip, volume, true, loop));
        }
    }

    public Sound GetSound()
    {
        if(sounds.Count == 0)
        {
            sounds = new List<Sound>(lastSounds);
            lastSounds.Clear();
        }

        int index = Random.Range(0, sounds.Count);

        Sound s = sounds[index];
        sounds.Remove(s);
        lastSounds.Add(s);

        return s;
    }

}
