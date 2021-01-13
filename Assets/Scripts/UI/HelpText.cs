using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HelpText : MonoBehaviour
{
	TextMeshProUGUI textUI;
	bool hasText;


	#region SINGLETON
	public static HelpText instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("More than one instance of HelpText found!");
		}
		instance = this;
		textUI = GetComponent<TextMeshProUGUI>();
	}
	#endregion

	public void updateHelpText(string text)
	{
		this.textUI.text = text;
		textUI.text = text;
		textUI.GetComponent<Animator>().Play("FadeOut");
		textUI.transform.gameObject.SetActive(true);
	}

	public bool HasText()
	{
		return hasText;
	}
}
