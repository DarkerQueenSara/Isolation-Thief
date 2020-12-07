using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player.Controls
{
	public class DoorInteractionAnimator : Interactable
	{
		public Animator animator;

		public override void interact()
		{
			bool isOpen = animator.GetBool("isOpenDoor");
			animator.SetBool("isOpenDoor", !isOpen);
		}

		public override string getInteractingText()
		{
			bool isOpen = animator.GetBool("isOpenDoor");
			return isOpen ? "Close door" : "Open door";
		}
	}
}