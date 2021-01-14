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

	void updateSkillUI(string skillUILabel, string skillID)
	{
		Skill skill = skillsTree.GetSkill(skillID);

		Transform skillUI = transform.Find("Tree").Find(skillUILabel);

		skillUI.Find("Image").GetComponent<Image>().sprite = skill.getSprite();

		TextMeshProUGUI skillName = skillUI.Find("Name").GetComponent<TextMeshProUGUI>();
		skillName.text = skill.getName();

		TextMeshProUGUI skillPrice = skillUI.Find("Price").GetComponent<TextMeshProUGUI>();
		skillPrice.text = skill.getXPCost().ToString() + " XP";

		Image skillButton = skillUI.Find("Button").GetComponent<Image>();

		if (skill.unlocked)
		{
			skillPrice.transform.gameObject.SetActive(false);
			skillButton.transform.gameObject.SetActive(false);
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		this.skillsTree = GameManager.Instance.skillsTree;

		updateExperienceText();

		// Update gadget price text
		updateSkillUI("Jumpman", Jumpman.ID);
		updateSkillUI("Jumpman2", Jumpman2.ID);
		updateSkillUI("LightStep", LightStep.ID);
		updateSkillUI("LightStep2", LightStep2.ID);
		updateSkillUI("GottaGoFast", GottaGoFast.ID);
		updateSkillUI("GottaGoFast2", GottaGoFast2.ID);

	}

	private void updateExperienceText()
	{
		transform.Find("Experience").GetComponent<TextMeshProUGUI>().text = GameManager.Instance.availableXp.ToString() + " XP";
	}

}