using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallPhone : MonoBehaviour
{
    public float timeTillCops = 30.0f;
    public NPCMovement NPC;
    
    private float currentTime;
    private float finalTime;
    private bool countdouwnStarted;
    private GameManager manager;

    [SerializeField] Text countdownText;
    // Start is called before the first frame update
    void Start()
    {
        countdouwnStarted = false;
        manager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC") && NPC.callingCops && !countdouwnStarted)
        {
            Debug.Log("Chamou policia!");
            GameManager.Instance.copsCalled = true;
            NPC.callingCops = false;
            //chamou policia
            this.CallPolice();
        }
    }
    public void CallPolice()
    {
        currentTime = Time.time;
        finalTime = currentTime + timeTillCops;
        countdouwnStarted = true;
        countdownText.text = timeTillCops.ToString("0");
        countdownText.enabled = true;
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
