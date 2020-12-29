using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuitButton : MenuButton
{
	public override void ClickAction()
	{
		Debug.Log("Quit clicked");
		Application.Quit();
	}
}
