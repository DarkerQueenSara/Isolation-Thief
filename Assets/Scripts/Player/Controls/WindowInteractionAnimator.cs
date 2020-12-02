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
}