using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimpleHackingDevice : HackingDevice
{
	public const string gadgetID = "Simple Hacking Device";
	public System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
	private Action<int> gameEndCallback;

	public SimpleHackingDevice() : base()
	{
		usability = int.MaxValue; //infinite pmuch
		gadgetDependencies = new List<Gadget>();
		minLevel = 1;
		this.gadgetInfo = Resources.Load<GadgetInfo>(Gadget.GADGET_INFO_DIR + "SimpleHackingDevice");

	}

	public override float GetHackingTime()
	{
		float timeReduction = GameManager.Instance.level > 3 ? 3 : GameManager.Instance.level;
		return 4 - timeReduction; //3 to 1 
	}

	public override bool CanUse()
	{
		return base.CanUse();
	}

	public override void Use()
	{
		//Empty for now
	}

	private int count = 0;
	public override void HackObject(Action<int> gameEndCallback, int remainingTries)
	{
		this.gameEndCallback = gameEndCallback;
		HackingMinigameController.Instance.StartMinigame(onHackEnd, remainingTries);

	}

	private void onHackEnd(int finalResult)
	{
		if (finalResult == HackingMinigameController.WON_GAME || finalResult == HackingMinigameController.LOST_GAME)
		{
			count++;
		}
		gameEndCallback?.Invoke(finalResult);
	}
}
