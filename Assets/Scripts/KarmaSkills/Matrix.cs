using UnityEngine;
using System.Collections;


public class Matrix : KarmaSkill
{
    public const string ID = "Matrix";
    public const float EXTRA_TIME = 5.0f;

    public Matrix()
    {
        this.unlocked = true;
        this.skillInfo = Resources.Load<SkillInfo>(KarmaSkill.KARMA_SKILL_INFO_DIR + "Matrix");
    }

    public override void activate()
    {
        if (unlocked && (parent == null || !parent.unlocked))
        {
            LevelManager.Instance.timeTillCops += EXTRA_TIME;
            Debug.Log("Activating skill " + getID());
        }
    }

    public override string getID()
    {
        return ID;
    }
}