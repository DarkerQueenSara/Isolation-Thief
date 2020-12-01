using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player.Controls
{
    public class WindowInteractionAnimator : Interactable
    {
        public Animator animatorLeft;
        public Animator animatorRight;

        private bool isOpen = false;
        public override void interact()
        {
            animatorLeft.SetTrigger("OpenCloseWindowL");
            animatorRight.SetTrigger("OpenCloseWindowR");
            
            isOpen = !isOpen;
        }

        public override string getInteractingText()
        {
            return isOpen? "Close window" : "Open window";
        }
    }
}