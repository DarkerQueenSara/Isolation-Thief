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

	public void AddFastLockpick()
	{
		Gadget gadget = gadgetTree.GetGadget(FastLockpick.gadgetID);
		if (gadget.canUnlock())
		{
			Debug.Log("unlocking gadget!");
			gadget.unlock();
			Image FastLockPickBtn = transform.Find("Button").GetComponent<Image>();
			TextMeshProUGUI FastLockPickPrice = transform.Find("Price").GetComponent<TextMeshProUGUI>();
			GadgetTreeUI.Instance.updateVisualUnocked(FastLockPickBtn, FastLockPickPrice);
			GadgetTreeUI.Instance.updateHelpText("Fast Lockpick unlocked");
		}
		else
		{
			Debug.Log("Can't unlock gadget!");
			GadgetTreeUI.Instance.updateHelpText("Can't unlock Fast Lockpick");
		}
	}

}
