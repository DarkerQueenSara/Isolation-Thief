using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GadgetType
{
    LOCKPICK,
    LANTERN
}

public abstract class Gadget
{
    
    public int usability;
    public bool unlocked;
    public List<Gadget> gadgetDependencies;
    public int minLevel;

    protected Player player;

    public Gadget()
    {
        this.unlocked = false;
        this.player = Player.Instance;

    }

    public abstract GadgetType getGadgetType();

    public virtual bool CanUse()
    {
        return this.player.level >= this.minLevel;
    }

    public abstract void Use();
}
