using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLockpick : Lockpick
{

    float progress;
    public System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();

    public SimpleLockpick() : base()
    {
        usability = int.MaxValue; //infinite pmuch
        List<Gadget> gadgetDependencies = new List<Gadget>();
        minLevel = 1;
        progress = .0f;
    }

    public override float GetLockPickingTime()
    {
        float timeReduction = this.player.level > 3 ? 3 : this.player.level;
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

    public override bool LockpickObject()
    {
        //Exit immedialy if you stop holding the interact button
        if (Input.GetButtonUp("Interact"))
        {
            //loadBar = false;
            progress = .0f;
            this.loadingBar.SetDisabled();
            st.Stop();
            Debug.Log(string.Format("Lockpicked for {0} ms and stopped", st.ElapsedMilliseconds));
            st.Reset();
            return false;
        }

        //Start the lockpicking process if not yet started
        if(progress == .0f)
        {

            loadingBar.SetActive();
            //loadingBar.loadBar = true;
            st.Start();
        }


        //Lockpick
        float timeIncrement = 0.004f / GetLockPickingTime(); //around 3 seconds for a lvl 1 lockpick

        if (progress < 1f) //if not done, continue lockpicking
        {
            progress += timeIncrement;
            loadingBar.SetLoadingBarStatus(Mathf.Clamp01(progress), "Lockpicking: " + Mathf.Floor(progress * 100f) + "%");
        } else if (progress >= 1f) //if done, end and return true
        {
            //loadBar = false;
            progress = .0f;
            this.loadingBar.SetDisabled();
            st.Stop();
            Debug.Log(string.Format("Lockpicking took {0} ms to complete", st.ElapsedMilliseconds));
            st.Reset();

            this.Use();
            return true;
        }


        return false;
    }
}
