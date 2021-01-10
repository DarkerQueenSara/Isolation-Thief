using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetTree
{
	public Dictionary<string, Gadget> gadgets;


	public GadgetTree()
	{
		SimpleLockpick simpleLockpick = new SimpleLockpick();
		FastLockpick fastLockpick = new FastLockpick();
		SimpleHackingDevice simpleHackingDevice = new SimpleHackingDevice();
		Lighter lighter = new Lighter();
		SpyCam spyCam = new SpyCam();

		this.gadgets = new Dictionary<string, Gadget>();
		this.gadgets.Add(SimpleLockpick.gadgetID, simpleLockpick);
		this.gadgets.Add(FastLockpick.gadgetID, fastLockpick);
		this.gadgets.Add(SimpleHackingDevice.gadgetID, simpleHackingDevice);
		this.gadgets.Add(Lighter.gadgetID, lighter);
		this.gadgets.Add(SpyCam.gadgetID, spyCam);

		fastLockpick.setGadgetDependencies(new List<Gadget>() { simpleLockpick });
		lighter.setGadgetDependencies(new List<Gadget>() { simpleLockpick });
		simpleHackingDevice.setGadgetDependencies(new List<Gadget>() { fastLockpick });
		spyCam.setGadgetDependencies(new List<Gadget>() { lighter });
	}

	public Gadget GetGadget(string gadgetName)
	{
		Gadget gadget = gadgets.ContainsKey(gadgetName) ? gadgets[gadgetName] : null;

		if (gadget != null)
		{
			return gadget;
		}

		else return null;

	}

	public List<Gadget> getUnlockedGadgets()
	{
		List<Gadget> unlockedGadgets = new List<Gadget>();
		foreach (Gadget gadget in this.gadgets.Values)
		{
			if (gadget.unlocked)
			{
				unlockedGadgets.Add(gadget);
			}
		}
		return unlockedGadgets;
	}

	public List<Gadget> getAllGadgets()
	{
		return new List<Gadget>(this.gadgets.Values);
	}


}
