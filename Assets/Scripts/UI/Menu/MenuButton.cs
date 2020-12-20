using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	public Animator animator;
	bool isPressing = false;

	public void Start()
	{
		animator = gameObject.GetComponent<Animator>();
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
		StartCoroutine(WaitAfterClick());
	}

	IEnumerator WaitAfterClick()
	{
		//Debug.Log("Started Coroutine at timestamp : " + Time.time);
		yield return WaitForRealSeconds(0.7f);
		//Debug.Log("Finished Coroutine at timestamp : " + Time.time);
		ClickAction();
	}

	IEnumerator WaitForRealSeconds(float time)
	{
		float start = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < start + time)
		{
			yield return null;
		}
	}

	public virtual void ClickAction()
	{

	}
}
