using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedFishes : Interactable
{
    public override void interact()
    {
        if (!LevelManager.Instance.fedFishes)
        {
            LevelManager.Instance.fedFishes = true;
            LevelManager.Instance.audioManager.Play("GoodDeed");
        }
    }

    public override string getInteractingText()
    {
        return !LevelManager.Instance.fedFishes ? "Feed the fishes" : "";
    }
}
