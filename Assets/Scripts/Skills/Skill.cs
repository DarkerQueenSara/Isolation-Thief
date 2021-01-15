using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Skill
{

	public bool unlocked;
	public const string SKILL_INFO_DIR = "SkillInfos/";
	public SkillInfo skillInfo;
	public Skill parent;

	//Dependencies are defined in the class SkillsTree
	public List<Skill> skillDependencies;

	public Skill()
	{
		skillDependencies = new List<Skill>();
		unlocked = false;
		parent = null;
	}

	public void addDependency(Skill skill)
	{
		skillDependencies.Add(skill);
	}

	public void setParent(Skill parent)
	{
		this.parent = parent;
	}

	public bool canUnlock()
	{
		return !unlocked && skillDependencies.TrueForAll(skill => skill.unlocked) && GameManager.Instance.availableXp >= this.getXPCost();
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
		return this.skillInfo.cost;
	}

	public Sprite getSprite()
	{
		return this.skillInfo.GetSprite();
	}
}