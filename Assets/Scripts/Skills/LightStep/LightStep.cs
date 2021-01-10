using UnityEngine;
using System.Collections;


public class LightStep : Skill
{

    public const string ID = "LightStep";
    public const float NOISE_REDUCTION_PERCENT = 0.25f;

    public LightStep()
    {
        unlocked = true;
        this.skillInfo = Resources.Load<SkillInfo>(Skill.SKILL_INFO_DIR + "LightStep");
    }

    public override void activate()
    {
        if (unlocked && (parent == null || !parent.unlocked))
        {
            Player.Instance.GetAudioManager().ChangeVolume("Sprint", (1 - NOISE_REDUCTION_PERCENT));
            Debug.Log("Activating skill " + getID());
        }
    }

    public override string getID()
    {
        return ID;
    }
}