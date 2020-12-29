using System;
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
    public float cost;
    protected bool isTypeF;

    protected Player player;

    public Gadget()
    {
        isTypeF = false;
        this.unlocked = false;
        this.player = Player.Instance;

    }

    public bool canUnlock()
    {
        return this.player.getMoney() >= this.cost;
    }

    public abstract GadgetType getGadgetType();

    public void unlock()
    {
        if (!this.unlocked && this.canUnlock())
        {
            player.changeMoney(-this.cost);
            this.unlocked = true;
        }
    }

    public virtual bool CanUse()
    {
        return this.player.level >= this.minLevel;
    }

    public abstract void Use();

    public bool getIsTypeF()
    {
        return isTypeF;
    }

    public abstract string getID();
}
