using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player.Controls
{
    public class LightSwitch : Interactable
    {
        //public Animator animator;
        public Light roomlight;

        private float lightIntensity;


        //Public 
        private bool isOn()
        {
            return roomlight.intensity > 0.001;
        }

        private void Awake()
        {
            if (isOn())
            {
                lightIntensity = roomlight.intensity;
            }
            else
            {
                //Default value de lightIntensity vai ser 1;
                lightIntensity = 1;
            }
        }


        public override void interact()
        {
            gameObject.transform.Rotate(.0f, .0f, 180.0f, Space.Self);
            if(isOn())
            {
                roomlight.intensity = 0.0f;
            }
            else
            {
                roomlight.intensity = lightIntensity;
            }
        }

        //Look
        public override string getInteractingText()
        {
            return "Click E to turn " + (isOn() ? "off" : "on") + " light";
        }


    }

}