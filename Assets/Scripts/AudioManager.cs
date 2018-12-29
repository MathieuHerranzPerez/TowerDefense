using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] soundList;


    void Awake()
    {
        foreach (Sound s in soundList)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;

            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Use this for initialization
    void Start()
    {
        Play("forestAmbience");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(soundList, sound => sound.name == name);
        if (s != null)
            s.source.Play();
        else
            Debug.Log("Sound " + name + " doesn't exist");
    }
}
