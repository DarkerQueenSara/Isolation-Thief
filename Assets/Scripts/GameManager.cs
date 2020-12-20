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

    public bool copsCalled { get; private set; }
    public bool copsArrived;
    public bool hasEnded;
    // Start is called before the first frame update
    void Start()
    {
        hasEnded = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
        SoundManagerScript.instance.stopSound();
        if (copsArrived) //Lose
        {
            LevelEndText.instance.setText(
                "You were caught by the cops!",
                "Goal : " + moneyGoal + " $",
                "Value : " + +player.GetTotalStolen() + " $",
                "You LOST!", false);
        }
        else if (player.GetTotalStolen() < moneyGoal) //Lose
        {
            LevelEndText.instance.setText(
                "You escaped!",
                "Goal : " + moneyGoal + " $",
                "Value : " + +player.GetTotalStolen() + " $",
                "You LOST!", false);
        }
        else //win
        {
            LevelEndText.instance.setText(
                "You escaped!",
                "Goal : " + moneyGoal + " $",
                "Value : " + +player.GetTotalStolen() + " $",
                "You WON!", true);
        }

        player.DisableMovement();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
