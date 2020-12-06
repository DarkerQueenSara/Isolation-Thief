using UnityEngine;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.Player.Controls
{
    public class DoorInteractionAnimator : Interactable
    {
        public Animator animator;

        //Public 
        public bool isLocked;

        //Private
        private bool isOpen;
        private float progress;
        private bool loadBar;
        private float timeToLockpick;
        Stopwatch st = new Stopwatch();

        private void Awake()
        {
            isOpen = false;
            progress = .0f;
            loadBar = false;
        }


        public override void interact()
        {
            if (isLocked)
            {
                if (lockpickable)
                {
                    if (player.CanLockpick())
                    {
                        this.StartLockpick();
                    }
                }


            } else
            {
                this.OpenOrClose();
            }
        }

        private void StartLockpick()
        {
            timeToLockpick = player.LockpickingTime();
            loadingBar.SetActive();
            loadBar = true;
            st.Start();
        }

        private void Update()
        {
            Lockpick();
        }

        private void Lockpick()
        {
            float timeIncrement = 0.004f / timeToLockpick; //around 3 seconds for a lvl 1 lockpick

            if (loadBar && !Input.GetButtonUp("Interact") && progress < 1f)
            {
                progress += timeIncrement;
                loadingBar.SetLoadingBarStatus(Mathf.Clamp01(progress), "Lockpicking: " + progress * 100f + "%");
            }
            if (progress >= 1f && loadBar)
            {
                loadBar = false;
                this.OpenOrClose();
                progress = 0;
                this.isLocked = false;
                this.loadingBar.SetDisabled();
                st.Stop();
                UnityEngine.Debug.Log(string.Format("MyMethod took {0} ms to complete", st.ElapsedMilliseconds));
            }

            if (Input.GetButtonUp("Interact") && loadBar)
            {
                loadBar = false;
                progress = 0;
                this.loadingBar.SetDisabled();
                st.Stop();
                Debug.Log(string.Format("MyMethod took {0} ms to complete", st.ElapsedMilliseconds));
            }
        }

        private void OpenOrClose()
        {
            animator.SetTrigger("OpenCloseDoor");
            isOpen = !isOpen;
        }

        public override string getInteractingText()
        {
            if(isLocked && isOpen)
            {
                Debug.LogError("Door is Locked and Open!");
                return "Close door";
            }

            if (isLocked && !isOpen)
            {
                if (lockpickable)
                {
                    if (player.CanLockpick())
                    {
                        return "Lockpick";
                    }
                }

                return "Locked";

            } else
            {

                return isOpen? "Close door" : "Open door";
            }
        }
    }
}