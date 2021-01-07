using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingDevice : Gadget
{
    public LoadingBar loadingBar;

    public HackingDevice() : base()
    {
        this.loadingBar = LoadingBar.instance;
        //Debug.Log(loadingBar);
    }

    public override bool CanUse()
    {

        return base.CanUse();
    }

    public override void Use()
    {
        //empty for now
    }

    public virtual void HackObject(Action<Boolean> gameEndCallback)
    {
        //this method is ment to be overwriten
        //return false;
    }
    public virtual void stopHacking() { 
        //this method is ment to be overwriten
    }


    public virtual float GetHackingTime()
    {
        return float.MaxValue;
    }

    public override GadgetType getGadgetType()
    {
        return GadgetType.HACKING_DEVICE;
    }
}
