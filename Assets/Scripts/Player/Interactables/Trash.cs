using UnityEngine;
using System.Collections;


public class Trash : Flammable
{
    public Item trashItem;

    new public void Awake()
    {
        base.Awake();
        base.objectName = "trash";
    }

    public override void interact()
    {
        if (!base.isBurning)
        {
            base.interact();
            //If whe didnt set it on fire with a lighter
            if (!base.isBurning)
            {
                Player.Instance.AddToInventory(trashItem, transform.parent.gameObject);
            }
        }
    }

    public override string getInteractingText()
    {
        string baseText = base.getInteractingText();
        if (baseText != "")
        {
            return baseText;
        }
        if (base.isBurning)
        {
            return "";
        }
        return"Take trash";
    }
}