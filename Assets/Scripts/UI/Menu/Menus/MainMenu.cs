using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	bool showMainMenu = false;

	#region SINGLETON
	public static MainMenu instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("More than one instance of MainMenu found!");
		}

		instance = this;
	}
	#endregion

	public bool isVisible()
	{
		return showMainMenu;
	}

	public void visible()
	{
		if (showMainMenu)
		{
			showMainMenu = false;

		}
		else
		{
			showMainMenu = true;
		}

		if (showMainMenu)
		{
			gameObject.SetActive(true);
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else
		{
			gameObject.SetActive(false);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
}
