using UnityEngine;
using System.Collections.Generic;

public class KarmaSkillsTree
{
    public Dictionary<string, KarmaSkill> skills;

    public KarmaSkillsTree()
    {
        this.skills = new Dictionary<string, KarmaSkill>();
    }

    public KarmaSkill GetSkill(string skillName)
    {
        KarmaSkill skill = skills.ContainsKey(skillName) ? skills[skillName] : null;

        if (skill != null)
        {
            return skill;
        }

        else return null;

    }

    public List<KarmaSkill> getUnlockedSkills()
    {
        List<KarmaSkill> unlockedSkills = new List<KarmaSkill>();
        foreach (KarmaSkill skill in this.skills.Values)
        {
            if (skill.unlocked)
            {
                unlockedSkills.Add(skill);
            }
        }
        return unlockedSkills;
    }

    public List<KarmaSkill> getAllSkills()
    {
        return new List<KarmaSkill>(this.skills.Values);
    }

    public void activateAllSkills()
    {
        Debug.Log("Activating skills");
        foreach(KarmaSkill skill in this.skills.Values)
        {
            skill.activate();
        }
    }

}