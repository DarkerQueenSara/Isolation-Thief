using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuitButton : MainMenuButton
{
	public override void Action()
	{
		Debug.Log("Quit clicked");
		Application.Quit();
	}
}
