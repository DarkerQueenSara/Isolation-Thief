using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hackable : Interactable
{
    public const int MAX_HACKING_TRIES = 1;
    public bool isLocked;
    private int numTries;
    protected string objectName;

    public void Awake()
    {
        objectName = gameObject.name;
        numTries = 0;
    }

    public override void interact()
    {
        //this method is ment to be blank
        if (isLocked)
        {
            HackingDevice hackingDevice = (HackingDevice)player.getGadgetOnHand(GadgetType.HACKING_DEVICE);
            if (hackingDevice != null && hackingDevice.CanUse())
            {
                //TODO hacking device open minigame and if succeed then I have to unlock
                //StartCoroutine(parallelHacking(hackingDevice));
                if(numTries < MAX_HACKING_TRIES)
                {
                    numTries++;
                    hackingDevice.HackObject(onHackEnd);
                }
            }
        }
    }

    private void onHackEnd(bool win)
    {
        if (win)
        {
            isLocked = false;
            //TODO maybe remove
            //this.interact();
            InteractionTextManager.instance.setInteractingText(this.getInteractingText());
            Debug.Log("Device hacked");
        }
        else
        {
            Debug.Log("Hack failed!");
        }
    }

    /*public override void interacting()
    {
        if (isLocked)
        {
            HackingDevice hackingDevice = (HackingDevice)player.getGadgetOnHand(GadgetType.HACKING_DEVICE);
            if(hackingDevice != null && hackingDevice.CanUse())
            {
                if (hackingDevice.HackObject())
                {
                    isLocked = false;
                    //TODO maybe remove
                    this.interact();
                    Debug.Log("Device hacked");
                }
            }
        }
    }

    public override void interactionStopped()
    {
        if (isLocked)
        {
            HackingDevice hackingDevice = (HackingDevice)player.getGadgetOnHand(GadgetType.HACKING_DEVICE);
            if (hackingDevice != null)
            {
                hackingDevice.stopHacking();
            }
        }
    }*/

    public override string getInteractingText()
    {
        if (isLocked)
        {
            if (player.hasGadgetOnHand(GadgetType.HACKING_DEVICE) && numTries < MAX_HACKING_TRIES)
            {
                return "Hack " + objectName;
            }
            else
            {
                return objectName + " is Locked";
            }
        }
        return "";
    }
}
