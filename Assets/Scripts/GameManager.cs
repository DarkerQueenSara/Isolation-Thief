using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float moneyGoal = 1000.0f;
    public float timeTillCops = 30.0f;

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
        hasEnded = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void endGame()
    {
        hasEnded = true;
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
