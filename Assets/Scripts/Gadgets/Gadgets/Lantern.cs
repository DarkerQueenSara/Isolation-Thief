using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : Gadget
{
    public const string gadgetID = "Lantern";
    public Light lantern;
    private AudioManager audioManager;

    public void Start()
    {
        
    }

    public Lantern()
    {
        lantern = GameObject.FindGameObjectWithTag("PlayerLantern").GetComponent<Light>();
        this.useAnywhere = true;
        this.unlocked = true;
        this.gadgetInfo = Resources.Load<GadgetInfo>(Gadget.GADGET_INFO_DIR + "Lantern");
    }

    public override void Use()
    {

        //lantern.enabled = !lantern.enabled;
        //LevelManager.Instance.usedFlashlight = true;
    }

    public override GadgetType getGadgetType()
    {
        return GadgetType.LANTERN;
    }

}
