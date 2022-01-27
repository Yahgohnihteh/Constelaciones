using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds; //Array of sounds
    public static AudioManager instance;


    // Use this for initialization
    private void Awake()
    {

        //Check that there's only one AudioManager in the scene (Singleton)
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //Make sure that the AudioManager doesn't get destroyed
        DontDestroyOnLoad(gameObject);

        //Creat an AudioSource for each sound
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    private void Start()
    {
        Play("Main_Theme");
    }

    //Function that plays a sound based on the name given
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found !");
            return;
        }

        s.source.enabled = true;
        s.source.Play();
    }


    //Function that plays a sound based on the name given with a random pitch
    public void PlayWithRandomPitch(string name, float min, float max)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found !");
            return;
        }

        s.source.enabled = true;
        s.source.pitch = UnityEngine.Random.Range(min, max);
        s.source.Play();
    }

    //Pause the sound given
    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found !");
            return;
        }
        s.source.Pause();
        s.source.enabled = false;
    }
}