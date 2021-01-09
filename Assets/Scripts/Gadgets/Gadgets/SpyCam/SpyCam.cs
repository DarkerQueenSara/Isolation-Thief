using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyCam : Gadget
{
    public const string gadgetID = "SpyCam";
    private SpyCamController controller;
    private bool hasCamera = false;
    private bool isShowing = false;

    public SpyCam() : base()
    {
        gadgetDependencies = new List<Gadget>();
        unlocked = true;
        this.useAnywhere = true;
        this.gadgetInfo = Resources.Load<GadgetInfo>(Gadget.GADGET_INFO_DIR + "SpyCam");
    }

    public override bool CanUse()
    {
        return base.CanUse();
    }

    

    public override GadgetType getGadgetType()
    {
        return GadgetType.CAMERA;
    }

    public override void Use()
    {
        if(controller == null)
        {
            controller = SpyCamController.Instance;
        }
        if (!hasCamera)
        {
            controller.CreateSpyCam();
            hasCamera = !hasCamera;
        }
        else
        {
            if (!isShowing)
            {
                controller.ShowSpyCam();
            }
            else
            {
                controller.StopShowingSpyCam();
            }
            isShowing = !isShowing;
        }
        //Apart from decreasing fuel does nothing, class Flammable does the work
    }

    public void RetrieveCamera()
    {
        if (controller == null)
        {
            controller = SpyCamController.Instance;
        }
        controller.RetrieveSpyCam();
        isShowing = false;
        hasCamera = false;
    }
}
