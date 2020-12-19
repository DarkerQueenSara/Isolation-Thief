using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player.Controls
{
	public class WindowInteractionAnimator : Interactable
	{
		public Animator animator;

		public override void interact()
		{
			bool isOpen = animator.GetBool("isOpenWindow");
			animator.SetBool("isOpenWindow", !isOpen);
		}

		public override string getInteractingText()
		{
			bool isOpen = animator.GetBool("isOpenWindow");
			return isOpen ? "Close window" : "Open window";
		}
	}

	/* caso queiramos tornar janelas lockpickable ja criei template
	 * 
	 public class WindowInteractionAnimator : Lockpickable
	{
		public Animator animator;
        new private void Awake()
        {
			base.objectName = "window";
		}
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
	 * 
	 */
}