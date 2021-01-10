using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
	Transform loadingBarTransform;
	Slider loadingBar;
	TextMeshProUGUI ActionText;

	#region SINGLETON
	public static LoadingBar instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("More than one instance of LoadingBar found!");
		}
		instance = this;
		loadingBarTransform = transform.Find("LoadingBar");
		loadingBar = loadingBarTransform.GetComponent<Slider>();
		ActionText = loadingBarTransform.Find("ActionText").GetComponent<TextMeshProUGUI>();
	}
	#endregion

	public void SetLoadingBarStatus(float progress, string text)
	{
		loadingBar.value = progress;
		ActionText.text = text;
	}

	public void SetActive()
	{
		this.loadingBarTransform.gameObject.SetActive(true);
	}

	public void SetDisabled()
	{
		this.loadingBarTransform.gameObject.SetActive(false);
	}
}
