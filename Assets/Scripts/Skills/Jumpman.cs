using UnityEngine;
using System.Collections;


public class Jumpman : Skill
{

    public const string ID = "Jumpman";

    public Jumpman()
    {
        unlocked = true;
        this.skillInfo = Resources.Load<SkillInfo>(Skill.SKILL_INFO_DIR + "Jumpman");
    }

    public override void activate()
    {
        if (parent == null || !parent.unlocked)
        {
            Debug.Log("Activating skill " + getID());
        }
    }

    public override string getID()
    {
        return ID;
    }
}