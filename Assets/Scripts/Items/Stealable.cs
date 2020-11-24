using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealable : Interactable
{
    public override void interact()
    {
        Player.Instance.addToInventory(this.GetComponent<Item>());
        Destroy(gameObject);
    }

    public override string getInteractingText()
    {
        return "Steal " + transform.name;
    }
}
