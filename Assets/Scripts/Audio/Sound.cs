using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.0f, 1.0f)]
    public float spatialBlend;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    public bool fadeIn;

    [HideInInspector]
    public AudioSource source;

}
