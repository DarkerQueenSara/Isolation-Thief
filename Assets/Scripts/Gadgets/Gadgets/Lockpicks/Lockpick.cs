using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockpick : Gadget
{
    public LoadingBar loadingBar;

    public Lockpick() : base()
    {
        this.loadingBar = LoadingBar.instance;
    }

    public override bool CanUse()
    {

        return base.CanUse();
    }

    public override void Use()
    {
        
    }

    public virtual bool LockpickObject()
    {
        return false;
    }

    public virtual float GetLockPickingTime()
    {
        return float.MaxValue;
    }
}
