﻿using UnityEngine;
using System.Collections;


public class Matrix : KarmaSkill
{
    public const string ID = "Slow Cops";
    public const float EXTRA_TIME = 5.0f;

    public Matrix()
    {
        this.skillInfo = Resources.Load<SkillInfo>(KarmaSkill.KARMA_SKILL_INFO_DIR + "Matrix");
    }

    public override void activate()
    {
        if (unlocked)
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