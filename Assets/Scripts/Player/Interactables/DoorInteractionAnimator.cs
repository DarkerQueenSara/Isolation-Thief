using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player.Controls
{
    public class DoorInteractionAnimator : Lockpickable
    {
        private Animator animator;

        new private void Awake()
        {
            base.objectName = "door";
            animator = gameObject.GetComponent<Animator>();
        }

        public override void interact()
        {
            if (isLocked)
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
            if (isLocked && isOpen)
            {
                Debug.LogError("Door is Locked and Open!");
                return "Close door";
            }
            #endregion

            if (base.isLocked)
            {
                return base.getInteractingText();
            }

            return isOpen ? "Close door" : "Open door";
        }


    }

}