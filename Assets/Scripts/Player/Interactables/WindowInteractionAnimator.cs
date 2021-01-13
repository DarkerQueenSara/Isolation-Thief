using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player.Controls
{
	 public class WindowInteractionAnimator : Lockpickable
	{
		public Animator animator;
       
        public override void interact()
		{
            if (base.isLocked)
            {
				return;
            }
			bool isOpen = animator.GetBool("isOpenWindow");
			animator.SetBool("isOpenWindow", !isOpen);
		}

		public override string getInteractingText()
		{
            if (base.isLocked)
            {
				return base.getInteractingText();
            }
			bool isOpen = animator.GetBool("isOpenWindow");
			return isOpen ? "Close window" : "Open window";
		}
	}

}