using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : Gadget
{
    public Light lantern;
    private AudioManager audioManager;

    public void Start()
    {
        
    }

    public Lantern()
    {
        lantern = GameObject.FindGameObjectWithTag("PlayerLantern").GetComponent<Light>();
        this.isTypeF = true;
    }

    public override void Use()
    {
        
        lantern.enabled = !lantern.enabled;
        LevelManager.Instance.usedFlashlight = true;
    }

    public override GadgetType getGadgetType()
    {
        return GadgetType.LANTERN;
    }

}
