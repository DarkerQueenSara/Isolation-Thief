using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private UI_Inventory ui_Inventory;

    private Inventory inventory;

    private PlayerMovement playerMovement;
    private Controls playerControls;

    public float money = 60.0f;

    public bool isLit;

    public int level { get; private set; }

    private GadgetTree gadgetTree;
    public List<Gadget> inInventory { get; private set;}
    public Gadget leftHand { get; set; }
    public Gadget rightHand { get; set; }
    //private List<Gadget> onHand;

    private void Awake()
    {

        Instance = this;
        inventory = new SimpleBag();
        ui_Inventory.SetInventory(inventory);
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerControls = gameObject.GetComponent<Controls>();
        level = 1;
        gadgetTree = new GadgetTree();
        inInventory = new List<Gadget>();
        inInventory.Add(gadgetTree.gadgets[SimpleLockpick.gadgetID]);
        inInventory.Add(gadgetTree.gadgets[Lantern.gadgetID]);
        inInventory.Add(gadgetTree.gadgets[FastLockpick.gadgetID]);
        //asumir que so vai para a mao o que pode ser usado
        //onHand = new List<Gadget>();
        //onHand.Add(gadgetTree.gadgets[SimpleLockpick.gadgetID]);
        //onHand.Add(gadgetTree.gadgets[Lantern.gadgetID]);
        rightHand = gadgetTree.gadgets[SimpleLockpick.gadgetID];
        leftHand = gadgetTree.gadgets[Lantern.gadgetID];
    }

    public void changeMoney(float value)
    {
        this.money += value;
    }

    public float getMoney()
    {
        return this.money;
    }

    public void AddToInventory(Item item)
    {
        this.inventory.AddItem(item);
        ui_Inventory.Refresh();
    }

    public void DisableMovement()
    {
        playerMovement.disabled = true;
    }

    public void ChangeInventoryVisible()
    {
        this.ui_Inventory.visible();
        this.playerControls.setDisabled(this.ui_Inventory.isVisible());
    }

    public int GetTotalStolen()
    {
        return this.inventory.TotalValue;
    }


    public GadgetTree GetGadgetTree()
    {
        return this.gadgetTree;
    }

    public void unlockGadget(string gadgetName)
    {
        Gadget gadget = this.gadgetTree.gadgets[gadgetName];

        if(gadget != null)
        {
            gadget.unlocked = true;
        }
    }

    public void setInventoryGadgets(List<Gadget> chosenGadgets)
    {
        this.inInventory = chosenGadgets;
        this.leftHand = null;
        this.rightHand = null;
        foreach(Gadget gadget in chosenGadgets)
        {
            if (leftHand == null && gadget.getIsTypeF())
            {
                leftHand = gadget;
            }else if(rightHand == null && !gadget.getIsTypeF())
            {
                rightHand = gadget;
            }
        }
        this.ui_Inventory.Refresh();
    }
    public bool hasGadgetOnHand(String name)
    {
        /*foreach(Gadget gadget in this.onHand)
        {
            if(gadget.getGadgetType() == type)
            {
                return true;
            }
        }*/

        //se o name estiver em alguma das duas maos retornamos true
        return (rightHand != null && rightHand.getID() == name) || (leftHand != null && leftHand.getID() == name);
    }

    public bool hasGadgetOnHand(GadgetType type)
    {
        /*foreach(Gadget gadget in this.onHand)
        {
            if(gadget.getGadgetType() == type)
            {
                return true;
            }
        }*/

        //se o tipo estiver em alguma das duas maos retornamos true
        return (rightHand != null && rightHand.getGadgetType() == type) || (leftHand != null && leftHand.getGadgetType() == type);
    }

    public Gadget getGadgetOnHand(GadgetType type)
    {
        /*foreach (Gadget gadget in this.onHand)
        {
            if (gadget.getGadgetType() == type)
            {
                return gadget;
            }
        }*/
        if(rightHand != null && rightHand.getGadgetType() == type)
        {
            return rightHand;
        }
        if(leftHand != null && leftHand.getGadgetType() == type)
        {
            return leftHand;
        }
        return null;
    }

    public Gadget getGadgetTypeFOnHand()
    {
        /*foreach (Gadget gadget in this.onHand)
        {
            if (gadget.getIsTypeF())
            {
                return gadget;
            }
        }*/
        if (rightHand != null && rightHand.getIsTypeF())
        {
            return rightHand;
        }
        if (leftHand != null && leftHand.getIsTypeF())
        {
            return leftHand;
        }
        return null;
    }

    void Start()
    {
        //TODO remove this (or not!)
        this.unlockGadget(SimpleLockpick.gadgetID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
