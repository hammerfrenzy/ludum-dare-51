using UnityEngine;
using System;


//call AudioManager by using FindObjectOfType<AudioManager>().Play("Sound_Name");

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    public bool muteSfx = false; //instantly set to false on load because checkbox sucks??
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("Background Music");
    }
    public void Play(string name)
    {
        if (muteSfx) return;

        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found in AudioManager.");
            return;
        }

        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found in AudioManager.");
            return;
        }

        s.source.Play();
    }

    public void ToggleBGM()
    {
        Sound s = Array.Find(sounds, sound => sound.name == "Background Music");

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found in AudioManager.");
            return;
        }

        if(s.source.isPlaying)
            s.source.Stop();
        else
            s.source.Play();
    }

    public void ToggleSFX()
    {
        muteSfx = !muteSfx;
    }

    public void PlayGonGit()
    {
        Sound s = Array.Find(sounds, sound => sound.name == "Gon Git");

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found in AudioManager.");
            return;
        }

        s.source.Play();
    }
}
