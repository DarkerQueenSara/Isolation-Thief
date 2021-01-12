using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KarmaSkillsTreeUI : MonoBehaviour
{
	public static KarmaSkillsTreeUI Instance { get; private set; }

	public KarmaSkillsTree karmaSkillsTree;

	private void Awake()
	{
		Instance = this;
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
		updateKarmaText();
	}

	public void updateHelpText(string text)
	{
		TextMeshProUGUI HelpText = transform.Find("HelpText").GetComponent<TextMeshProUGUI>();
		HelpText.text = text;
		HelpText.GetComponent<Animator>().Play("FadeOut");
		HelpText.transform.gameObject.SetActive(true);
	}

	void updateKarmaSkillPriceText(string karmaSkillUILabel, string karmaSkillID)
	{
		TextMeshProUGUI skillPrice = transform.Find("Tree").Find(karmaSkillUILabel).Find("Price").GetComponent<TextMeshProUGUI>();
		skillPrice.text = karmaSkillsTree.GetSkill(karmaSkillID).getXPCost().ToString() + " XP";
	}

	// Start is called before the first frame update
	void Start()
	{
		this.karmaSkillsTree = GameManager.Instance.karmaSkillsTree;

		updateKarmaText();

		// Update gadget price text
		updateKarmaSkillPriceText("Matrix", Matrix.ID);

	}

	private void updateKarmaText()
	{
		transform.Find("Karma").GetComponent<TextMeshProUGUI>().text = GameManager.Instance.availableKp.ToString() + " KP";
	}

}