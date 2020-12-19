using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartButton : MainMenuButton
{
	public override void Action()
	{
		StartCoroutine(WaitAfterClick());
	}

	IEnumerator WaitAfterClick()
	{
		Debug.Log("Started Coroutine at timestamp : " + Time.time);
		yield return new WaitForSeconds(1);
		Debug.Log("Finished Coroutine at timestamp : " + Time.time);

		GameManager.Instance.StartGame();
		Debug.Log("Start clicked");
	}
}
