using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                //instance = new GameObject("GameManager").AddComponent<GameManager>();
                instance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    public void OnApplicationQuit()
    {
        instance = null;
    }


    public Sound[] sounds;
    
    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend;
        }
    }

    private void Start()
    {
        //Play("StageMusic");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
