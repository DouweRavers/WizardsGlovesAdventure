using UnityEngine.Audio;
using System;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.src = gameObject.AddComponent<AudioSource>();
            s.src.clip = s.clip;

            s.src.volume = s.volume;
            s.src.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        s.src.Play();
    }
}
