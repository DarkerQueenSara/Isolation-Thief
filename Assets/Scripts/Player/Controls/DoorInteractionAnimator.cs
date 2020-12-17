using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player.Controls
{
    public class DoorInteractionAnimator : Interactable
    {
        public Animator animator;

        //Public 
        public bool isLocked;

        private bool lockpicking;

        private void Awake()
        {
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


            }
            else
            {
                this.OpenOrClose();
            }
        }

        //Look
        public override string getInteractingText()
        {

            bool isOpen = animator.GetBool("isOpenDoor");

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
                    this.isLocked = false;
                    this.lockpicking = false;
                    this.OpenOrClose();
                }
            }
        }


        public void OpenOrClose()
        {
            bool isOpen = animator.GetBool("isOpenDoor");
            animator.SetBool("isOpenDoor", !isOpen);
            if (animator.GetBool("isOpenDoor"))
            {
                gameObject.layer = LayerMask.NameToLayer("OpenDoor");
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("ClosedDoor");
            }
        }


    }

}