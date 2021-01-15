using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultController : Hackable
{
    private Animator animator;
    public bool isASafe;
        
    new private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public override void interact()
    {
        if (isLocked)
        {
            base.interact();
            return;
        }
        this.OpenOrClose();
    }

    public void OpenOrClose()
    {
        bool isOpen = animator.GetBool("isOpenDoor");
        animator.SetBool("isOpenDoor", !isOpen);
        if (isASafe)
        {
            LevelManager.Instance.hackedSafe = true;
        }
    }

    public override string getInteractingText()
    {
        bool isOpen = animator.GetBool("isOpenDoor");

        if (isLocked)
        {
            if (player.hasGadgetOnHand(GadgetType.HACKING_DEVICE) && NumTries < MAX_HACKING_TRIES)
            {
                return "Hack Door";
            }
            else
            {
                return "Door is Locked (Need hacking device)";
            }
        }
        return isOpen ? "Close Door" : "Open Door";
    }
    
}
