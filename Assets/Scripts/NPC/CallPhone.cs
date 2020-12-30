using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallPhone : MonoBehaviour
{

    public static CallPhone Instance { get; private set; }

    private float finalTime;
    private bool countdouwnStarted;
    private LevelManager manager;

    [SerializeField] Text countdownText;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        countdouwnStarted = false;
        manager = LevelManager.Instance;
    }

    public void CallPolice()
    {
        if (NPCManager.Instance.CopsCalled && !countdouwnStarted)
        {
            Debug.Log("Chamou policia!");
            LevelManager.Instance.callCops();
            finalTime = Time.time + manager.timeTillCops;
            countdouwnStarted = true;
            countdownText.text = manager.timeTillCops.ToString("0");
            countdownText.enabled = true;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (countdouwnStarted)
        {
            float timeLeft = finalTime - Time.time;
            if (!manager.hasEnded)
            {
                countdownText.text = timeLeft.ToString("0");
                if (timeLeft < 0.1f)
                {
                    countdouwnStarted = false;
                    countdownText.text = "";
                    //acabar jogo
                    manager.copsArrived = true;
                    manager.endGame();
                }
            }
            else
            {
                countdownText.text = "";
                LevelManager.Instance.copsTimeLeft = timeLeft;
            }
        }
    }
}
