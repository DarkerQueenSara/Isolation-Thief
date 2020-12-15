using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player.Controls
{
    public class DoubleDoorInteractionAnimator : Lockpickable
    {
        public Animator animator;

        new private void Awake()
        {
            animator.SetBool("isLocked", base.isLocked);
            base.objectName = "door";
        }


        public override void interact()
        {
            if (base.isLocked)
            {
                return;
            }
            this.OpenOrClose();
        }

        public void OpenOrClose()
        {
            bool isOpen = animator.GetBool("isOpenDoor");
            animator.SetBool("isOpenDoor", !isOpen);
        }

        public override string getInteractingText()
        {
            bool isOpen = animator.GetBool("isOpenDoor");
            #region debug
            if (isLocked && animator.GetBool("isLocked") && isOpen)
            {
                Debug.LogError("Door is Locked and Open!");
                return "Close door";
            }
            #endregion

            if (base.isLocked && animator.GetBool("isLocked"))
            {
                return base.getInteractingText();
            }

            return isOpen ? "Close door" : "Open door";
        }

        public override void interacting()
        {
            if (base.isLocked)
            {
                base.isLocked = animator.GetBool("isLocked");
                base.interacting();
                if (!base.isLocked)
                {
                    animator.SetBool("isLocked", false);
                }
            }
        }


    }

}