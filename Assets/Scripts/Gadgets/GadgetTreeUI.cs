using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GadgetTreeUI : MonoBehaviour
{
	public static GadgetTreeUI Instance { get; private set; }
	//public GameObject crosshair;

	public GadgetTree gadgetTree;

	private void Awake()
	{
		Instance = this;
		//transform.Find("SimpleLockPickBtn FastLockPickBtn LanternBtn")
	}

	void disableButton(Button button)
	{
		//button.enabled = false;
		button.transform.gameObject.SetActive(false);
	}

	public void updateVisualUnocked(Image button, TextMeshProUGUI priceText)
	{
		button.transform.gameObject.SetActive(false);
		priceText.transform.gameObject.SetActive(false);
		updateMoneyText();
	}

	public void updateHelpText(string text)
	{
		TextMeshProUGUI HelpText = transform.Find("HelpText").GetComponent<TextMeshProUGUI>();
		HelpText.text = text;
		HelpText.GetComponent<Animator>().Play("FadeOut");
		HelpText.transform.gameObject.SetActive(true);
	}

	void updateGadgetUI(string gadgetUILabel, string gadgetID)
	{
		Gadget gadget = gadgetTree.GetGadget(gadgetID);

		Transform gadgetUI = transform.Find("Tree").Find(gadgetUILabel);

		gadgetUI.Find("Image").GetComponent<Image>().sprite = gadget.getSprite();

		TextMeshProUGUI gadgetName = gadgetUI.Find("Name").GetComponent<TextMeshProUGUI>();
		gadgetName.text = gadget.getID();

		TextMeshProUGUI gadgetPrice = gadgetUI.Find("Price").GetComponent<TextMeshProUGUI>();
		gadgetPrice.text = gadget.getCost().ToString() + " $";

		Image gadgetButton = gadgetUI.Find("Button").GetComponent<Image>();

		if (gadget.unlocked)
		{
			gadgetPrice.transform.gameObject.SetActive(false);
			gadgetButton.transform.gameObject.SetActive(false);
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		this.gadgetTree = GameManager.Instance.gadgetTree;

		updateMoneyText();

		// Update gadget price text
		updateGadgetUI("SimpleLockPick", SimpleLockpick.gadgetID);
		updateGadgetUI("FastLockPick", FastLockpick.gadgetID);
		updateGadgetUI("Lighter", Lighter.gadgetID);
		updateGadgetUI("HackingDevice", SimpleHackingDevice.gadgetID);
		updateGadgetUI("SpyCamera", SpyCam.gadgetID);

	}

	private void updateMoneyText()
	{
		transform.Find("Money").GetComponent<TextMeshProUGUI>().text = GameManager.Instance.money.ToString() + " $";
	}

}
