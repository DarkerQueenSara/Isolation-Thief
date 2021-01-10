using UnityEngine;
using System.Collections;


public class LightStep2 : Skill
{

    public const string ID = "LightStep2";
    public const float NOISE_REDUCTION_PERCENT = 0.50f;

    public LightStep2()
    {
        unlocked = true;
        this.skillInfo = Resources.Load<SkillInfo>(Skill.SKILL_INFO_DIR + "LightStep2");
    }

    public override void activate()
    {
        if (unlocked && (parent == null || !parent.unlocked))
        {
            Player.Instance.GetAudioManager().ChangeVolume("Sprint", (1 - NOISE_REDUCTION_PERCENT));
            Player.Instance.GetAudioManager().ChangeVolume("Walk", (1 - NOISE_REDUCTION_PERCENT));
            Player.Instance.GetAudioManager().ChangeVolume("Sneak", (1 - NOISE_REDUCTION_PERCENT));
            Debug.Log("Activating skill " + getID());
        }
    }

    public override string getID()
    {
        return ID;
    }
}