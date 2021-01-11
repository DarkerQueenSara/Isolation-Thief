using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	// Scenes indexes
	const int MAIN_MENU = 0;
	const int LEVEL_MENU = 1;
	const int GADGETS_MENU = 2;
	const int SKILLS_MENU = 3;
	const int LEVEL_SCENE = 4;

	private static GameManager instance;
	public static AudioManager audioManager;
	public static GameManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameObject("GameManager").AddComponent<GameManager>();
				DontDestroyOnLoad(instance);
			}
			return instance;
		}
	}

	public void OnApplicationQuit()
	{
		instance = null;
	}

	private void Awake()
	{
		CreateAllChallenges();
		InitializePlayerInfo();
	}

	//Player information
	public int level { get; set; }
	public GadgetTree gadgetTree { get; private set; }
	public SkillsTree skillsTree { get; private set; }

	void InitializePlayerInfo()
	{
		level = 1;
		gadgetTree = new GadgetTree();
		skillsTree = new SkillsTree();
	}
	//------------------


	//cl = currentLevel
	public LevelManager cl;

	//TODO meter aqui as três árvores
	List<Challenge> goodDeeds = new List<Challenge>();
	List<Challenge> challenges = new List<Challenge>();
	//Cash to buy gadgets/ammo for gadgets with
	public int money;
	//Points to buy skills with
	public int availableXp;
	public int availableKp;
	//Total points (to show mastery of the level)
	public int totalXp;
	public int totalKp;

	void CreateAllChallenges()
	{
		//TODO ajustar nomes, numero e experiencia de todas estas challenges

		//Numero na lista porque vou dar sort, nome, descrição, check, exp
		challenges.Add(new Challenge(1, "Challenge 1", "Beat the level in under 3 minutes", () => cl.timeElapsed < 3 * 60, 400));
		challenges.Add(new Challenge(2, "Challenge 2", "Beat the level in under 4 minutes", () => cl.timeElapsed < 4 * 60, 350));
		challenges.Add(new Challenge(3, "Challenge 3", "Beat the level in under 5 minutes", () => cl.timeElapsed < 5 * 60, 300));
		challenges.Add(new Challenge(4, "Challenge 4", "Beat the level in under 6 minutes", () => cl.timeElapsed < 6 * 60, 250));
		challenges.Add(new Challenge(5, "Challenge 5", "Beat the level in under 7 minutes", () => cl.timeElapsed < 7 * 60, 200));
		challenges.Add(new Challenge(6, "Challenge 6", "Beat the level in under 8 minutes", () => cl.timeElapsed < 8 * 60, 150));
		challenges.Add(new Challenge(7, "Challenge 7", "Beat the level in under 9 minutes", () => cl.timeElapsed < 9 * 60, 100));
		challenges.Add(new Challenge(8, "Challenge 8", "Beat the level in under 10 minutes", () => cl.timeElapsed < 10 * 60, 50));
		challenges.Add(new Challenge(9, "Challenge 9", "Beat the level with at least 6000$", () => cl.cashInInventory >= 6000, 200));
		challenges.Add(new Challenge(10, "Challenge 10", "Beat the level with at least 7000$", () => cl.cashInInventory >= 7000, 250));
		challenges.Add(new Challenge(11, "Challenge 11", "Beat the level with at least 8000$", () => cl.cashInInventory >= 8000, 300));
		challenges.Add(new Challenge(12, "Challenge 12", "Beat the level with at least 9000$", () => cl.cashInInventory >= 9000, 350));
		challenges.Add(new Challenge(13, "Challenge 13", "Beat the level with at least 10000$", () => cl.cashInInventory >= 10000, 400));
		challenges.Add(new Challenge(14, "Challenge 14", "Beat the level without turning on the flashlight", () => !cl.usedFlashlight, 200));
		challenges.Add(new Challenge(15, "Challenge 15", "Clear the level undetected", () => cl.timesDetected < 1, 400));
		/**/
		challenges.Add(new Challenge(16, "Challenge 16", "Clear the level without waking anyone up", () => cl.timesWokeUp < 1, 400));
		/**/
		challenges.Add(new Challenge(17, "Challenge 17", "Enter thr bedroom while it's empty", () => cl.enteredEmptyBedroom, 400));
		//TODO ver se estas dao bem 
		challenges.Add(new Challenge(18, "Challenge 18", "Evade the cops with 10 seconds to spare", () => cl.copsCalled && cl.copsTimeLeft >= 10, 200));
		challenges.Add(new Challenge(19, "Challenge 19", "Evade the cops with 20 seconds to spare", () => cl.copsCalled && cl.copsTimeLeft >= 20, 300));
		challenges.Add(new Challenge(20, "Challenge 20", "Evade the cops with 30 seconds to spare", () => cl.copsCalled && cl.copsTimeLeft >= 30, 400));
		/**/
		challenges.Add(new Challenge(22, "Challenge 22", "Get inside the house through a window", () => cl.enteredFirstWindow, 400));
		/**/
		challenges.Add(new Challenge(24, "Challenge 24", "Get inside the house through the back door", () => cl.enteredBackDoor, 400));
		/**/
		challenges.Add(new Challenge(26, "Challenge 26", "Get inside the house through the front door", () => cl.enteredFrontDoor, 400));
		/**/
		challenges.Add(new Challenge(27, "Challenge 27", "Get to the yard by jumping the fence", () => cl.jumpedFence, 400));
		/**/
		challenges.Add(new Challenge(28, "Challenge 28", "Get to the yard through the front gate", () => cl.jumpedFence, 400));
		//TODO meter o numero correcto de hacks possiveis
		/**/
		challenges.Add(new Challenge(29, "Challenge 29", "Hack sucessfully all hackable devices", () => cl.successfullHacks == 5, 400));
		/**/
		challenges.Add(new Challenge(30, "Challenge 30", "Hack sucessfully once", () => cl.successfullHacks >= 1, 400));
		/**/
		challenges.Add(new Challenge(31, "Challenge 31", "Hack the safe", () => cl.hackedSafe, 400));
		challenges.Add(new Challenge(32, "Challenge 32", "Lockpick a door", () => cl.doorsLockpicked >= 1, 50));
		challenges.Add(new Challenge(33, "Challenge 33", "Lockpick a window", () => cl.windowsLockpicked >= 1, 50));

		//TODO regressar a estas quando implementares drop do inventario
		/*
        challenges.Add(new Challenge(34, "Challenge 34", "Steal only from the basement", () => cl.timesWokenUp < 1, 400));
        challenges.Add(new Challenge(35, "Challenge 35", "Steal only from the bedrooms", () => cl.timesWokenUp < 1, 400));
        challenges.Add(new Challenge(36, "Challenge 36", "Steal only from the dining room", () => cl.timesWokenUp < 1, 400));
        challenges.Add(new Challenge(37, "Challenge 37", "Steal only from the first floor", () => cl.timesWokenUp < 1, 400));
        challenges.Add(new Challenge(38, "Challenge 38", "Steal only from the gym", () => cl.timesWokenUp < 1, 400));
        challenges.Add(new Challenge(39, "Challenge 39", "Steal only from the kitchen (and the corridor in front)", () => cl.timesWokenUp < 1, 400));
        challenges.Add(new Challenge(40, "Challenge 40", "Steal only from the living room", () => cl.timesWokenUp < 1, 400));
        challenges.Add(new Challenge(41, "Challenge 41", "Steal only from the office (and the corridor in front)", () => cl.timesWokenUp < 1, 400));
        challenges.Add(new Challenge(42, "Challenge 42", "Steal only from the second floor", () => cl.timesWokenUp < 1, 400));
        */

		/**/
		challenges.Add(new Challenge(43, "Challenge 43", "Use a Noise Bomb to distract the home owner", () => cl.noiseBombDistractions >= 1, 400));
		//TODO nestas duas preencher o numero certo de maxNoiseBombs e de objetos flamaveis
		/**/
		challenges.Add(new Challenge(44, "Challenge 44", "Use all Noise Bombs to distract the home owner", () => cl.noiseBombDistractions == 5, 400));
		/**/
		challenges.Add(new Challenge(45, "Challenge 45", "Use the Lighter to burn all flamable objects", () => cl.objectsBurned == 5, 400));
		/**/
		challenges.Add(new Challenge(46, "Challenge 46", "Use the Lighter to distract the home owner", () => cl.lighterDistractions >= 1, 400));

		challenges = challenges.OrderBy(c => c.number).ToList();


		/**/
		goodDeeds.Add(new Challenge(1, "Don't Sleep With Them. They're Hungry", "Feed the fishes", () => cl.fedFishes, 400));
		/**/
		goodDeeds.Add(new Challenge(2, "Get That Garbage Outta Here!", "Put the trash in the garbage can", () => cl.trashInCan, 400));
		/**/
		goodDeeds.Add(new Challenge(3, "And The Award Goes Too...", "Put the oscar back up", () => cl.oscarFlipped, 400));

		goodDeeds = goodDeeds.OrderBy(c => c.number).ToList();

	}

	public void CheckAllChallenges()
	{
		if (cl.hasEndedSuccessfully)
		{
			foreach (Challenge c in challenges)
			{
				c.checkFullfiled();
				if (c.fullfilled)
				{
					availableXp += c.xp;
					Debug.Log(c.name + ": " + c.description + " - FULLFILLED!");
				}
			}

			foreach (Challenge c in goodDeeds)
			{
				c.checkFullfiled();
				if (c.fullfilled)
				{
					availableKp += c.xp;
					Debug.Log(c.name + ": " + c.description + " - FULLFILLED!");
				}
			}
		}
	}

	public List<Challenge> GetChallenges()
	{
		return challenges;
	}

	public List<Challenge> GetGoodDeeds()
	{
		return goodDeeds;
	}

	public void NewGame()
	{
		// Index defined in project build settings
		SceneManager.LoadScene(LEVEL_MENU);
		//cl = LevelManager.Instance;
	}

	public void StartLevel()
	{
		SceneManager.LoadScene(LEVEL_SCENE);
		//cl.StartGame();
	}

	public void ShowMainMenu()
	{
		SceneManager.LoadScene(MAIN_MENU);
	}

	public void ShowLevelMenu()
	{
		SceneManager.LoadScene(LEVEL_MENU);
	}

	public void ShowGadgetsMenu()
	{
		SceneManager.LoadScene(GADGETS_MENU);
	}

	public void ShowSkillsMenu()
	{
		SceneManager.LoadScene(SKILLS_MENU);
	}

}
