using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetTree
{
    public Dictionary<string,Gadget> gadgets;


    public GadgetTree()
    {
        this.gadgets = new Dictionary<string, Gadget>();
        this.gadgets.Add("lockpick", new SimpleLockpick());
        this.gadgets.Add("lantern", new Lantern());
    }

    public Gadget GetGadget(string gadgetName)
    {
        Gadget gadget = gadgets.ContainsKey(gadgetName) ? gadgets[gadgetName] : null;

        if (gadget != null)
        {
            return gadget;
        }

        else return null;

    }
}
