using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndMenu : MonoBehaviour
{
	TextMeshProUGUI Result;
	TextMeshProUGUI Description;
	TextMeshProUGUI Goal;
	TextMeshProUGUI Stolen;

	Color winColor = new Color(0.3199871f, 0.8867924f, 0.2802598f, 1f);
	Color loseColor = new Color(0.7735849f, 0.0693307f, 0.09777957f, 1f);

	bool showLevelEndMenu = false;

	#region SINGLETON
	public static LevelEndMenu Instance;

	private void Awake()
	{
		if (Instance != null)
		{
			Debug.LogWarning("More than one instance of LevelEndMenu found!");
		}
		Instance = this;

		Result = transform.Find("Result").GetComponent<TextMeshProUGUI>();
		Description = transform.Find("Description").GetComponent<TextMeshProUGUI>();
		Goal = transform.Find("Goal").GetComponent<TextMeshProUGUI>();
		Stolen = transform.Find("Stolen").GetComponent<TextMeshProUGUI>();

		gameObject.SetActive(false);
	}
	#endregion

	public void setText(string result, string description, string goal, string stolen, bool win)
	{
		Result.text = result;
		Description.text = description;
		Goal.text = goal;
		Stolen.text = stolen;

		if (win) Result.color = winColor;
		else Result.color = loseColor;
	}

	public bool isVisible()
	{
		return showLevelEndMenu;
	}

	public void visible()
	{
		if (showLevelEndMenu)
		{
			showLevelEndMenu = false;

		}
		else
		{
			showLevelEndMenu = true;
		}

		if (showLevelEndMenu)
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
