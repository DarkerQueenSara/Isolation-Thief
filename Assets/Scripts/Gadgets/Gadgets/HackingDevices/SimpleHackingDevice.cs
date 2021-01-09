using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimpleHackingDevice : HackingDevice
{
    public const string gadgetID = "Simple Hacking Device";
    public System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
    private Action<int> gameEndCallback;

    public SimpleHackingDevice() : base()
    {
        usability = int.MaxValue; //infinite pmuch
        gadgetDependencies = new List<Gadget>();
        minLevel = 1;
        isPicking = false;
        this.unlocked = true;
        this.gadgetInfo = Resources.Load<GadgetInfo>(Gadget.GADGET_INFO_DIR + "SimpleHackingDevice");

    }

    public override float GetHackingTime()
    {
        float timeReduction = GameManager.Instance.level > 3 ? 3 : GameManager.Instance.level;
        return 4 - timeReduction; //3 to 1 
    }

    public override bool CanUse()
    {
        return base.CanUse();
    }

    public override void Use()
    {
        //Empty for now
    }


    private bool isPicking;
    private float finalTime;
    //public override bool HackObject()
    //{
    //    if(loadingBar == null)
    //    {
    //        loadingBar = LoadingBar.instance;
    //    }
    //    //Exit immedialy if you stop holding the interact button
    //    if (!isPicking)
    //    {
    //        isPicking = true;
    //        finalTime = Time.time + GetHackingTime();
    //        loadingBar.SetActive();
    //        //Debug.Log("Starting pick!");
    //        st.Start();
    //    }

    //    //Start the lockpicking process if not yet started
    //    /*if(progress == .0f)
    //    {

    //        loadingBar.SetActive();
    //        //loadingBar.loadBar = true;
    //        st.Start();
    //    }*/


    //    //Lockpick
    //    //float timeIncrement = 0.004f / GetLockPickingTime(); //around 3 seconds for a lvl 1 lockpick
    //    float progress = Mathf.Clamp01((GetHackingTime() - (finalTime - Time.time)) / GetHackingTime());
    //    if (progress < 1f) //if not done, continue lockpicking
    //    {
    //        loadingBar.SetLoadingBarStatus(progress, "Hacking: " + Mathf.Floor(progress * 100f) + "%");
    //    } else if (progress >= 1f) //if done, end and return true
    //    {
    //        //loadBar = false;
    //        isPicking = false;
    //        this.loadingBar.SetDisabled();
    //        st.Stop();
    //        Debug.Log(string.Format("Hacking took {0} ms to complete", st.ElapsedMilliseconds));
    //        st.Reset();

    //        this.Use();
    //        return true;
    //    }


    //    return false;
    //}

    private int count = 0;
    public override void HackObject(Action<int> gameEndCallback, int remainingTries)
    {
        this.gameEndCallback = gameEndCallback;
        HackingMinigameController.Instance.StartMinigame(onHackEnd, remainingTries);

    }

    private void onHackEnd(int finalResult)
    {
        if(finalResult == HackingMinigameController.WON_GAME || finalResult == HackingMinigameController.LOST_GAME)
        {
            count++;
        }
        gameEndCallback?.Invoke(finalResult);
    }

    //public override void stopHacking()
    //{
    //    this.loadingBar.SetDisabled();
    //    st.Stop();
    //    Debug.Log(string.Format("Hacked for {0} ms and stopped", st.ElapsedMilliseconds));
    //    st.Reset();
    //    isPicking = false;
    //}

}
