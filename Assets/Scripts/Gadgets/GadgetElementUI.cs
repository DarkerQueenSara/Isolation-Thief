using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class GadgetElementUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	public Animator animator;
	private GadgetTree gadgetTree;
	public UnityEvent addFunction;

	// Start is called before the first frame update
	void Start()
	{
		animator = transform.Find("Button").GetComponent<Animator>();
		this.gadgetTree = GameManager.Instance.gadgetTree;
	}

	public void OnPointerEnter(PointerEventData e)
	{
		animator.SetBool("selected", true);
	}

	public void OnPointerExit(PointerEventData e)
	{
		animator.SetBool("selected", false);
		animator.SetBool("pressed", false);
	}

	public void OnPointerClick(PointerEventData e)
	{
		animator.SetBool("pressed", true);
		addFunction.Invoke();
	}

	public void TestAction()
	{
		Debug.Log("Gadget button test action");
	}

	void AddGadget(string gadgetID, string gadgetName)
	{
		Gadget gadget = gadgetTree.GetGadget(gadgetID);
		if (gadget.canUnlock())
		{
			Debug.Log("unlocking gadget!");
			gadget.unlock();
			Image button = transform.Find("Button").GetComponent<Image>();
			TextMeshProUGUI price = transform.Find("Price").GetComponent<TextMeshProUGUI>();
			GadgetTreeUI.Instance.updateVisualUnocked(button, price);
			GadgetTreeUI.Instance.updateHelpText(gadgetName + " unlocked");
		}
		else
		{
			Debug.Log("Can't unlock gadget!");
			GadgetTreeUI.Instance.updateHelpText("Can't unlock " + gadgetName);
		}
	}

	public void AddFastLockpick()
	{
		AddGadget(FastLockpick.gadgetID, "Fast Lockpick");
	}

	public void AddLighter()
	{
		AddGadget(Lighter.gadgetID, "Lighter");
	}

	public void AddHackingDevice()
	{
		AddGadget(SimpleHackingDevice.gadgetID, "Hacking Device");
	}

	public void AddSpyCamera()
	{
		AddGadget(SpyCam.gadgetID, "Spy Camera");
	}

}
