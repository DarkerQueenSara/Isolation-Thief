using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
	public GameObject challengeItemPrefab;

	#region SINGLETON
	public static LevelMenu Instance;

	private void Awake()
	{
		if (Instance != null)
		{
			Debug.LogWarning("More than one instance of LevelMenu found!");
		}
		Instance = this;
	}
	#endregion

	void Start()
	{
		setChallenges();
		setGoodDeeds();
		setPlayerInfo();

	}

	public void setChallenges()
	{
		List<Challenge> challenges = GameManager.Instance.GetChallenges();
		int completedChallenges = challenges.FindAll(c => c.fullfilled).Count;

		Transform challengeContainer = transform.Find("Challenges");

		TextMeshProUGUI title = challengeContainer.Find("Title").GetComponent<TextMeshProUGUI>();
		title.text = "Challenges (" + completedChallenges.ToString() + "/" + challenges.Count.ToString() + ")";

		Transform challengesList = challengeContainer
			   .Find("Scroll View")
			   .Find("Viewport")
			   .Find("ChallengesList");


		foreach (Challenge challenge in challenges)
		{
			GameObject temp = Instantiate(challengeItemPrefab, challengesList);
			TextMeshProUGUI challengeText = temp.transform.Find("Text").GetComponent<TextMeshProUGUI>();
			challengeText.text = challenge.description;
			if (challenge.fullfilled)
			{
				challengeText.color = new Color32(255, 255, 255, 100);
				challengeText.fontStyle = FontStyles.Strikethrough;
			}
		}
	}

	public void setGoodDeeds()
	{
		List<Challenge> goodDeeds = GameManager.Instance.GetGoodDeeds();
		int completedGoodDeeds = goodDeeds.FindAll(c => c.fullfilled).Count;

		Transform challengeContainer = transform.Find("GoodDeeds");

		TextMeshProUGUI title = challengeContainer.Find("Title").GetComponent<TextMeshProUGUI>();
		title.text = "Good Deeds (" + completedGoodDeeds.ToString() + "/" + goodDeeds.Count.ToString() + ")";

		Transform challengesList = challengeContainer
			   .Find("Scroll View")
			   .Find("Viewport")
			   .Find("GoodDeedsList");


		foreach (Challenge goodDeed in goodDeeds)
		{
			GameObject temp = Instantiate(challengeItemPrefab, challengesList);
			TextMeshProUGUI challengeText = temp.transform.Find("Text").GetComponent<TextMeshProUGUI>();
			challengeText.text = goodDeed.name;
			if (goodDeed.fullfilled)
			{
				challengeText.color = new Color32(255, 255, 255, 100);
				challengeText.fontStyle = FontStyles.Strikethrough;
			}
		}
	}

	public void setPlayerInfo()
	{
		Transform playerInfo = transform.Find("PlayerInfo");
		TextMeshProUGUI money = playerInfo.Find("Money").GetComponent<TextMeshProUGUI>();
		TextMeshProUGUI experience = playerInfo.Find("Experience").GetComponent<TextMeshProUGUI>();
		TextMeshProUGUI karma = playerInfo.Find("Karma").GetComponent<TextMeshProUGUI>();

		money.text = GameManager.Instance.money.ToString() + " $";
		experience.text = GameManager.Instance.availableXp.ToString() + " XP";
		karma.text = GameManager.Instance.availableKp.ToString() + " KP";
	}
}
