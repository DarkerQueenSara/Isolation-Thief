using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	public Animator animator;
	public UnityEvent clickAction;

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
		clickAction.Invoke();
	}

	IEnumerator WaitForRealSeconds(float time)
	{
		float start = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < start + time)
		{
			yield return null;
		}
	}

	public void TestAction()
	{
		Debug.Log("Menu button clicked");
	}

	// Main Menu

	public void NewGameAction()
	{
		Debug.Log("New game clicked");
		GameManager.Instance.NewGame();
	}

	public void LoadGameAction()
	{
		Debug.Log("Load game clicked");
	}

	public void QuitGameAction()
	{
		Debug.Log("Quit clicked");
		Application.Quit();
	}

	// Level Menu

	public void StartLevelAction()
	{
		Debug.Log("Start Level clicked");
		GameManager.Instance.StartLevel();
	}

	public void OpenGadgetsMenu()
	{
		Debug.Log("Gadgets clicked");
		GameManager.Instance.ShowGadgetsMenu();
	}

	public void OpenSkillsMenu()
	{
		Debug.Log("Skills clicked");
		GameManager.Instance.ShowSkillsMenu();
	}

	// Paused Menu

	public void ContinueLevelAction()
	{
		Debug.Log("Continue clicked");
		PausedMenu.Instance.visible();
	}

	public void RestartLevelAction()
	{
		Debug.Log("Restart clicked");
		NPCManager.Instance.StopAllNPC();
		PausedMenu.Instance.visible();
		GameManager.Instance.StartLevel();
	}

	public void QuitLevelAction()
	{
		Debug.Log("Quit level clicked");
		if (PausedMenu.Instance.isVisible())
		{
			ConfirmationDialog.Instance.visible();
		}
	}

	public void ConfirmDialogYesAction()
	{
		NPCManager.Instance.StopAllNPC();
		Time.timeScale = 1;
		GameManager.Instance.ShowLevelMenu();
	}

	public void ConfirmDialogNoAction()
	{
		ConfirmationDialog.Instance.visible();
	}

	public void ShowLevelMenuAction()
	{
		GameManager.Instance.ShowLevelMenu();
	}

	public void ShowMainMenuAction()
	{
		GameManager.Instance.ShowMainMenu();
	}

}
