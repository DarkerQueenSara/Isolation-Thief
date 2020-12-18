using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallPhone : MonoBehaviour
{

    public static CallPhone Instance { get; private set; }

    private float finalTime;
    private bool countdouwnStarted;
    private GameManager manager;

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
        manager = GameManager.Instance;
    }

    public void CallPolice()
    {
        if (NPCManager.Instance.CopsCalled && !countdouwnStarted)
        {
            Debug.Log("Chamou policia!");
            //These call cops are different from the NPC ones. The NPC ones select an NPC and move it to the phone or bedroom
            //This is to say: The cops responsiblility is now handed to the GameManager. The NPCManager has done everything
            GameManager.Instance.callCops();
            finalTime = Time.time + manager.timeTillCops;
            countdouwnStarted = true;
            countdownText.text = manager.timeTillCops.ToString("0");
            countdownText.enabled = true;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (countdouwnStarted && !manager.hasEnded)
        {
            float timeLeft = finalTime - Time.time;
            countdownText.text = timeLeft.ToString("0");
            if(timeLeft < 0.1f)
            {
                countdouwnStarted = false;
                countdownText.text = "";
                //acabar jogo
                manager.copsArrived = true;
                manager.endGame();
            }
        }
        if(countdouwnStarted && manager.hasEnded)
        {
            countdownText.text = "";
        }
    }
}
