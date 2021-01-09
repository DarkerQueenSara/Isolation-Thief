using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
	public GameObject challengeItemPrefab;
	private Transform ChallengesList;

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
			.Find("ChallengesList");

		setChallenges();
	}
	#endregion

	public void setChallenges()
	{
		List<Challenge> challenges = GameManager.Instance.GetChallenges();
		//string challengesText = "";

		foreach (Challenge challenge in challenges)
		{
			GameObject temp = Instantiate(challengeItemPrefab, ChallengesList);
			TextMeshProUGUI challengeText = temp.transform.Find("Text").GetComponent<TextMeshProUGUI>();
			challengeText.text = challenge.description;
			if (challenge.fullfilled)
			{
				challengeText.color = new Color32(255, 255, 255, 100);
				challengeText.fontStyle = FontStyles.Strikethrough;
			}

			//challengesText += challenge.description + "\n";

		}

		//ChallengesList.text = challengesText;
	}

}
