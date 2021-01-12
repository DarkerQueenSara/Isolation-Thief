using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipStatue : Interactable
{
    public Animator statueAnimator;

    public override void interact()
    {
        if (!LevelManager.Instance.oscarFlipped)
        {
            LevelManager.Instance.oscarFlipped = true;
            LevelManager.Instance.audioManager.Play("GoodDeed");
            statueAnimator.SetBool("Flipped", true);
        }
    }

    public override string getInteractingText()
    {
        return !LevelManager.Instance.oscarFlipped? "Flip the statue" : "";
    }
}
