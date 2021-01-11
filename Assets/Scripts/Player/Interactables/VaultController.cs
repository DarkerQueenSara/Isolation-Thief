using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultController : Hackable
{
    private Animator animator;

    new private void Awake()
    {
        base.objectName = "vault";
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
    }

    public override string getInteractingText()
    {
        bool isOpen = animator.GetBool("isOpenDoor");
        //#region debug
        //if (isLocked && isOpen)
        //{
        //    Debug.LogError("Door is Locked and Open!");
        //    return "Close door";
        //}
        //#endregion

        if (base.isLocked)
        {
            return base.getInteractingText();
        }

        return isOpen ? "Close door" : "Open door";
        //return "Steal Vault";
    }
}
