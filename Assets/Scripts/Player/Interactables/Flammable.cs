using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : Interactable
{
    public GameObject flameAnim;
    protected bool isBurning;
    protected string objectName;

    public void Awake()
    {
        objectName = gameObject.name;
        isBurning = false;
    }

    new public void Start()
    {
        base.Start();
        flameAnim.SetActive(false);
    }

    public override void interact()
    {
        //this method is ment to be blank
        if (!isBurning)
        {
            Lighter lighter = (Lighter)player.getGadgetOnHand(GadgetType.LIGHTER);
            if (lighter != null && lighter.CanUse())
            {
                isBurning = true;
                lighter.Use();
                flameAnim.SetActive(true);
                LevelManager.Instance.objectsBurned++;
                //TODO NPC REACT TO FIRE = lighterDistractions++
            }
        }
    }

    public override string getInteractingText()
    {
        Debug.Log("Looking");
        if (!isBurning)
        {
            Lighter lighter = (Lighter)player.getGadgetOnHand(GadgetType.LIGHTER);
            if (lighter != null && lighter.CanUse())
            {
                return "Light " + objectName + " on fire";
            }
        }
        return "";
    }
}
