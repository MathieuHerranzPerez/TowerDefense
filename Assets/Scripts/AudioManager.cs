using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Sound[] soundList;

    public Slider slider;
    public Slider musicSlider;
    [HideInInspector]
    public float multiplier = 1f;
    [HideInInspector]
    public float musicMultiplier = 1f;


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

        // get the volumes
        multiplier = PlayerPrefs.GetFloat("volume", 1f);
        musicMultiplier = PlayerPrefs.GetFloat("volumeMusic", 1f);

        if (slider != null)
        {
            slider.value = multiplier;
            musicSlider.value = musicMultiplier;
        }

        // change the volume
        UpdateVolume();
        UpdateMusicVolume();        
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

    public void Stop(string name)
    {
        Sound s = Array.Find(soundList, sound => sound.name == name);
        if (s != null)
            s.source.Stop();
        else
            Debug.Log("Sound " + name + " doesn't exist");
    }

    public void UpdateVolume()
    {
        if (slider != null)
        {
            multiplier = slider.value;
        }
        PlayerPrefs.SetFloat("volume", multiplier);                 // set in the preferences
        AudioListener.volume = multiplier;
    }

    public void UpdateMusicVolume()
    {
        if (slider != null)
        {
            musicMultiplier = musicSlider.value;
        }
        PlayerPrefs.SetFloat("volumeMusic", musicMultiplier);       // set in the preferences
        foreach (Sound s in soundList)
        {
            if (s.isMusic)
            {
                s.source.volume = s.volume * musicMultiplier;
            }
        }
    }
}
