using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	public Animator animator;

	public void Start()
	{
		animator = gameObject.GetComponent<Animator>();
	}

	public void OnPointerEnter(PointerEventData e)
	{
		animator.SetBool("selected", true);
		Debug.Log("Mouse is over menu button");
	}

	public void OnPointerExit(PointerEventData e)
	{
		animator.SetBool("selected", false);
		animator.SetBool("pressed", false);
		Debug.Log("Mouse exited menu button");
	}

	public void OnPointerClick(PointerEventData e)
	{
		animator.SetBool("pressed", true);
		Debug.Log("Mouse click menu button");
		Action();
	}

	public virtual void Action()
	{

	}
}
