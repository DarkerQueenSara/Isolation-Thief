using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static SoundManagerScript instance;
    public static string POLICE_SIRENS = "policeSirens";
    public static AudioClip policeSirensSound;
    static AudioSource audioSrc;

    float finalTime;
    float timeToIncrease;
    bool increasingVolume;
    float volume;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one instance of SoundManagerScript found!");
        }
        instance = this;

        policeSirensSound = Resources.Load<AudioClip>(POLICE_SIRENS);

        audioSrc = GetComponent<AudioSource>();
        volume = 1.0f;
        increasingVolume = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (increasingVolume)
        {
            float progress = Mathf.Clamp01((timeToIncrease - (finalTime - Time.time)) / timeToIncrease);
            audioSrc.volume = (progress * progress * progress) / 2 + 0.01f;
            if(progress > 0.99999)
            {
                increasingVolume = false;
                audioSrc.volume = 0f;
            }
        }

    }

    public void stopSound()
    {
        audioSrc.Stop();
    }

    public void PlaySoundGradually(string clip, float timeToIncrease)
    {
        if (POLICE_SIRENS.Equals(clip))
        {
            audioSrc.volume = .0f;
            audioSrc.PlayOneShot(policeSirensSound);
            finalTime = Time.time + timeToIncrease;
            this.timeToIncrease = timeToIncrease;
            increasingVolume = true;
        }
    }

    public void PlaySound(string clip)
    {
        if (POLICE_SIRENS.Equals(clip))
        {
            audioSrc.PlayOneShot(policeSirensSound);
        }
    }
}
