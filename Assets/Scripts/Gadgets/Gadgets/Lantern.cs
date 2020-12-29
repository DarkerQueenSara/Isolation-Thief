using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : Gadget
{
    public const string gadgetID = "Lantern";
    public Light lantern;

    public Lantern()
    {
        lantern = GameObject.FindGameObjectWithTag("PlayerLantern").GetComponent<Light>();
        this.isTypeF = true;
        this.cost = 0;
        this.unlocked = true;
    }

    public override void Use()
    {
        
        lantern.enabled = !lantern.enabled;
    }

    public override GadgetType getGadgetType()
    {
        return GadgetType.LANTERN;
    }

    public override string getID()
    {
        return gadgetID;
    }
}
