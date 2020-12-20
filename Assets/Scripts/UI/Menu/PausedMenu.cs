using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PausedMenu : MonoBehaviour
{
	bool showPausedMenu = false;

	#region SINGLETON
	public static PausedMenu Instance;

	private void Awake()
	{
		if (Instance != null)
		{
			Debug.LogWarning("More than one instance of PausedMenu found!");
		}

		Instance = this;

		gameObject.SetActive(false);
	}
	#endregion

	public bool isVisible()
	{
		return showPausedMenu;
	}

	public void visible()
	{
		if (showPausedMenu)
		{
			showPausedMenu = false;

		}
		else
		{
			showPausedMenu = true;
		}

		if (showPausedMenu)
		{
			Time.timeScale = 0;
			gameObject.SetActive(true);
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else
		{
			Time.timeScale = 1;
			gameObject.SetActive(false);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
}
