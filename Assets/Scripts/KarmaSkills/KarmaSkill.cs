using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class KarmaSkill
{

	public bool unlocked;
	public bool realUnlocked;
	public const string KARMA_SKILL_INFO_DIR = "KarmaSkillInfos/";
	public SkillInfo skillInfo;
	//public KarmaSkill parent;
	//public List<KarmaSkill> skillDependencies;

	public KarmaSkill()
	{
		realUnlocked = false;
		//skillDependencies = new List<KarmaSkill>();
		unlocked = false;
		//parent = null;
	}
	/*
    public void setParent(KarmaSkill parent)
    {
        this.parent = parent;
    }*/

	public bool canUnlock()
	{
		return !unlocked && GameManager.Instance.availableKp >= this.getXPCost();
	}

	public void unlock()
	{
		if (canUnlock())
		{
			GameManager.Instance.availableKp -= this.getXPCost();
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
		return GameManager.Instance.karmaSkillsTree.getUnlockedSkills().Count + 1;
		//return this.skillInfo.cost;
	}

	public Sprite getSprite()
	{
		return this.skillInfo.GetSprite();
	}
}