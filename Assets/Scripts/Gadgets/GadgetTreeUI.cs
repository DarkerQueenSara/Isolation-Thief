using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GadgetTreeUI : MonoBehaviour
{
	public static GadgetTreeUI Instance { get; private set; }
	//public GameObject crosshair;

	private GadgetTree gadgetTree;

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

	// Start is called before the first frame update
	void Start()
	{
		this.gadgetTree = GameManager.Instance.gadgetTree;

		updateMoneyText();

		// Update gadget price text
		TextMeshProUGUI FastLockPickPrice = transform.Find("Tree").Find("FastLockPick").Find("Price").GetComponent<TextMeshProUGUI>();
		FastLockPickPrice.text = gadgetTree.GetGadget(FastLockpick.gadgetID).getCost().ToString() + " $";
	}

	private void updateMoneyText()
	{
		transform.Find("Money").GetComponent<TextMeshProUGUI>().text = GameManager.Instance.money.ToString() + " $";
	}

}
