using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillsTreeUI : MonoBehaviour
{
	public static SkillsTreeUI Instance { get; private set; }

	public SkillsTree skillsTree;

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
		updateExperienceText();
	}

	public void updateHelpText(string text)
	{
		TextMeshProUGUI HelpText = transform.Find("HelpText").GetComponent<TextMeshProUGUI>();
		HelpText.text = text;
		HelpText.GetComponent<Animator>().Play("FadeOut");
		HelpText.transform.gameObject.SetActive(true);
	}

	void updateSkillPriceText(string skillUILabel, string skillID)
	{
		TextMeshProUGUI skillPrice = transform.Find("Tree").Find(skillUILabel).Find("Price").GetComponent<TextMeshProUGUI>();
		skillPrice.text = skillsTree.GetSkill(skillID).getXPCost().ToString() + " XP";
	}

	// Start is called before the first frame update
	void Start()
	{
		this.skillsTree = GameManager.Instance.skillsTree;

		updateExperienceText();

		// Update gadget price text
		updateSkillPriceText("Jumpman", Jumpman.ID);
		updateSkillPriceText("Jumpman2", Jumpman2.ID);
		updateSkillPriceText("LightStep", LightStep.ID);
		updateSkillPriceText("LightStep2", LightStep2.ID);

	}

	private void updateExperienceText()
	{
		transform.Find("Experience").GetComponent<TextMeshProUGUI>().text = GameManager.Instance.availableXp.ToString() + " XP";
	}

}