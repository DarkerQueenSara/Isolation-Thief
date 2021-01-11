using UnityEngine;
using System.Collections;


public class GottaGoFast2 : Skill
{

    public const string ID = "GottaGoFast2";
    public const float SPEED_BOOST_PERCENT = 0.35f;

    public GottaGoFast2()
    {
        this.skillInfo = Resources.Load<SkillInfo>(Skill.SKILL_INFO_DIR + "GottaGoFast2");
    }

    public override void activate()
    {
        if (unlocked && (parent == null || !parent.unlocked))
        {
            PlayerMovement.Instance.crouchSpeed += SPEED_BOOST_PERCENT;
            PlayerMovement.Instance.speed += SPEED_BOOST_PERCENT;
            PlayerMovement.Instance.proneSpeed += SPEED_BOOST_PERCENT;
            PlayerMovement.Instance.sprintSpeed += SPEED_BOOST_PERCENT;
            Debug.Log("Activating skill " + getID());
        }
    }

    public override string getID()
    {
        return ID;
    }
}