using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
	TextMeshProUGUI ChallengesList;

	#region SINGLETON
	public static LevelMenu Instance;

	private void Awake()
	{
		if (Instance != null)
		{
			Debug.LogWarning("More than one instance of LevelMenu found!");
		}
		Instance = this;

		ChallengesList = transform
			.Find("Challenges")
			.Find("Scroll View")
			.Find("Viewport")
			.Find("ChallengesList").GetComponent<TextMeshProUGUI>();

		setChallengesText();
	}
	#endregion

	public void setChallengesText()
	{
		List<Challenge> challenges = GameManager.Instance.GetChallenges();
		string challengesText = "";

		foreach (Challenge challenge in challenges)
		{
			challengesText += challenge.description + "\n";
		}

		ChallengesList.text = challengesText;
	}

}
