using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyCamRetriever : Interactable
{
    SpyCam spyCam;
    new private void Start()
    {
        base.Start();
        spyCam = (SpyCam) Player.Instance.GetGadgetTree().GetGadget(SpyCam.gadgetID);
    }

    public override void interact()
    {
        spyCam.RetrieveCamera();
    }

    public override string getInteractingText()
    {
        return "Retrieve SpyCam";
    }
}
