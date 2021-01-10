using UnityEngine;
using System.Collections;


public class Jumpman2 : Skill
{

    public const string ID = "Jumpman2";
    public const float JUMP_BOOST_PERCENT = 0.35f;

    public Jumpman2()
    {
        this.skillInfo = Resources.Load<SkillInfo>(Skill.SKILL_INFO_DIR + "Jumpman2");
    }

    public override void activate()
    {
        if (unlocked && (parent == null || !parent.unlocked))
        {
            PlayerMovement.Instance.jumpHeight *= 1 + JUMP_BOOST_PERCENT;
            Debug.Log("Activating skill " + getID());
        }
    }

    public override string getID()
    {
        return ID;
    }
}