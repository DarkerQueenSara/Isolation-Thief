using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player.Controls
{
    public class DoorInteractionAnimator : Interactable
    {
        public Animator animator;

        //Public 
        public bool isLocked;

        //Private
        private bool isOpen;

        private void Awake()
        {
            isOpen = false;
        }

        public override void interact()
        {
            if (isLocked)
            {
                if (lockpickable)
                {
                    if (player.CanLockpick())
                    {
                        this.Lockpick();
                    }
                }


            } else
            {
                this.OpenOrClose();
            }
        }

        private void Lockpick()
        {
            this.isLocked = false;
            this.OpenOrClose();
        }

        private void OpenOrClose()
        {
            animator.SetTrigger("OpenCloseDoor");
            isOpen = !isOpen;
        }

        public override string getInteractingText()
        {
            if(isLocked && isOpen)
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

            } else
            {

                return isOpen? "Close door" : "Open door";
            }
        }
    }
}