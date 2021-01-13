using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseController : Hackable
{
    private int uses;
    public float distractionDuration = 15.0f;
    private AudioManager audioManager;
    new private void Awake()
    {
        audioManager = this.gameObject.GetComponent<AudioManager>();
        uses = 0;
    }

    public override void interact()
    {
        if (isLocked)
        {
            base.interact();
            return;
        }
    }

    public new void Update()
    {
         if (!isLocked && uses == 0)
        {
            LevelManager.Instance.noisyHacks++;
            audioManager.Play(audioManager.sounds[0]);
            Invoke("StopSound", distractionDuration);
            //TODO fazer barulho para NPC (e preencher maxDist no som)
            NPCManager.Instance?.InvestigateSound(gameObject.transform.position, 500, 1);
            uses++;
        }
    }

    private void StopSound()
    {
        audioManager.Stop(audioManager.sounds[0]);

        //TODO parar barulho para NPC
    }

    public override string getInteractingText()
    {

        if (isLocked)
        {
            if (player.hasGadgetOnHand(GadgetType.HACKING_DEVICE) && NumTries < MAX_HACKING_TRIES)
            {
                return "Hack Device";
            }
            else
            {
                return "It's a keypad";
            }
        }
        return "";
    }
}
