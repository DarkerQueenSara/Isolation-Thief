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

	void updateGadgetPriceText(string gadgetUILabel, string gadgetID)
	{
		TextMeshProUGUI gadgetPrice = transform.Find("Tree").Find(gadgetUILabel).Find("Price").GetComponent<TextMeshProUGUI>();
		gadgetPrice.text = gadgetTree.GetGadget(gadgetID).getCost().ToString() + " $";
	}

	// Start is called before the first frame update
	void Start()
	{
		this.gadgetTree = GameManager.Instance.gadgetTree;

		updateMoneyText();

		// Update gadget price text
		updateGadgetPriceText("FastLockPick", FastLockpick.gadgetID);
		updateGadgetPriceText("Lighter", Lighter.gadgetID);
		updateGadgetPriceText("HackingDevice", SimpleHackingDevice.gadgetID);
		updateGadgetPriceText("SpyCamera", SpyCam.gadgetID);

	}

	private void updateMoneyText()
	{
		transform.Find("Money").GetComponent<TextMeshProUGUI>().text = GameManager.Instance.money.ToString() + " $";
	}

}
