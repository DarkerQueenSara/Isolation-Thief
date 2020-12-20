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
    public int cost;
    protected bool isTypeF;

    protected Player player;

    public Gadget()
    {
        isTypeF = false;
        this.unlocked = false;
        this.player = Player.Instance;

    }

    public abstract GadgetType getGadgetType();

    public virtual bool CanUse()
    {
        return this.player.level >= this.minLevel;
    }

    public abstract void Use();

    public bool getIsTypeF()
    {
        return isTypeF;
    }
}
