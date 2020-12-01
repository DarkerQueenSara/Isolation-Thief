using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player.Controls
{
    public class DoorInteractionAnimator : Interactable
    {
        public Animator animator;

        public override void interact()
        {
            animator.SetTrigger("OpenCloseDoor");
        }

        public override string getInteractingText()
        {
            return "Open door";
        }
    }
}