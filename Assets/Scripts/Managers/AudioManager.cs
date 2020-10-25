using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //TODO: Add randomized pitch, global value change for Effects vs Music

    public Sound[] sounds;

    public static AudioManager instance;

    // TODO: Load these values from PlayerPrefs in Start().
    [HideInInspector] public float masterMusicVolume = 1.0f;
    [HideInInspector] public float masterEffectVolume = 1.0f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public AudioSource PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound of name " + name + " not found!");
            return null;
        }
        s.source.volume = s.volume * (s.isEffect ? masterEffectVolume : masterMusicVolume);
        s.source.Play();

        return s.source;
    }

    public void SetEffectsVolume(float value)
    {
        masterEffectVolume = value;
    }

    public void SetMusicVolume(float value)
    {
        masterMusicVolume = value;
    }
}
