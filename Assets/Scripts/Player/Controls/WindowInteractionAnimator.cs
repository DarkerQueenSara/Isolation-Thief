using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player.Controls
{
    public class WindowInteractionAnimator : Interactable
    {
        public Animator animatorLeft;
        public Animator animatorRight;

        public override void interact()
        {
            animatorLeft.SetTrigger("OpenCloseWindowL");
            animatorRight.SetTrigger("OpenCloseWindowR");
        }

        public override string getInteractingText()
        {
            return "Open window";
        }
    }
}