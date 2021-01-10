using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class KarmaSkill
{

    public bool unlocked;
    public const string SKILL_INFO_DIR = "SkillInfos/";
    public SkillInfo skillInfo;
    public KarmaSkill parent;
    public List<KarmaSkill> skillDependencies;

    public KarmaSkill()
    {
        skillDependencies = new List<KarmaSkill>();
        unlocked = false;
        parent = null;
    }

    public void setParent(KarmaSkill parent)
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
    public abstract string getID();

    public string getName()
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