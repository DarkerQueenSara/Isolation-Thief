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
    }
}
