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
    public const string GADGET_INFO_DIR = "GadgetInfos/";
    public GadgetInfo gadgetInfo;
    public int usability;
    public bool unlocked;
    public List<Gadget> gadgetDependencies;
    public int minLevel;
    protected bool useAnywhere;

    protected Player player;

    public Gadget()
    {
        useAnywhere = false;
        this.unlocked = false;
        this.player = Player.Instance;
    }

    public bool canUnlock()
    {
        return this.player.getMoney() >= this.getCost();
    }

    public abstract GadgetType getGadgetType();

    public void unlock()
    {
        if (!this.unlocked && this.canUnlock())
        {
            player.changeMoney(-this.getCost());
            this.unlocked = true;
        }
    }

    public virtual bool CanUse()
    {
        return this.player.level >= this.minLevel;
    }

    public abstract void Use();

    public bool CanUseAnywhere()
    {
        return useAnywhere;
    }

    public string getID()
    {
        return this.gadgetInfo.name;
    }

    public float getCost()
    {
        return this.gadgetInfo.cost;
    }

    public Sprite getSprite()
    {
        return this.gadgetInfo.GetSprite();
    }
}
