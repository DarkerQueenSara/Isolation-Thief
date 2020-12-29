using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationDialog : MonoBehaviour
{
	bool showConfirmationDialog = false;

	#region SINGLETON
	public static ConfirmationDialog Instance;

	private void Awake()
	{
		if (Instance != null)
		{
			Debug.LogWarning("More than one instance of ConfirmationDialog found!");
		}

		Instance = this;

		gameObject.SetActive(false);
	}
	#endregion

	public bool isVisible()
	{
		return showConfirmationDialog;
	}

	public void visible()
	{
		if (showConfirmationDialog)
		{
			showConfirmationDialog = false;

		}
		else
		{
			showConfirmationDialog = true;
		}

		if (showConfirmationDialog)
		{
			gameObject.SetActive(true);
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

}
