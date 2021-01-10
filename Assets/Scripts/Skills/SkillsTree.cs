using UnityEngine;
using System.Collections.Generic;

public class SkillsTree
{
    public Dictionary<string, Skill> skills;

    public SkillsTree()
    {
        this.skills = new Dictionary<string, Skill>();
        //this.gadgets.Add(Lantern.gadgetID, new Lantern());
        this.skills.Add(Jumpman.ID, new Jumpman());
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
        foreach(Skill skill in this.skills.Values)
        {
            skill.activate();
        }
    }

}