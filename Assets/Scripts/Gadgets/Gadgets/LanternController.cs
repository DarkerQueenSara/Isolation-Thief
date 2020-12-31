using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour
{
    Light light;
    private void Awake()
    {
        light = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Lantern"))
        {
            light.enabled = !light.enabled;
            LevelManager.Instance.usedFlashlight = true;
        }
    }
}
