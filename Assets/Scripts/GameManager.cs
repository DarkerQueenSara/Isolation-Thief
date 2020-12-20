using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public float moneyGoal = 2500.0f;
	public float timeTillCops = 15.0f;

	public static GameManager Instance;
	private void Awake()
	{
		Instance = this;
	}

	private Player player;

	public bool copsCalled;
	public bool copsArrived;
	public bool hasEnded;

	// Start is called before the first frame update
	void Start()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		hasEnded = false;
		MainMenu.instance.visible();
	}

	public void StartGame()
	{
		MainMenu.instance.visible();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public bool CanPause()
	{
		return !MainMenu.instance.isVisible() && !LevelEndMenu.Instance.isVisible();
	}

	public void callCops()
	{
		this.copsCalled = true;
		SoundManagerScript.instance.PlaySoundGradually(SoundManagerScript.POLICE_SIRENS, timeTillCops);
	}

	public void endGame()
	{
		Debug.Log("End Game");

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		hasEnded = true;
		SoundManagerScript.instance.stopSound();

		if (copsArrived) //Lose
		{
			LevelEndMenu.Instance.setText(
				"You LOST!",
				"You were caught by the cops!",
				moneyGoal + " $",
				+player.GetTotalStolen() + " $",
				false);
		}
		else if (player.GetTotalStolen() < moneyGoal) //Lose
		{
			LevelEndMenu.Instance.setText(
				"You LOST!",
				"You escaped, but you didn't steal enough...",
				moneyGoal + " $",
				+player.GetTotalStolen() + " $",
				false);
		}
		else //win
		{
			LevelEndMenu.Instance.setText(
				"You WON!",
				"You escaped!",
				moneyGoal + " $",
				+player.GetTotalStolen() + " $",
				true);
		}
		LevelEndMenu.Instance.visible();

		player.DisableMovement();
	}


	// Update is called once per frame
	void Update()
	{

	}
}
