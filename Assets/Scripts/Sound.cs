using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    // Declaring attributes of Sound class
    public string name;
    public AudioClip clip;

    [Range(0, 1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch = 1;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

    public bool enabled;
}
