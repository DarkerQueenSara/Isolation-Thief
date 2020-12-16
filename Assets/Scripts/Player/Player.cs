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

    public bool isLit;

    public int level { get; private set; }

    private GadgetTree gadgetTree;
    private List<Gadget> onHand;

    private void Awake()
    {

        Instance = this;
        inventory = new SimpleBag();
        ui_Inventory.SetInventory(inventory);
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerControls = gameObject.GetComponent<Controls>();
        level = 1;
        gadgetTree = new GadgetTree();
        //asumir que so vai para a mao o que pode ser usado
        onHand = new List<Gadget>();
        onHand.Add(gadgetTree.gadgets["lockpick"]);
        onHand.Add(gadgetTree.gadgets["lantern"]);
    }

    public void AddToInventory(Item item)
    {
        this.inventory.AddItem(item);
        ui_Inventory.RefreshInventoryItems();
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

    public float GetTotalStolen()
    {
        return this.inventory.TotalValue;
    }


    public void unlockGadget(string gadgetName)
    {
        Gadget gadget = this.gadgetTree.gadgets[gadgetName];

        if(gadget != null)
        {
            gadget.unlocked = true;
        }
    }

    public bool hasGadgetOnHand(GadgetType type)
    {
        foreach(Gadget gadget in this.onHand)
        {
            if(gadget.getGadgetType() == type)
            {
                return true;
            }
        }
        return false;
    }

    public Gadget getGadgetOnHand(GadgetType type)
    {
        foreach (Gadget gadget in this.onHand)
        {
            if (gadget.getGadgetType() == type)
            {
                return gadget;
            }
        }
        return null;
    }

    public Gadget getGadgetTypeFOnHand()
    {
        foreach (Gadget gadget in this.onHand)
        {
            if (gadget.getIsTypeF())
            {
                return gadget;
            }
        }
        return null;
    }

    void Start()
    {
        //TODO remove this (or not!)
        this.unlockGadget("lockpick");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
