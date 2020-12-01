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

            animatorLeft.SetBool("isOpen", !animatorLeft.GetBool("isOpen"));
        }

        public override string getInteractingText()
        {
            return animatorLeft.GetBool("isOpen")? "Close window" : "Open window";
        }
    }
}