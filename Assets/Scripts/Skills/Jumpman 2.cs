using UnityEngine;
using System.Collections;


public class Jumpman2 : Skill
{

    public const string ID = "Jumpman2";

    public Jumpman2()
    {
        unlocked = false;
        this.skillInfo = Resources.Load<SkillInfo>(Skill.SKILL_INFO_DIR + "Jumpman2");
        GameManager.Instance.skillsTree.GetSkill(Jumpman.ID).setParent(this);
    }

    public override void activate()
    {
        if(parent == null || !parent.unlocked)
        {
            Debug.Log("Activating skill " + getID());
        }
    }

    public override string getID()
    {
        return ID;
    }
}