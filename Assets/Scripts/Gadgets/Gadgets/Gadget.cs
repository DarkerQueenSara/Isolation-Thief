using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    public virtual bool CanUse()
    {
        return this.player.level >= this.minLevel;
    }

    public abstract void Use();
}
