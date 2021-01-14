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
		updateKarmaSkills();
	}

	public void updateHelpText(string text)
	{
		TextMeshProUGUI HelpText = transform.Find("HelpText").GetComponent<TextMeshProUGUI>();
		HelpText.text = text;
		HelpText.GetComponent<Animator>().Play("FadeOut");
		HelpText.transform.gameObject.SetActive(true);
	}

	void updateKarmaSkillUI(string karmaSkillUILabel, string karmaSkillID)
	{
		KarmaSkill karmaSkill = karmaSkillsTree.GetSkill(karmaSkillID);

		Transform karmaSkillUI = transform.Find("Tree").Find(karmaSkillUILabel);

		karmaSkillUI.Find("Image").GetComponent<Image>().sprite = karmaSkill.getSprite();

		TextMeshProUGUI karmaSkillName = karmaSkillUI.Find("Name").GetComponent<TextMeshProUGUI>();
		karmaSkillName.text = karmaSkill.getName();

		TextMeshProUGUI karmaSkillPrice = karmaSkillUI.Find("Price").GetComponent<TextMeshProUGUI>();
		karmaSkillPrice.text = karmaSkill.getXPCost().ToString() + " KP";

		Image karmaSkillButton = karmaSkillUI.Find("Button").GetComponent<Image>();

		if (karmaSkill.unlocked)
		{
			karmaSkillPrice.transform.gameObject.SetActive(false);
			karmaSkillButton.transform.gameObject.SetActive(false);
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		this.karmaSkillsTree = GameManager.Instance.karmaSkillsTree;

		updateKarmaText();

		updateKarmaSkills();
	}

	void updateKarmaSkills()
	{
		updateKarmaSkillUI("SlowCops", Matrix.ID);
		updateKarmaSkillUI("Bodybuilder", Bodybuilder.ID);
	}

	private void updateKarmaText()
	{
		transform.Find("Karma").GetComponent<TextMeshProUGUI>().text = GameManager.Instance.availableKp.ToString() + " KP";
	}

}