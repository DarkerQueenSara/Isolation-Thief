using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	private static LevelManager instance;

	public static LevelManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
			}
			return instance;
		}
	}

	private Player player;

	public float moneyGoal = 2500.0f;
	public float timeTillCops = 15.0f;
	public bool copsCalled { get; private set; }
	public bool copsArrived;
	public bool hasEnded;

	[HideInInspector]
	public float timeElapsed;
	[HideInInspector]
	public int cashInInventory;
	[HideInInspector]
	public bool usedFlashlight;
	[HideInInspector]
	public int timesDetected;
	[HideInInspector]
	public int timesWokeUp;
	[HideInInspector]
	public bool enteredEmptyBedroom;
	[HideInInspector]
	public bool enteredBalconyDoor;
	[HideInInspector]
	public bool enteredFirstWindow;
	[HideInInspector]
	public bool enteredSecondWindow;
	[HideInInspector]
	public bool enteredBackDoor;
	[HideInInspector]
	public bool enteredBasementWindow;
	[HideInInspector]
	public bool enteredFrontDoor;
	[HideInInspector]
	public bool jumpedFence;
	[HideInInspector]
	public bool enteredGate;
	[HideInInspector]
	public int successfullHacks;
	[HideInInspector]
	public bool hackedSafe;
	[HideInInspector]
	public float copsTimeLeft;
	[HideInInspector]
	public int doorsLockpicked;
	[HideInInspector]
	public int windowsLockpicked;
	[HideInInspector]
	public int noiseBombDistractions;
	[HideInInspector]
	public int lighterDistractions;
	[HideInInspector]
	public int objectsBurned;

	private void Awake()
	{
		GameManager.Instance.cl = this;
	}

	// Start is called before the first frame update
	void Start()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		MainMenu.instance.visible();
	}

	public void StartGame()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		MainMenu.instance.visible();

		hasEnded = false;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

		timeElapsed = 0f;
		cashInInventory = 0;
		usedFlashlight = false;
		timesDetected = 0;
		timesWokeUp = 0;
		enteredEmptyBedroom = false;
		enteredBalconyDoor = false;
		enteredFirstWindow = false;
		enteredSecondWindow = false;
		enteredBackDoor = false;
		enteredBasementWindow = false;
		enteredFrontDoor = false;
		jumpedFence = false;
		enteredGate = false;
		successfullHacks = 0;
		hackedSafe = false;
		copsTimeLeft = 0f;
		doorsLockpicked = 0;
		windowsLockpicked = 0;
		noiseBombDistractions = 0;
		lighterDistractions = 0;
		objectsBurned = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if (!hasEnded)
		{
			timeElapsed += Time.deltaTime;
		}
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
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		hasEnded = true;
		cashInInventory = player.GetTotalStolen();
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
		GameManager.Instance.CheckAllChallenges();
	}

	public void CheckItemsOrigin(string tag)
	{

	}

}
