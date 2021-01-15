using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLockpick : Lockpick
{
    public const string gadgetID = "Simple Lockpick";
    public System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();

    public SimpleLockpick() : base()
    {
        usability = int.MaxValue; //infinite pmuch
        gadgetDependencies = new List<Gadget>();
        minLevel = 1;
        isPicking = false;
        this.unlocked = true;
        this.gadgetInfo = Resources.Load<GadgetInfo>(Gadget.GADGET_INFO_DIR + "SimpleLockpick");

    }

    public override float GetLockPickingTime()
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
    public override bool LockpickObject()
    {
        if (loadingBar == null)
        {
            loadingBar = LoadingBar.instance;
        }
        //Exit immedialy if you stop holding the interact button
        if (!isPicking)
        {
            isPicking = true;
            finalTime = Time.time + GetLockPickingTime();
            loadingBar.SetActive();
            //Debug.Log("Starting pick!");
            st.Start();
            playerAudioManager.Play("Lockpick");
        }

        //Start the lockpicking process if not yet started
        /*if(progress == .0f)
        {

            loadingBar.SetActive();
            //loadingBar.loadBar = true;
            st.Start();
        }*/


        //Lockpick
        //float timeIncrement = 0.004f / GetLockPickingTime(); //around 3 seconds for a lvl 1 lockpick
        float progress = Mathf.Clamp01((GetLockPickingTime() - (finalTime - Time.time)) / GetLockPickingTime());
        if (progress < 1f) //if not done, continue lockpicking
        {
            loadingBar.SetLoadingBarStatus(progress, "Lockpicking: " + Mathf.Floor(progress * 100f) + "%");
        }
        else if (progress >= 1f) //if done, end and return true
        {
            //loadBar = false;            
            playerAudioManager.Stop("Lockpick");
            isPicking = false;
            this.loadingBar.SetDisabled();
            st.Stop();
            Debug.Log(string.Format("Lockpicking took {0} ms to complete", st.ElapsedMilliseconds));
            st.Reset();

            this.Use();
            return true;
        }


        return false;
    }

    public override void stopPicking()
    {
        playerAudioManager.Stop("Lockpick");
        this.loadingBar.SetDisabled();
        st.Stop();
        Debug.Log(string.Format("Lockpicked for {0} ms and stopped", st.ElapsedMilliseconds));
        st.Reset();
        isPicking = false;
    }
}
