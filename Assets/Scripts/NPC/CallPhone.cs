using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallPhone : MonoBehaviour
{
    public NPCMovement NPC;

    private float finalTime;
    private bool countdouwnStarted;
    private LevelManager manager;

    [SerializeField] Text countdownText;
    // Start is called before the first frame update
    void Start()
    {
        countdouwnStarted = false;
        manager = LevelManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC") && NPC.callingCops && !countdouwnStarted)
        {
            Debug.Log("Chamou policia!");
            LevelManager.Instance.callCops();
            NPC.callingCops = false;
            //chamou policia
            this.CallPolice();
        }
    }
    public void CallPolice()
    {
        finalTime = Time.time + manager.timeTillCops;
        countdouwnStarted = true;
        countdownText.text = manager.timeTillCops.ToString("0");
        countdownText.enabled = true;
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
