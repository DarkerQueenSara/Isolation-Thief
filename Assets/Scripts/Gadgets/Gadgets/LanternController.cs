using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour
{
    new Light light;
    private AudioManager playerAudioManager;
    private void Awake()
    {
        light = this.gameObject.GetComponent<Light>();
        playerAudioManager = this.gameObject?.GetComponentInParent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Lantern"))
        {
            playerAudioManager.Play("Click");
            light.enabled = !light.enabled;
            LevelManager.Instance.usedFlashlight = true;
            
        }
    }
}
