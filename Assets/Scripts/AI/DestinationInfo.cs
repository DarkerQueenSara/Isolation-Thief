using Assets.Scripts.Player.Controls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationInfo : MonoBehaviour
{
    public Transform lightSwitchTransform;
    [HideInInspector]public LightSwitch lightSwitch;
    public Vector3 destination;

    // Start is called before the first frame update
    void Awake()
    {
        destination = gameObject.transform.position;
        lightSwitch = lightSwitchTransform.GetComponent<LightSwitch>();
    }
}
