using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player.Controls
{
    public class DoorInteractionAnimator : Interactable
    {
        public Animator animator;

        private bool isOpen = false;
        public override void interact()
        {
            animator.SetTrigger("OpenCloseDoor");
            isOpen = !isOpen;
        }

        public override string getInteractingText()
        {
            return isOpen? "Close door" : "Open door";
        }
    }
}