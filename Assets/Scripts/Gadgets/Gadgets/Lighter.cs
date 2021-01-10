using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : Gadget
{
    public const string gadgetID = "Lighter";
    private int fuel;

    public Lighter() : base()
    {
        gadgetDependencies = new List<Gadget>();
        unlocked = true;
        this.useAnywhere = false;
        this.gadgetInfo = Resources.Load<GadgetInfo>(Gadget.GADGET_INFO_DIR + "Lighter");
        fuel = 100;
    }

    public override bool CanUse()
    {
        return base.CanUse() && fuel > 0;
    }

    

    public override GadgetType getGadgetType()
    {
        return GadgetType.LIGHTER;
    }

    public override void Use()
    {
        fuel--;
        //Apart from decreasing fuel does nothing, class Flammable does the work
    }
}
