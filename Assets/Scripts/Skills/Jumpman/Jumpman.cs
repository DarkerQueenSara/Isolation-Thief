using UnityEngine;
using System.Collections;


public class Jumpman : Skill
{

    public const string ID = "Jumpman";
    public const float JUMP_BOOST_PERCENT = 0.2f;
    public Jumpman()
    {
        unlocked = true;
        this.skillInfo = Resources.Load<SkillInfo>(Skill.SKILL_INFO_DIR + "Jumpman");
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