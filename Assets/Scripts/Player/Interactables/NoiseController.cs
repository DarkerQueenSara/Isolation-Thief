using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseController : Hackable
{
    //private Animator animator;

    new private void Awake()
    {
        //base.objectName = "vault";
        //animator = gameObject.GetComponent<Animator>();
    }

    public override void interact()
    {
        
    }

    public void OpenOrClose()
    {

    }

    public override string getInteractingText()
    {
        //bool isOpen = animator.GetBool("isOpenDoor");
        //#region debug
        //if (isLocked && isOpen)
        //{
        //    Debug.LogError("Door is Locked and Open!");
        //    return "Close door";
        //}
        //#endregion

        return "Make noise";
    }
}
