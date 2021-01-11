using UnityEngine;
using System.Collections;


public class GottaGoFast : Skill
{

    public const string ID = "GottaGoFast";
    public const float SPEED_BOOST_PERCENT = 0.2f;
    public GottaGoFast()
    {
        this.unlocked = true;
        this.skillInfo = Resources.Load<SkillInfo>(Skill.SKILL_INFO_DIR + "GottaGoFast");
    }

    public override void activate()
    {
        if (unlocked && (parent == null || !parent.unlocked))
        {
            PlayerMovement.Instance.crouchSpeed += SPEED_BOOST_PERCENT;
            PlayerMovement.Instance.speed += SPEED_BOOST_PERCENT;
            PlayerMovement.Instance.proneSpeed += SPEED_BOOST_PERCENT;
            PlayerMovement.Instance.sprintSpeed+= SPEED_BOOST_PERCENT;
            Debug.Log("Activating skill " + getID());
        }
    }

    public override string getID()
    {
        return ID;
    }
}