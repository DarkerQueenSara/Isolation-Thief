using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockpick : Gadget
{
    public Lockpick() : base()
    {
        
    }

    public override bool CanUse()
    {

        return base.CanUse();
    }

    public override void Use()
    {
        
    }

    public virtual float GetLockPickingTime()
    {
        return float.MaxValue;
    }
}
