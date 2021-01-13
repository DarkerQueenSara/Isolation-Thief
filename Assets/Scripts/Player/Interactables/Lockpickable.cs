using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockpickable : Interactable
{
    public bool isLocked;
    protected string objectName;

    public void Awake()
    {
        objectName = gameObject.name;
    }

    public override void interacting()
    {
        if (isLocked)
        {
            Lockpick lockpick = (Lockpick)player.getGadgetOnHand(GadgetType.LOCKPICK);
            if(lockpick != null && lockpick.CanUse())
            {
                if (lockpick.LockpickObject())
                {
                    isLocked = false;
                    //TODO maybe remove
                    
                    if (this.gameObject.CompareTag("Door"))
                    {
                        Debug.Log("Picked a door");
                        LevelManager.Instance.doorsLockpicked++;
                    } else
                    {
                        Debug.Log("Picked a window");
                        LevelManager.Instance.windowsLockpicked++;
                    }
                }
            }
        }
    }

    public override void interactionStopped()
    {
        if (isLocked)
        {
            Lockpick lockpick = (Lockpick)player.getGadgetOnHand(GadgetType.LOCKPICK);
            if (lockpick != null)
            {
                lockpick.stopPicking();
            }
        }
    }

    public override string getInteractingText()
    {
        if (isLocked)
        {
            if (player.hasGadgetOnHand(GadgetType.LOCKPICK))
            {
                return "Pick the lock";
                //return "Pick " + objectName;
            }
            else
            {
                return "It's Locked";
                //return objectName + " is Locked";
            }
        }
        return "";
    }
}
