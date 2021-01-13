using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hackable : Interactable
{
    public const int MAX_HACKING_TRIES = 1;
    public bool isLocked;
    public int NumTries { get; private set; }
    protected string objectName;

    public void Awake()
    {
        objectName = gameObject.name;
        NumTries = 0;
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
                if(NumTries < MAX_HACKING_TRIES)
                {
                    hackingDevice.HackObject(onHackEnd, MAX_HACKING_TRIES - NumTries);
                }
            }
        }
    }

    private void onHackEnd(int finalResult)
    {
        switch (finalResult)
        {
            case HackingMinigameController.WON_GAME:
                NumTries++;
                isLocked = false;
                InteractionTextManager.instance.setInteractingText(this.getInteractingText());
                LevelManager.Instance.successfullHacks++;
                Debug.Log("Device hacked");
                break;
            case HackingMinigameController.LOST_GAME:
                NumTries++;
                InteractionTextManager.instance.setInteractingText(this.getInteractingText());
                Debug.Log("Hack failed!");
                break;
            case HackingMinigameController.LEFT_GAME:
                Debug.Log("Left Hacking menu!");
                break;
            default:
                break;
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
            if (player.hasGadgetOnHand(GadgetType.HACKING_DEVICE) && NumTries < MAX_HACKING_TRIES)
            {
                return "Hack Device";
            }
        }
        return "";
    }
}
