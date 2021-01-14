using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bodybuilder : KarmaSkill
{
	public const string ID = "Bodybuilder";
	public const float EXTRA_WEIGHT = 25.0f;

	public Bodybuilder()
	{
		this.skillInfo = Resources.Load<SkillInfo>(KarmaSkill.KARMA_SKILL_INFO_DIR + "Bodybuilder");
	}
	public override void activate()
	{
		if (unlocked)
		{
			LevelManager.Instance.player.inventory.extraWeight = EXTRA_WEIGHT;
			Debug.Log("Activating skill " + getID());
		}
	}

	public override string getID()
	{
		return ID;
	}
}
