using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealable : Interactable
{
    public Item item;
    public override void interact()
    {
        Player.Instance.AddToInventory(item);

        Destroy(gameObject);
    }

    public override string getInteractingText()
    {
        return "Steal " + item.name;
    }
}
