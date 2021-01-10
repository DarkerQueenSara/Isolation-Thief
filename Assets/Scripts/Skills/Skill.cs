using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Skill
{

    public bool unlocked;
    public const string SKILL_INFO_DIR = "SkillInfos/";
    public SkillInfo skillInfo;
    public Skill parent;
    public List<Skill> skillDependencies;

    public Skill()
    {
        skillDependencies = new List<Skill>();
        unlocked = false;
        parent = null;
    }

    protected void setParent(Skill parent)
    {
        this.parent = parent;
    }

    public bool canUnlock()
    {
        return GameManager.Instance.availableXp > this.getXPCost();
    }
    
    public void unlock()
    {
        if (canUnlock())
        {
            GameManager.Instance.availableXp -= this.getXPCost();
            this.unlocked = true;
        }
    }

    public abstract void activate();

    public string getID()
    {
        return this.skillInfo.name;
    }

    public int getXPCost()
    {
        return this.skillInfo.xpCost;
    }

    public Sprite getSprite()
    {
        return this.skillInfo.GetSprite();
    }
}