using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	private static LevelManager instance;
	public AudioManager audioManager;
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

	public Player player;

	public int moneyGoal = 4000;
	public float timeTillCops = 15.0f;
	public bool copsCalled { get; private set; }
	public bool copsArrived;
	public bool hasEnded;
	public bool hasEndedSuccessfully;

	[HideInInspector]
	public float timeElapsed;
	[HideInInspector]
	public int cashInInventory;
	[HideInInspector]
	public bool usedFlashlight;
	[HideInInspector]
	public int timesDetected;
	[HideInInspector]
	public bool enteredEmptyBedroom;
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
	public int objectsBurned;
	[HideInInspector]
	internal int noisyHacks;

	[HideInInspector]
	public bool fedFishes;
	[HideInInspector]
	public bool trashInCan;
	[HideInInspector]
	public bool oscarFlipped;
    

    private void Awake()
	{
		GameManager.Instance.cl = this;
		audioManager = this.gameObject.GetComponent<AudioManager>();
	}

	// Start is called before the first frame update
	void Start()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		ChooseGadgetUI.Instance.changeVisibility();
		//StartGame();
	}

	public void StartGame()
	{
		Debug.Log("Level manager start game");
		audioManager.Play("Ambient");

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Time.timeScale = 1;

		hasEnded = false;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

		timeElapsed = 0f;
		cashInInventory = 0;
		usedFlashlight = false;
		timesDetected = 0;
		enteredEmptyBedroom = false;
		successfullHacks = 0;
		hackedSafe = false;
		copsTimeLeft = 0f;
		doorsLockpicked = 0;
		windowsLockpicked = 0;
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
		audioManager.Stop("Ambient");
		audioManager.Play("PoliceSirens");
	}

	public void endGame()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		hasEnded = true;
		cashInInventory = player.GetTotalStolen();
		audioManager.Stop("PoliceSirens");

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
			hasEndedSuccessfully = true;
			GameManager.Instance.money += (player.GetTotalStolen() - moneyGoal);
			LevelEndMenu.Instance.setText(
					"You WON!",
					"You escaped!",
					moneyGoal + " $",
					+player.GetTotalStolen() + " $",
					true);
		}
		NPCManager.Instance?.StopAllNPC();
		LevelEndMenu.Instance.visible();

		player.DisableMovement();
		GameManager.Instance.CheckAllChallenges();
	}

	public void CheckItemsOrigin(string tag)
	{

	}

}
