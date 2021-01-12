using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class KarmaSkillElementUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	public Animator animator;
	private KarmaSkillsTree karmaskillsTree;
	public UnityEvent addFunction;

	// Start is called before the first frame update
	void Start()
	{
		animator = transform.Find("Button").GetComponent<Animator>();
		this.karmaskillsTree = GameManager.Instance.karmaSkillsTree;
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
		KarmaSkill skill = karmaskillsTree.GetSkill(skillID);
		if (skill.canUnlock())
		{
			Debug.Log("unlocking skill!");
			skill.unlock();
			Image button = transform.Find("Button").GetComponent<Image>();
			TextMeshProUGUI price = transform.Find("Price").GetComponent<TextMeshProUGUI>();
			KarmaSkillsTreeUI.Instance.updateVisualUnocked(button, price);
			KarmaSkillsTreeUI.Instance.updateHelpText(skillName + " unlocked");
		}
		else
		{
			Debug.Log("Can't unlock skill!");
			KarmaSkillsTreeUI.Instance.updateHelpText("Can't unlock " + skillName);
		}
	}

	public void AddMatrix()
	{
		AddSkill(Matrix.ID, "Matrix");
	}

}
