using UnityEngine;
using System.Collections;


public class LightStep2 : Skill
{

    public const string ID = "LightStep2";
    public const float NOISE_REDUCTION_PERCENT = 0.50f;

    public LightStep2()
    {
        this.skillInfo = Resources.Load<SkillInfo>(Skill.SKILL_INFO_DIR + "LightStep2");
    }

    public override void activate()
    {
        if (unlocked && (parent == null || !parent.unlocked))
        {
            Player.Instance.GetAudioManager().ChangeVolume("Sprint1", (1 - NOISE_REDUCTION_PERCENT));
            Player.Instance.GetAudioManager().ChangeVolume("Sprint2", (1 - NOISE_REDUCTION_PERCENT));
            Player.Instance.GetAudioManager().ChangeVolume("Sprint2", (1 - NOISE_REDUCTION_PERCENT));
            Player.Instance.GetAudioManager().ChangeVolume("Footstep1", (1 - NOISE_REDUCTION_PERCENT));
            Player.Instance.GetAudioManager().ChangeVolume("Footstep3", (1 - NOISE_REDUCTION_PERCENT));
            Player.Instance.GetAudioManager().ChangeVolume("Footstep4", (1 - NOISE_REDUCTION_PERCENT));
            Player.Instance.GetAudioManager().ChangeVolume("Sneak1", (1 - NOISE_REDUCTION_PERCENT));
            Player.Instance.GetAudioManager().ChangeVolume("Sneak2", (1 - NOISE_REDUCTION_PERCENT));
            Player.Instance.GetAudioManager().ChangeVolume("Sneak3", (1 - NOISE_REDUCTION_PERCENT));
            Debug.Log("Activating skill " + getID());
        }
    }

    public override string getID()
    {
        return ID;
    }
}