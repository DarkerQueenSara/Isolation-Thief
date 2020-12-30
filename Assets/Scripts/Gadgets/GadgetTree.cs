using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetTree
{
    public Dictionary<string,Gadget> gadgets;


    public GadgetTree()
    {
        this.gadgets = new Dictionary<string, Gadget>();
        this.gadgets.Add(SimpleLockpick.gadgetID, new SimpleLockpick());
        //this.gadgets.Add(Lantern.gadgetID, new Lantern());
        this.gadgets.Add(FastLockpick.gadgetID, new FastLockpick());
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

    public List<Gadget> getUnlockedGadgets()
    {
        List<Gadget> unlockedGadgets = new List<Gadget>();
        foreach(Gadget gadget in this.gadgets.Values)
        {
            if (gadget.unlocked)
            {
                unlockedGadgets.Add(gadget);
            }
        }
        return unlockedGadgets;
    }

    public List<Gadget> getAllGadgets()
    {
        return new List<Gadget>(this.gadgets.Values);
    }


}
