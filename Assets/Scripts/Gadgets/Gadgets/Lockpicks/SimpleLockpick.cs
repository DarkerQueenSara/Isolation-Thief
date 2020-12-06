﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLockpick : Lockpick
{

    public SimpleLockpick() : base()
    {
        usability = int.MaxValue; //infinite pmuch
        List<Gadget> gadgetDependencies = new List<Gadget>();
        minLevel = 1;
    }

    public override bool CanUse()
    {
        return base.CanUse();
    }

    public override void Use()
    {
        //Empty for now
    }
}
