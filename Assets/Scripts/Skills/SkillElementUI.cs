using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SkillElementUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	public Animator animator;
	private SkillsTree skillsTree;
	public UnityEvent addFunction;

	// Start is called before the first frame update
	void Start()
	{
		animator = transform.Find("Button").GetComponent<Animator>();
		this.skillsTree = GameManager.Instance.skillsTree;
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
		Debug.Log("Skill button test action");
	}

	void AddSkill(string skillID, string skillName)
	{
		Skill skill = skillsTree.GetSkill(skillID);
		if (skill.canUnlock())
		{
			Debug.Log("unlocking skill!");
			skill.unlock();
			Image button = transform.Find("Button").GetComponent<Image>();
			TextMeshProUGUI price = transform.Find("Price").GetComponent<TextMeshProUGUI>();
			SkillsTreeUI.Instance.updateVisualUnocked(button, price);
			SkillsTreeUI.Instance.updateHelpText(skillName + " unlocked");
		}
		else
		{
			Debug.Log("Can't unlock skill!");
			SkillsTreeUI.Instance.updateHelpText("Can't unlock " + skillName);
		}
	}

	public void AddJumpman()
	{
		AddSkill(Jumpman.ID, "Jumpman");
	}

	public void AddJumpman2()
	{
		AddSkill(Jumpman2.ID, "Jumpman 2");
	}

	public void AddLightStep()
	{
		AddSkill(LightStep.ID, "Light Step");
	}

	public void AddLightStep2()
	{
		AddSkill(LightStep2.ID, "Light Step 2");
	}

}
