using UnityEngine;
using System.Collections.Generic;

public class SkillsTree
{
	public Dictionary<string, Skill> skills;

	public SkillsTree()
	{
		this.skills = new Dictionary<string, Skill>();
		Skill jumpMan = new Jumpman();
		Skill jumpMan2 = new Jumpman2();
		jumpMan.setParent(jumpMan2);

		Skill lightStep = new LightStep();
		Skill lightStep2 = new LightStep2();
		lightStep.setParent(lightStep2);


        Skill goFast = new GottaGoFast();
        Skill goFast2 = new GottaGoFast2();
        goFast.setParent(goFast2);

        #region dependencies
        jumpMan.addDependency(lightStep);

        goFast.addDependency(lightStep);

        jumpMan2.addDependency(jumpMan);
        jumpMan2.addDependency(lightStep);

        goFast2.addDependency(lightStep);
        goFast2.addDependency(goFast);

        lightStep2.addDependency(jumpMan2);
        lightStep2.addDependency(jumpMan);
        lightStep2.addDependency(goFast);
        lightStep2.addDependency(goFast2);
        lightStep2.addDependency(lightStep);
        #endregion

        this.skills.Add(Jumpman.ID, jumpMan);
        this.skills.Add(Jumpman2.ID, jumpMan2);
        this.skills.Add(LightStep.ID, lightStep);
        this.skills.Add(LightStep2.ID, lightStep2);
        this.skills.Add(GottaGoFast.ID, goFast);
        this.skills.Add(GottaGoFast2.ID, goFast2);
    }

	public Skill GetSkill(string skillName)
	{
		Skill skill = skills.ContainsKey(skillName) ? skills[skillName] : null;

		if (skill != null)
		{
			return skill;
		}

		else return null;

	}

	public List<Skill> getUnlockedSkills()
	{
		List<Skill> unlockedSkills = new List<Skill>();
		foreach (Skill skill in this.skills.Values)
		{
			if (skill.unlocked)
			{
				unlockedSkills.Add(skill);
			}
		}
		return unlockedSkills;
	}

	public List<Skill> getAllSkills()
	{
		return new List<Skill>(this.skills.Values);
	}

	public void activateAllSkills()
	{
		Debug.Log("Activating skills");
		foreach (Skill skill in this.skills.Values)
		{
			skill.activate();
		}
	}

}