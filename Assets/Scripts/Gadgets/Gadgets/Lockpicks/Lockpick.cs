using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockpick : Gadget
{
	public LoadingBar loadingBar;
	[HideInInspector]
	//protected AudioManager playerAudioManager;

	public Lockpick() : base()
	{
		this.loadingBar = LoadingBar.instance;
		//Debug.Log(loadingBar);
		//playerAudioManager = GameObject.FindGameObjectWithTag("Player")?.GetComponent<AudioManager>();
	}

	public override bool CanUse()
	{

		return base.CanUse();
	}

	public override void Use()
	{
		//empty for now
	}

	public virtual bool LockpickObject()
	{
		//this method is ment to be overwriten
		return false;
	}
	public virtual void stopPicking()
	{
		//this method is ment to be overwriten
	}


	public virtual float GetLockPickingTime()
	{
		return float.MaxValue;
	}

	public override GadgetType getGadgetType()
	{
		return GadgetType.LOCKPICK;
	}
}
