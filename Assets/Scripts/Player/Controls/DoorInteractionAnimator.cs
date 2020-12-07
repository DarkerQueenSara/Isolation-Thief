using UnityEngine;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.Player.Controls
{
    public class DoorInteractionAnimator : Interactable
    {
        public Animator animator;

        //Public 
        public bool isLocked;

        //Private
        private bool isOpen;


        private bool lockpicking;

        private void Awake()
        {
            isOpen = false;
            lockpicking = false;
        }


        public override void interact()
        {
            if (isLocked)
            {
                if (lockpickable)
                {
                    if (player.CanLockpick())
                    {
                        this.StartLockpick();
                    }
                }


            } else
            {
                this.OpenOrClose();
            }
        }

        //Look
        public override string getInteractingText()
        {
            if (isLocked && isOpen)
            {
                Debug.LogError("Door is Locked and Open!");
                return "Close door";
            }

            if (isLocked && !isOpen)
            {
                if (lockpickable)
                {
                    if (player.CanLockpick())
                    {
                        return "Lockpick";
                    }
                }

                return "Locked";

            }
            else
            {

                return isOpen ? "Close door" : "Open door";
            }
        }

        private void StartLockpick()
        {
            lockpicking = true;
        }

        private void Update()
        {
            if (lockpicking)
            {
                if (Input.GetButtonUp("Interact"))
                {
                    this.lockpicking = false;
                }

                bool lockpicked = player.Lockpick();
                if (lockpicked)
                {
                    this.OpenOrClose();
                    this.isLocked = false;
                    this.lockpicking = false;
                }
            }
        }



        private void OpenOrClose()
        {
            animator.SetTrigger("OpenCloseDoor");
            isOpen = !isOpen;
        }


    }
}