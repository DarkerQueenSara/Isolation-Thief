using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Player.Controls
{
    public class LightSwitch : Interactable
    {
        //public Animator animator;
        public List<Light> roomLights;
        public float defaultIntensity = 1.0f;
        private List<float> lightIntensities;


        //Public 
        public bool isOn()
        {
            return roomLights[0].intensity > 0.001;
        }

        private void Awake()
        {
            lightIntensities = new List<float>(roomLights.Count);
            for (int i = 0; i < roomLights.Count; i++)
            {
                Light roomLight = roomLights[i];
                if (isOn())
                {
                    lightIntensities.Add(roomLight.intensity);
                }
                else
                {
                    //Default value de lightIntensity vai ser 1;
                    lightIntensities.Add(defaultIntensity);
                }
            }
        }


        public override void interact()
        {
            gameObject.transform.Rotate(.0f, .0f, 180.0f, Space.Self);
            //imaginando todas as luzes ligadas ou desligadas
            if(isOn())
            {
                for (int i = 0; i < roomLights.Count; i++)
                {
                    roomLights[i].intensity = 0.0f;
                }
            }
            else
            {
                for (int i = 0; i < roomLights.Count; i++)
                {
                    roomLights[i].intensity = lightIntensities[i];
                }
            }
        }

        //Look
        public override string getInteractingText()
        {
            return "Click E to turn " + (isOn() ? "off" : "on") + " light";
        }


    }

}